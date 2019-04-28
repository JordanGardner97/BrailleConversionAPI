using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BrailleConversionApi.Models;
using BrailleConversionApi.Classes;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Web;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web.Hosting;

namespace BrailleConversionApi.Controllers
{
    public class BraileConvertedController : ApiController
    {

        [HttpGet]
        [Route("api/GetLetter/{Letter}")]
        public IHttpActionResult Get(String Letter)
        {
            BraileConverted letter = new BraileConverted(Letter);
            
            return Ok(letter);
        }


        //For single braile image
        [HttpPost]
        [Route("api/GetLetterReponse")]
        public async Task<HttpResponseMessage> Post()
        {
            Image imageSentFromApi;
            

            using (var stream = await Request.Content.ReadAsStreamAsync())
            {

                imageSentFromApi = Image.FromStream(stream);
            }
          Bitmap bmp = new Bitmap(imageSentFromApi);

            ImageCoversionClass normalPicture = new ImageCoversionClass(bmp);


            
            List<Bitmap> croppedPhotos = normalPicture.cropImageIntoSegments(normalPicture.edgedetection(bmp));

            CircleDetectionClass circlesDection = new CircleDetectionClass(croppedPhotos);

            List<bool> circleThereList = circlesDection.GetBoolList();

            LetterDector dectoring = new LetterDector(circleThereList);

            BraileConverted letter = new BraileConverted();

            letter.CovertedBrailleLetter = dectoring.checkLetter();



            Debug.WriteLine("The Letter is "+ letter.CovertedBrailleLetter);
            HttpRequestMessage request = new HttpRequestMessage();

           //Returns the string to the in the 
           string url = "https://brailleconversionapi20190131031437.azurewebsites.net/api/GetLetter/" + letter.CovertedBrailleLetter;
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
           response.Headers.Location = new Uri( url);

            return response;

        }



        //for more then one bit map use
        [HttpPost]
        [Route("api/GetLetterReponse2")]
        public async Task<HttpResponseMessage> Post2()
        {
            Image imageSentFromApi;


            using (var stream = await Request.Content.ReadAsStreamAsync())
            {

                imageSentFromApi = Image.FromStream(stream);
            }
            Bitmap bmp = new Bitmap(imageSentFromApi);

            Debug.WriteLine("The width of the orginal bitmap is: " + bmp.Width);
            Debug.WriteLine("The height of the orginal bitmap is: " + bmp.Height);


            Debug.WriteLine("I got 3");
            ImageCoversionClass converseImage = new ImageCoversionClass();
            Debug.WriteLine("I got 2");

            List<Bitmap> row = converseImage.breakBigBitMapUp(bmp);
            Debug.WriteLine("I got ");
            List<List<Bitmap>> braileList = new List<List<Bitmap>>();



            int count = 0;


            Debug.WriteLine("The size is " + row.Count);

            foreach (var i in row)
            {


                //Debug.WriteLine("The width of the bitmap is: " + i.Width);
                //Debug.WriteLine("The height of the bitmap is: " + i.Height);

                try
                {
                    List<int> coordinates = converseImage.GetLocationOfAllCircles(i);



                    braileList.Add(converseImage.cropfromPhotedImage(converseImage.edgedetection(i), coordinates[0], coordinates[1], coordinates[2]));
                    Debug.WriteLine("The count is " + count);
                    count++;

                }

                catch (ArgumentOutOfRangeException)
                {

                    braileList.Add(converseImage.cropfromPhotedImage(converseImage.edgedetection(i), 0, 0, 0));
                    //Debug.WriteLine("The count is " + count);
                    count++;
                }


            }

            List<String> letters = new List<String>();
            foreach (var j in braileList)
            {

                Debug.WriteLine("\n");
                CircleDetectionClass circlesDection = new CircleDetectionClass(j);

                List<bool> circleThereList = circlesDection.GetBoolList();

                LetterDector dectoring = new LetterDector(circleThereList);





                Debug.WriteLine("The letter is " + dectoring.checkLetter());

                letters.Add(dectoring.checkLetter());


                //Debug.WriteLine("i HAVE FINISHED A ROUND"+ count+"\n");




            }

            string allChars = null;

            foreach (var k in letters)
            {

                if (k != "?")
                {

                    allChars += k;
                }
            }

            BraileConverted letter = new BraileConverted();
            letter.CovertedBrailleLetter = allChars;





            //Debug.WriteLine("The Letter is " + letter.CovertedBrailleLetter);
            HttpRequestMessage request = new HttpRequestMessage();


            string url = "https://brailleconversionapi20190131031437.azurewebsites.net/api/GetLetter/" + letter.CovertedBrailleLetter;
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(url);

            return response;

        }





        private string GetDeserializedFileName(MultipartFileData fileData)
        {
            var fileName = GetFileName(fileData);
            return JsonConvert.DeserializeObject(fileName).ToString();
        }

        public string GetFileName(MultipartFileData fileData)
        {
            return fileData.Headers.ContentDisposition.FileName;
        }


        //[HttpPost]
        //[Route("api/GetLetter")]
        //public async Task<IHttpActionResult> AddFile()
        //{
        //    if (!Request.Content.IsMimeMultipartContent())
        //    {
        //        this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
        //    }

        //    string root = HttpContext.Current.Server.MapPath("~/");
        //    var provider = new MultipartFormDataStreamProvider(root);
        //    var result = await Request.Content.ReadAsMultipartAsync(provider);

        //    var originalFileName = GetDeserializedFileName(result.FileData.First());

        //    var uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName);
        //    string path = result.FileData.First().LocalFileName;
            
        //    BraileConverted letter = new BraileConverted();
        //    Bitmap image = new Bitmap(path);
        //    //Stopping here
        //    ImageCoversionClass normalPicture = new ImageCoversionClass();
           
        //    Bitmap greyImage = normalPicture.Invert(image);

        //    BraileImages m = new BraileImages();

        //    SimilarityClass sim = new SimilarityClass();

        //    letter.CovertedBrailleLetter = sim.GetLetter(greyImage);

        //    return Ok(letter.CovertedBrailleLetter);
        //}




  


    }
}
