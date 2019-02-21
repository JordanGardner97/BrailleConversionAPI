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
        [Route("api/GetLetter/{Letter:alpha}")]
        public IHttpActionResult Get(String Letter)
        {
            BraileConverted letter = new BraileConverted(Letter);
            
            return Ok(letter);
        }



        [HttpPost]
        [Route("api/GetLetterReponse")]
        public async Task<HttpResponseMessage> Post([FromBody] Stream Base64Image)
        {
            Image imageSentFromApi;
            

            using (var stream = await Request.Content.ReadAsStreamAsync())
            {

                imageSentFromApi = Image.FromStream(stream);
            }
            Bitmap bmp = new Bitmap(imageSentFromApi);

            ImageCoversionClass normalPicture = new ImageCoversionClass(bmp);

           Bitmap resizedImage =  normalPicture.ResizeImage();

            List<Bitmap> croppedPhotos = normalPicture.cropImageIntoSegments(resizedImage);

            CircleDetectionClass circlesDection = new CircleDetectionClass(croppedPhotos);

            List<bool> circleThereList = circlesDection.GetBoolList();

            LetterDector dectoring = new LetterDector(circleThereList);

            BraileConverted letter = new BraileConverted();

            letter.CovertedBrailleLetter = dectoring.checkLetter();


            /*  ImageCoversionClass normalPicture = new ImageCoversionClass();
              Debug.WriteLine("Got Here");
              Bitmap blackImage = normalPicture.GreyImage(bmp);
              Bitmap greyImage = normalPicture.Invert(blackImage);
              SimilarityClass sim = new SimilarityClass();
              BraileConverted letter = new BraileConverted();
              letter.CovertedBrailleLetter = sim.GetLetter(greyImage);
              */






            Debug.WriteLine("The Letter is "+ letter.CovertedBrailleLetter);
            HttpRequestMessage request = new HttpRequestMessage();

        
           string url = "https://brailleconversionapi20190131031437.azurewebsites.net/api/GetLetter/" + letter.CovertedBrailleLetter;
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
           response.Headers.Location = new Uri( url);

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


        [HttpPost]
        [Route("api/GetLetter")]
        public async Task<IHttpActionResult> AddFile()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/");
            var provider = new MultipartFormDataStreamProvider(root);
            var result = await Request.Content.ReadAsMultipartAsync(provider);

            var originalFileName = GetDeserializedFileName(result.FileData.First());

            var uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName);
            string path = result.FileData.First().LocalFileName;
            
            BraileConverted letter = new BraileConverted();
            Bitmap image = new Bitmap(path);
            //Stopping here
            ImageCoversionClass normalPicture = new ImageCoversionClass();
            Debug.WriteLine("Got Here");
            Bitmap greyImage = normalPicture.Invert(image);

            BraileImages m = new BraileImages();

            SimilarityClass sim = new SimilarityClass();

            letter.CovertedBrailleLetter = sim.GetLetter(greyImage);

            return Ok(letter.CovertedBrailleLetter);
        }




        /*

        [HttpPost]
        [Route("api/PostImage")]
        public async Task<HttpResponseMessage> AddFile()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);
               // Stream stream = content.ReadAsStreamAsync().Result;

                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {
                    Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                    Trace.WriteLine("Server file path: " + file.LocalFileName);
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

       




        public IHttpActionResult Post()
        {
            Image image = null;
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            if (Request.Content.IsMimeMultipartContent())
            {

                Request.Content.ReadAsMultipartAsync<MultipartMemoryStreamProvider>(new MultipartMemoryStreamProvider()).ContinueWith((task) =>
                {
                    MultipartMemoryStreamProvider provider = task.Result;
                    foreach (HttpContent content in provider.Contents)
                    {
                        Stream stream = content.ReadAsStreamAsync().Result;
                        image = Image.FromStream(stream);
                        var testName = content.Headers.ContentDisposition.Name;
                        String filePath = HostingEnvironment.MapPath("~/Images/");
                        String[] headerValues = (String[])Request.Headers.GetValues("UniqueId");
                        String fileName = headerValues[0] + ".jpg";
                        String fullPath = Path.Combine(filePath, fileName);
                        image.Save(fullPath);
                    }
                });




                return result;
            }
            else
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
            }


        }*/







    }
}
