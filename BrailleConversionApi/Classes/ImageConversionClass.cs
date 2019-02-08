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

        public ImageCoversionClass(Bitmap image)
        {
            this.BitmapToBeGreyed = image;
            this.BitmapToBeBlackened = image;
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

        public string CompareLetter()
        {
            return "No Match could Be Found";
        }
    }
}