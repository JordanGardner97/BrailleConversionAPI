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



            Debug.WriteLine("I have been cropped");


            int width = Convert.ToInt32(imageToBeCropped.Width * 0.5);
            int heightperSqaure = Convert.ToInt32(imageToBeCropped.Height * 0.3333);

            List<Bitmap> images = new List<Bitmap>();
            Crop topLeftCorner = new Crop(new Rectangle(0, 0, width, heightperSqaure + 5));
            Crop topRightCorner = new Crop(new Rectangle(width, 0, width, heightperSqaure + 5));
            Crop leftMiddleCorner = new Crop(new Rectangle(0, heightperSqaure, width, heightperSqaure + 5));
            Crop rightMiddleCorner = new Crop(new Rectangle(width, heightperSqaure, width, heightperSqaure + 5));
            Crop bottomLeftCorner = new Crop(new Rectangle(0, heightperSqaure * 2, width, heightperSqaure + 5));
            Crop bottomRightCorner = new Crop(new Rectangle(width, heightperSqaure * 2, width, heightperSqaure + 5));


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
            
               
                Bitmap resizedImage = filter.Apply(BitmapToBeResized);
                return resizedImage;
            
        }


        

        public string CompareLetter()
        {
            return "No Match could Be Found";
        }



       public Bitmap edgedetection(Bitmap n)
        {
            Bitmap greyBitmap = GreyImage(n);
            Debug.WriteLine("Strating edge dectection filter");
            // create filter
            Debug.WriteLine("Middle ofedge dectection filter");
            CannyEdgeDetector filter = new CannyEdgeDetector();
            // apply the filter
            Debug.WriteLine("Third ofedge dectection filter");
            filter.ApplyInPlace(greyBitmap);
            Debug.WriteLine("forth ofedge dectection filter");

            return greyBitmap;
        }
    }
}