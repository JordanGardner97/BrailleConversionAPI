using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace BrailleConversionApi.Classes
{
    public class SimilarityClass
    {
        private float similar = 0f;
        private int letterCount = 0;
        private int count = 0;
        const float similarityThreshold = 0.50f;


        ExhaustiveTemplateMatching exhaustiveTemplateMatching = new ExhaustiveTemplateMatching(similarityThreshold);
        int ListCount = 0;
        BraileImages pictures = new BraileImages();



        List<string> Letters = new List<string>
        {
            "A", "B", "C", "D", "E", "F", "G", "H",
            "I", "J", "K", "L", "M", "N", "O", "P",
            "Q", "R", "S", "T", "U", "V", "W", "X",
            "Y", "Z"
        };

        public string GetLetter(Bitmap bmp)
        {
            List<Bitmap> brailePictures = pictures.BitmapList;
            try
            {
                foreach (var i in brailePictures)
                {
                    Size size = GetSize(i.Size, bmp.Size);
                    ResizeBilinear filter = new ResizeBilinear(size.Width, size.Height);
                    bmp = filter.Apply(bmp);
                    TemplateMatch[] matches = exhaustiveTemplateMatching.ProcessImage(i, bmp);
                    float sim = matches[0].Similarity;

                    if (sim >= similar)
                    {
                        similar = sim;
                        letterCount = count;

                    }

                    count++;
                    Debug.WriteLine("The count is" + count);
                }
               

                return Letters[letterCount];
            }
            catch(IndexOutOfRangeException )
            {
                Debug.WriteLine("The letter could not be Identified");
                return "Unknown";
            }


        }


        //funtion to check the image against stored

        public String PresentDotsInPhoto(List<Bitmap> brailePictureList)
        {
            List<Bitmap> brailePictures = pictures.BitmapList;
            ImageCoversionClass spliter = new ImageCoversionClass();

            List<Bitmap> bmpToCompare =  spliter.cropImageIntoSegments(brailePictures[0]);

            

            foreach (var splitImage in brailePictureList)
            {


            }

            return "";
        }



        private static Size GetSize(Size maxSize, Size size)
        {
            double ratioWidth = (double)maxSize.Width / size.Width;
            double ratioHeight = (double)maxSize.Height / size.Height;
            double ratio = Math.Min(ratioWidth, ratioHeight);
            return new Size((int)Math.Floor(size.Width * ratio), (int)Math.Floor(size.Height * ratio));

        }
    }
}