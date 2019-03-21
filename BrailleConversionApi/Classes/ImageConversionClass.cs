using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.IO;
using AForge.Imaging.Filters;
using System.Diagnostics;

namespace BrailleConversionApi.Classes
{
    public class ImageCoversionClass
    {
        MemoryStream Imagestream { get; set; }
        Bitmap BitmapToBeGreyed { get; set; }
        Bitmap BitmapToBeBlackened { get; set; }
        Bitmap BitmapToBeResized { get; set; }

        public ImageCoversionClass(Bitmap image)
        {
            this.BitmapToBeGreyed = image;
            this.BitmapToBeBlackened = image;
            this.BitmapToBeResized = image;
        }

        public ImageCoversionClass(Stream streamImage)
        {
            BitmapToBeGreyed = new Bitmap(streamImage);
            BitmapToBeBlackened = new Bitmap(streamImage);

        }

        public ImageCoversionClass(Image image)
        {
            BitmapToBeGreyed = new Bitmap(image);
            BitmapToBeBlackened = new Bitmap(image);


        }

        public ImageCoversionClass()
        {
            

        }

        public Bitmap GreyImage(Bitmap bmp)
        {
            // create grayscale filter (BT709)
            Grayscale filter = new Grayscale(0.2125, 0.7154, 0.0721);
            // apply the filter
            Bitmap greyedImage = filter.Apply(bmp);
            



            return greyedImage;
        }

        public Bitmap ThreseHoldImage(Bitmap bmp)
        {
            Threshold filter = new Threshold(100);
            Bitmap blackenedImage = filter.Apply(bmp);

            return blackenedImage;
        }

        public Bitmap Invert(Bitmap imageToBlacken)
        {
            Debug.WriteLine("Got Here 2");
            Invert filter = new Invert();
            Debug.WriteLine("Got Here 3");
            Bitmap image = new Bitmap(imageToBlacken);
            Debug.WriteLine("Got Here 4");
           
                Bitmap blackenedImage = filter.Apply(imageToBlacken);
            Debug.WriteLine("Got Here 4");
            return blackenedImage;
           
            
        }

        public List<Bitmap> cropImageIntoSegments(Bitmap imageToBeCropped)
        {

            double BitsThirdDouble = (imageToBeCropped.Width * 0.3333);
            double BitsHalfDouble = (imageToBeCropped.Width * 0.3333);

            int BitsThirdInt = Convert.ToInt32(BitsThirdDouble);



            List<Bitmap> images = new List<Bitmap>();
            Crop topLeftCorner = new Crop(new Rectangle(0, 0, 500, 500));
            Crop topRightCorner = new Crop(new Rectangle(0, 500, 500, 500));
            Crop leftMiddleCorner = new Crop(new Rectangle(0, 500, 500, 500));
            Crop rightMiddleCorner = new Crop(new Rectangle(500, 500, 500, 500));
            Crop bottomLeftCorner = new Crop(new Rectangle(0, 1000, 500, 500));
            Crop bottomRightCorner = new Crop(new Rectangle(500, 1000, 500, 500));

            images.Add(topLeftCorner.Apply(imageToBeCropped));
            images.Add(topRightCorner.Apply(imageToBeCropped));
            images.Add(leftMiddleCorner.Apply(imageToBeCropped));
            images.Add(rightMiddleCorner.Apply(imageToBeCropped));
            images.Add(bottomLeftCorner.Apply(imageToBeCropped));
            images.Add(bottomRightCorner.Apply(imageToBeCropped));

            return images;
        }


        public Bitmap ResizeImage()
        {
            ResizeNearestNeighbor filter = new ResizeNearestNeighbor(1000, 1500);
            try
            {
               
                Bitmap resizedImage = filter.Apply(BitmapToBeResized);
                return resizedImage;
            }
            catch (Exception)
            {
                Bitmap resizedImage = filter.Apply(GreyImage(BitmapToBeResized));
                return resizedImage;
            }
        }


        

        public string CompareLetter()
        {
            return "No Match could Be Found";
        }



       public Bitmap edgedetection(Bitmap n)
        {
            // create filter
            CannyEdgeDetector filter = new CannyEdgeDetector();
            // apply the filter
            filter.ApplyInPlace(n);

            return n;
        }
    }
}