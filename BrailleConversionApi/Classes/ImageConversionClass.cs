using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.IO;
using AForge.Imaging.Filters;
using System.Diagnostics;
using AForge.Math.Geometry;
using AForge.Imaging;
using AForge;

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

        public ImageCoversionClass(System.Drawing.Image image)
        {
            BitmapToBeGreyed = new Bitmap(image);
            BitmapToBeBlackened = new Bitmap(image);


        }

        public ImageCoversionClass()
        {
            

        }

        public Bitmap GreyImage(Bitmap bmp)
        {

            try
            {
                // create grayscale filter (BT709)
                Grayscale filter = new Grayscale(0.2125, 0.7154, 0.0721);
                // apply the filter
                Bitmap greyedImage = filter.Apply(bmp);
                return greyedImage;

            }

            catch(AForge.Imaging.UnsupportedImageFormatException)
            {
                return bmp;
            }


            
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





        public List<Bitmap> chopImageIntoSegments(Bitmap bitmap, int leftpoint, int highpoint, int radius)
        {
            List<Bitmap> m = new List<Bitmap>();

            Crop TopLeft = new Crop(new Rectangle((leftpoint - radius), (highpoint - radius), (radius * 3), (radius * 3) + 5));
            Crop TopRight = new Crop(new Rectangle((leftpoint + radius * 2), (highpoint - radius), (radius * 3), (radius * 3) + 5));


            Crop MidLeft = new Crop(new Rectangle((leftpoint - radius), (highpoint + radius), (radius * 3), (radius * 3) + 5));
            Crop MidRight = new Crop(new Rectangle((leftpoint + radius * 2), (highpoint + radius), (radius * 3), (radius * 3) + 5));


            Crop BottomLeft = new Crop(new Rectangle((leftpoint - radius), (highpoint + radius * 2), (radius * 3), (radius * 3) + 5));
            Crop BottomRight = new Crop(new Rectangle((leftpoint + radius * 2), (highpoint + radius * 2), (radius * 3), (radius * 3) + 5));

            m.Add(TopLeft.Apply(bitmap));
            m.Add(TopRight.Apply(bitmap));
            m.Add(MidLeft.Apply(bitmap));
            m.Add(MidRight.Apply(bitmap));
            m.Add(BottomLeft.Apply(bitmap));
            m.Add(BottomRight.Apply(bitmap));

            return m;
        }





      public  List<Bitmap> breakBigBitMapUp(Bitmap fullBmp)
        {

            int width = Convert.ToInt32(fullBmp.Width * 0.1111111111);
            int heightperSqaure = Convert.ToInt32(fullBmp.Height * 0.14285714);

            List<Bitmap> images = new List<Bitmap>();
            Crop firstLine1 = new Crop(new Rectangle(0, 0, width, (heightperSqaure) + 5));
            Crop firstLine2 = new Crop(new Rectangle(width, 0, width, (heightperSqaure) + 5));
            Crop firstLine3 = new Crop(new Rectangle(width * 2, 0, width, (heightperSqaure) + 5));
            Crop firstLine4 = new Crop(new Rectangle(width * 3, 0, width, (heightperSqaure) + 5));
            Crop firstLine5 = new Crop(new Rectangle(width * 4, 0, width, (heightperSqaure) + 5));
            Crop firstLine6 = new Crop(new Rectangle(width * 5, 0, width, (heightperSqaure) + 5));


            Bitmap m = firstLine1.Apply(fullBmp);
            Bitmap m1 = firstLine2.Apply(fullBmp);
            Bitmap m2 = firstLine3.Apply(fullBmp);
            Bitmap m3 = firstLine4.Apply(fullBmp);
            Bitmap m4 = firstLine5.Apply(fullBmp);
            Bitmap m5 = firstLine6.Apply(fullBmp);

            images.Add(m);
            images.Add(m1);
            images.Add(m2);
            images.Add(m3);
            images.Add(m4);
            images.Add(m5);





            //Crop SecondLine1 = new Crop(new Rectangle(0, 0, width, (heightperSqaure) + 5));
            //Crop SecondLine2 = new Crop(new Rectangle(0, 0, width, (heightperSqaure) + 5));
            //Crop SecondLine3 = new Crop(new Rectangle(0, 0, width, (heightperSqaure) + 5));
            //Crop SecondLine4 = new Crop(new Rectangle(0, 0, width, (heightperSqaure) + 5));
            //Crop SecondLine5 = new Crop(new Rectangle(0, 0, width, (heightperSqaure) + 5));
            //Crop SecondLine6 = new Crop(new Rectangle(0, 0, width, (heightperSqaure) + 5));

            //Crop thirdLine1 = new Crop(new Rectangle(0, 0, width, (heightperSqaure) + 5));
            //Crop thirdLine2 = new Crop(new Rectangle(0, 0, width, (heightperSqaure) + 5));
            //Crop thirdLine3 = new Crop(new Rectangle(0, 0, width, (heightperSqaure) + 5));
            //Crop thirdLine4 = new Crop(new Rectangle(0, 0, width, (heightperSqaure) + 5));
            //Crop thirdLine5 = new Crop(new Rectangle(0, 0, width, (heightperSqaure) + 5));
            //Crop thirdLine6 = new Crop(new Rectangle(0, 0, width, (heightperSqaure) + 5));

            //Crop fourthLine1 = new Crop(new Rectangle(0, 0, width, (heightperSqaure) + 5));
            //Crop fourthLine2 = new Crop(new Rectangle(0, 0, width, (heightperSqaure) + 5));
            //Crop fourthLine3 = new Crop(new Rectangle(0, 0, width, (heightperSqaure) + 5));
            //Crop fourthLine4 = new Crop(new Rectangle(0, 0, width, (heightperSqaure) + 5));
            //Crop fourthLine5 = new Crop(new Rectangle(0, 0, width, (heightperSqaure) + 5));
            //Crop fourthLine6 = new Crop(new Rectangle(0, 0, width, (heightperSqaure) + 5));

            //Crop fifthLine1 = new Crop(new Rectangle(0, 0, width, (heightperSqaure) + 5));
            //Crop fifthLine2 = new Crop(new Rectangle(0, 0, width, (heightperSqaure) + 5));
            //Crop fifthLine3 = new Crop(new Rectangle(0, 0, width, (heightperSqaure) + 5));
            //Crop fifthLine4 = new Crop(new Rectangle(0, 0, width, (heightperSqaure) + 5));
            //Crop fifthLine5 = new Crop(new Rectangle(0, 0, width, (heightperSqaure) + 5));
            //Crop fifthLine6 = new Crop(new Rectangle(0, 0, width, (heightperSqaure) + 5));


            // Bitmap mm =topLeftCorner.Apply(fullBmp);



            //Crop topRightCorner = new Crop(new Rectangle(width, 0, width, heightperSqaure + 5));
            //Crop leftMiddleCorner = new Crop(new Rectangle(0, heightperSqaure, width, heightperSqaure + 5));
            //Crop rightMiddleCorner = new Crop(new Rectangle(width, heightperSqaure, width, heightperSqaure + 5));
            //Crop bottomLeftCorner = new Crop(new Rectangle(0, heightperSqaure * 2, width, heightperSqaure + 5));
            //Crop bottomRightCorner = new Crop(new Rectangle(width, heightperSqaure * 2, width, heightperSqaure + 5));


            return images;
        }





        public Bitmap ResizeImage()
        {
            ResizeNearestNeighbor filter = new ResizeNearestNeighbor(1000, 1500);
            
               
                Bitmap resizedImage = filter.Apply(BitmapToBeResized);
                return resizedImage;
            
        }



       public List<int> GetLocationOfAllCircles(Bitmap bit)
        {
            Bitmap bits = edgedetection(bit);
            List<int> circleX = new List<int>();
            List<int> circleY = new List<int>();
            List<int> circleRadius = new List<int>();

            SimpleShapeChecker shapeChecker = new SimpleShapeChecker();
            
            BlobCounter blobCounter = new BlobCounter();
            blobCounter.ProcessImage(bits);
            Blob[] blobs = blobCounter.GetObjectsInformation();
       

            shapeChecker.RelativeDistortionLimit = 0.80f;

            for (int i = 0, n = blobs.Length; i < n; i++)
            {
                //Console.WriteLine("blob info is  " + blobs[i].CenterOfGravity);
                List<IntPoint> edgePoints = blobCounter.GetBlobsEdgePoints(blobs[i]);

                AForge.Point center;
                float radius;


                // Console.WriteLine("The count is" + edgePoints.Count);



                if (shapeChecker.IsCircle(edgePoints, out center, out radius))
                {
                    Console.WriteLine("hello " + i);
                    Console.WriteLine("The circle center is " + center);
                    Console.WriteLine("The radius is " + radius);


                    circleX.Add((int)center.X);
                    circleY.Add((int)center.Y);
                    circleRadius.Add((int)radius);


                }
            }

          return getLoctaionOfCircle(bits, circleX, circleY, circleRadius);


        }




        List<int> getLoctaionOfCircle(Bitmap bitmp, List<int> circleX, List<int> circle1Y, List<int> radius1)
        {
            int leftMostItem = circleX[0];
            int highestDot = circle1Y[0];
            int biggestRadius = radius1[0];

            foreach (var item in circleX)
            {
                if (item < leftMostItem)
                {
                    leftMostItem = item;
                }
            }

            foreach (var item in circle1Y)
            {
                if (item < highestDot)
                {
                    highestDot = item;
                }
            }


            foreach (var rad in radius1)
            {
                if (rad > biggestRadius)
                {
                    biggestRadius = rad;
                }


                Debug.WriteLine(rad);

            }

            List<int> LeftPointDetails = new List<int>()
            {
                leftMostItem,
                highestDot,
                biggestRadius
            };
            Debug.WriteLine("The highest point is " + highestDot);

            Debug.WriteLine("The LeftMost point is " + leftMostItem);


            Debug.WriteLine("The Biggest radius is " + biggestRadius);


          
            return LeftPointDetails;



        }




        public string CompareLetter()
        {
            return "No Match could Be Found";
        }



       public Bitmap edgedetection(Bitmap n)
        {
            try
            {
                Bitmap nn = GreyImage(n);
                CannyEdgeDetector filter = new CannyEdgeDetector();
                // apply the filter
                filter.ApplyInPlace(nn);
                Debug.WriteLine("havent a breeze ");
                return nn;
                
            }

            catch (AForge.Imaging.UnsupportedImageFormatException)
            {
                // create filter
                CannyEdgeDetector filter = new CannyEdgeDetector();
                // apply the filter
                filter.ApplyInPlace(n);
                Debug.WriteLine("havent a breeze 2");
                return n;
            }
        }







       public List<Bitmap> cropfromPhotedImage(Bitmap bitmap, int leftpoint, int highpoint, int radius)
        {
            List<Bitmap> m = new List<Bitmap>();

            Crop TopLeft = new Crop(new Rectangle((leftpoint - radius), (highpoint - radius), (radius * 6), (radius * 6) + 5));
            Crop TopRight = new Crop(new Rectangle((leftpoint + radius * 6), (highpoint - radius), (radius * 6), (radius * 6) + 5));


            Crop MidLeft = new Crop(new Rectangle((leftpoint - radius), (highpoint + radius * 6), (radius * 6), (radius * 6) + 5));
            Crop MidRight = new Crop(new Rectangle((leftpoint + radius * 6), (highpoint + radius * 6), (radius * 6), (radius * 6) + 5));


            Crop BottomLeft = new Crop(new Rectangle((leftpoint - radius), (highpoint + radius * 12), (radius * 6), (radius * 6) + 5));
            Crop BottomRight = new Crop(new Rectangle((leftpoint + radius * 6), (highpoint + radius * 12), (radius * 6), (radius * 6) + 5));

            m.Add(TopLeft.Apply(bitmap));
            m.Add(TopRight.Apply(bitmap));
            m.Add(MidLeft.Apply(bitmap));
            m.Add(MidRight.Apply(bitmap));
            m.Add(BottomLeft.Apply(bitmap));
            m.Add(BottomRight.Apply(bitmap));

            return m;

        }


    }
}