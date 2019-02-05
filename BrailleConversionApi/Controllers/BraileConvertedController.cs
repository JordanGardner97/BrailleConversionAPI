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
        [Route("api/GetReponse")]
        public IHttpActionResult Get()
        {
            return Ok("No picture has been provided");
        }



        [HttpGet]
        [Route("api/GetLetterReponse/{Base64Image:alpha}")]
        public IHttpActionResult Get(String Base64Image)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

            byte[] byteresponse = Convert.FromBase64String(Base64Image);

            MemoryStream imageToConvert = new MemoryStream(byteresponse);
       

            Debug.WriteLine("Here I am!!!!!");
                BraileConverted letter = new BraileConverted();
                Bitmap image =new Bitmap(imageToConvert);
                
                ImageCoversionClass normalPicture = new ImageCoversionClass();
                Debug.WriteLine("Got Here");
                Bitmap greyImage = normalPicture.Invert(image);

                BraileImages m = new BraileImages();

                SimilarityClass sim = new SimilarityClass();

                letter.CovertedBrailleLetter = sim.GetLetter(greyImage);
                return Ok(letter.CovertedBrailleLetter);
           


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
