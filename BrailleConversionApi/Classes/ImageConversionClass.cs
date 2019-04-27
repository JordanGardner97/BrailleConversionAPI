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

            catch (AForge.Imaging.UnsupportedImageFormatException)
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
            Crop TopRight = new Crop(new Rectangle((leftpoint + radius ), (highpoint - radius), (radius * 3), (radius * 3) + 5));


            Crop MidLeft = new Crop(new Rectangle((leftpoint - radius), (highpoint + radius), (radius * 3), (radius * 3) + 5));
            Crop MidRight = new Crop(new Rectangle((leftpoint + radius * 2), (highpoint + radius), (radius * 3), (radius * 3) + 5));


            Crop BottomLeft = new Crop(new Rectangle((leftpoint - radius), (highpoint + radius * 4), (radius * 3), (radius * 3) + 5));
            Crop BottomRight = new Crop(new Rectangle((leftpoint + radius), (highpoint + radius * 4), (radius * 3), (radius * 3) + 5));

            m.Add(TopLeft.Apply(bitmap));
            m.Add(TopRight.Apply(bitmap));
            m.Add(MidLeft.Apply(bitmap));
            m.Add(MidRight.Apply(bitmap));
            m.Add(BottomLeft.Apply(bitmap));
            m.Add(BottomRight.Apply(bitmap));

            return m;
        }





        public List<Bitmap> breakBigBitMapUp(Bitmap fullBmp)
        {
            //9 accross seven down
            int width = Convert.ToInt32(fullBmp.Width * 0.1111111111);

            //7 down
            int heightperSqaure = Convert.ToInt32(fullBmp.Height * 0.14285714);

            //1st Row Start
            List<Bitmap> images = new List<Bitmap>();
            Crop firstLine1 = new Crop(new Rectangle(0, 0, width, (heightperSqaure) + 5));
            Crop firstLine2 = new Crop(new Rectangle(width, 0, width, (heightperSqaure) + 5));
            Crop firstLine3 = new Crop(new Rectangle(width * 2, 0, width, (heightperSqaure) + 5));
            Crop firstLine4 = new Crop(new Rectangle(width * 3, 0, width, (heightperSqaure) + 5));
            Crop firstLine5 = new Crop(new Rectangle(width * 4, 0, width, (heightperSqaure) + 5));
            Crop firstLine6 = new Crop(new Rectangle(width * 5, 0, width, (heightperSqaure) + 5));
            Crop firstLine7 = new Crop(new Rectangle(width * 6, 0, width, (heightperSqaure) + 5));
            Crop firstLine8 = new Crop(new Rectangle(width * 7, 0, width, (heightperSqaure) + 5));
            Crop firstLine9 = new Crop(new Rectangle(width * 8, 0, width, (heightperSqaure) + 5));



            Bitmap m = firstLine1.Apply(fullBmp);
            Bitmap m1 = firstLine2.Apply(fullBmp);
            Bitmap m2 = firstLine3.Apply(fullBmp);
            Bitmap m3 = firstLine4.Apply(fullBmp);
            Bitmap m4 = firstLine5.Apply(fullBmp);
            Bitmap m5 = firstLine6.Apply(fullBmp);
            Bitmap m6 = firstLine7.Apply(fullBmp);
            Bitmap m7 = firstLine8.Apply(fullBmp);
            Bitmap m8 = firstLine9.Apply(fullBmp);

            images.Add(m);
            images.Add(m1);
            images.Add(m2);
            images.Add(m3);
            images.Add(m4);
            images.Add(m5);
            images.Add(m6);
            images.Add(m7);
            images.Add(m8);

            //end of first row


            //Start of second row
            Crop SecondLine1 = new Crop(new Rectangle(0, heightperSqaure, width, (heightperSqaure) + 5));
            Crop SecondLine2 = new Crop(new Rectangle(width, heightperSqaure, width, (heightperSqaure) + 5));
            Crop SecondLine3 = new Crop(new Rectangle(width * 2, heightperSqaure, width, (heightperSqaure) + 5));
            Crop SecondLine4 = new Crop(new Rectangle(width * 3, heightperSqaure, width, (heightperSqaure) + 5));
            Crop SecondLine5 = new Crop(new Rectangle(width * 4, heightperSqaure, width, (heightperSqaure) + 5));
            Crop SecondLine6 = new Crop(new Rectangle(width * 5, heightperSqaure, width, (heightperSqaure) + 5));
            Crop SecondLine7 = new Crop(new Rectangle(width * 6, heightperSqaure, width, (heightperSqaure) + 5));
            Crop SecondLine8 = new Crop(new Rectangle(width * 7, heightperSqaure, width, (heightperSqaure) + 5));
            Crop SecondLine9 = new Crop(new Rectangle(width * 7, heightperSqaure, width, (heightperSqaure) + 5));


            Bitmap s = SecondLine1.Apply(fullBmp);
            Bitmap s1 = SecondLine2.Apply(fullBmp);
            Bitmap s2 = SecondLine3.Apply(fullBmp);
            Bitmap s3 = SecondLine4.Apply(fullBmp);
            Bitmap s4 = SecondLine5.Apply(fullBmp);
            Bitmap s5 = SecondLine6.Apply(fullBmp);
            Bitmap s6 = SecondLine7.Apply(fullBmp);
            Bitmap s7 = SecondLine8.Apply(fullBmp);
            Bitmap s8 = SecondLine9.Apply(fullBmp);



            images.Add(s);
            images.Add(s1);
            images.Add(s2);
            images.Add(s3);
            images.Add(s4);
            images.Add(s5);
            images.Add(s6);
            images.Add(s7);
            images.Add(s8);


            //end of the second row


            //Start of thrid row
            Crop ThridLine1 = new Crop(new Rectangle(0, heightperSqaure * 2, width, (heightperSqaure) + 5));
            Crop ThridLine2 = new Crop(new Rectangle(width, heightperSqaure * 2, width, (heightperSqaure) + 5));
            Crop ThridLine3 = new Crop(new Rectangle(width * 2, heightperSqaure * 2, width, (heightperSqaure) + 5));
            Crop ThridLine4 = new Crop(new Rectangle(width * 3, heightperSqaure * 2, width, (heightperSqaure) + 5));
            Crop ThridLine5 = new Crop(new Rectangle(width * 4, heightperSqaure * 2, width, (heightperSqaure) + 5));
            Crop ThridLine6 = new Crop(new Rectangle(width * 5, heightperSqaure * 2, width, (heightperSqaure) + 5));
            Crop ThridLine7 = new Crop(new Rectangle(width * 6, heightperSqaure * 2, width, (heightperSqaure) + 5));
            Crop ThridLine8 = new Crop(new Rectangle(width * 7, heightperSqaure * 2, width, (heightperSqaure) + 5));
            Crop ThridLine9 = new Crop(new Rectangle(width * 8, heightperSqaure * 2, width, (heightperSqaure) + 5));





            Bitmap t = ThridLine1.Apply(fullBmp);
            Bitmap t1 = ThridLine2.Apply(fullBmp);
            Bitmap t2 = ThridLine3.Apply(fullBmp);
            Bitmap t3 = ThridLine4.Apply(fullBmp);
            Bitmap t4 = ThridLine5.Apply(fullBmp);
            Bitmap t5 = ThridLine6.Apply(fullBmp);
            Bitmap t6 = ThridLine7.Apply(fullBmp);
            Bitmap t7 = ThridLine8.Apply(fullBmp);
            Bitmap t8 = ThridLine9.Apply(fullBmp);


            images.Add(t);
            images.Add(t1);
            images.Add(t2);
            images.Add(t3);
            images.Add(t4);
            images.Add(t5);
            images.Add(t6);
            images.Add(t7);
            images.Add(t8);



            //end of the thrid row

            //Start of fourth row
            Crop fourthLine1 = new Crop(new Rectangle(0, heightperSqaure * 3, width, (heightperSqaure) + 5));
            Crop fourthLine2 = new Crop(new Rectangle(width, heightperSqaure * 3, width, (heightperSqaure) + 5));
            Crop fourthLine3 = new Crop(new Rectangle(width * 2, heightperSqaure * 3, width, (heightperSqaure) + 5));
            Crop fourthLine4 = new Crop(new Rectangle(width * 3, heightperSqaure * 3, width, (heightperSqaure) + 5));
            Crop fourthLine5 = new Crop(new Rectangle(width * 4, heightperSqaure * 3, width, (heightperSqaure) + 5));
            Crop fourthLine6 = new Crop(new Rectangle(width * 5, heightperSqaure * 3, width, (heightperSqaure) + 5));
            Crop fourthLine7 = new Crop(new Rectangle(width * 6, heightperSqaure * 3, width, (heightperSqaure) + 5));
            Crop fourthLine8 = new Crop(new Rectangle(width * 7, heightperSqaure * 3, width, (heightperSqaure) + 5));
            Crop fourthLine9 = new Crop(new Rectangle(width * 8, heightperSqaure * 3, width, (heightperSqaure) + 5));





            Bitmap f = fourthLine1.Apply(fullBmp);
            Bitmap f1 = fourthLine2.Apply(fullBmp);
            Bitmap f2 = fourthLine3.Apply(fullBmp);
            Bitmap f3 = fourthLine4.Apply(fullBmp);
            Bitmap f4 = fourthLine5.Apply(fullBmp);
            Bitmap f5 = fourthLine6.Apply(fullBmp);
            Bitmap f6 = fourthLine7.Apply(fullBmp);
            Bitmap f7 = fourthLine8.Apply(fullBmp);
            Bitmap f8 = fourthLine9.Apply(fullBmp);


            images.Add(f);
            images.Add(f1);
            images.Add(f2);
            images.Add(f3);
            images.Add(f4);
            images.Add(f5);
            images.Add(f6);
            images.Add(f7);
            images.Add(f8);



            //end of the fourth row



            //Start of fifth row
            Crop FifthLine1 = new Crop(new Rectangle(0, heightperSqaure * 4, width, (heightperSqaure) + 5));
            Crop FifthLine2 = new Crop(new Rectangle(width, heightperSqaure * 4, width, (heightperSqaure) + 5));
            Crop FifthLine3 = new Crop(new Rectangle(width * 2, heightperSqaure * 4, width, (heightperSqaure) + 5));
            Crop FifthLine4 = new Crop(new Rectangle(width * 3, heightperSqaure * 4, width, (heightperSqaure) + 5));
            Crop FifthLine5 = new Crop(new Rectangle(width * 4, heightperSqaure * 4, width, (heightperSqaure) + 5));
            Crop FifthLine6 = new Crop(new Rectangle(width * 5, heightperSqaure * 4, width, (heightperSqaure) + 5));
            Crop FifthLine7 = new Crop(new Rectangle(width * 6, heightperSqaure * 4, width, (heightperSqaure) + 5));
            Crop FifthLine8 = new Crop(new Rectangle(width * 7, heightperSqaure * 4, width, (heightperSqaure) + 5));
            Crop FifthLine9 = new Crop(new Rectangle(width * 8, heightperSqaure * 4, width, (heightperSqaure) + 5));





            Bitmap fi = FifthLine1.Apply(fullBmp);
            Bitmap fi1 = FifthLine2.Apply(fullBmp);
            Bitmap fi2 = FifthLine3.Apply(fullBmp);
            Bitmap fi3 = FifthLine4.Apply(fullBmp);
            Bitmap fi4 = FifthLine5.Apply(fullBmp);
            Bitmap fi5 = FifthLine6.Apply(fullBmp);
            Bitmap fi6 = FifthLine7.Apply(fullBmp);
            Bitmap fi7 = FifthLine8.Apply(fullBmp);
            Bitmap fi8 = FifthLine9.Apply(fullBmp);


            images.Add(fi);
            images.Add(fi1);
            images.Add(fi2);
            images.Add(fi3);
            images.Add(fi4);
            images.Add(fi5);
            images.Add(fi6);
            images.Add(fi7);
            images.Add(fi8);



            //end of the Fifth row



            //Start of Sixth row
            Crop SixthLine1 = new Crop(new Rectangle(0, heightperSqaure * 5, width, (heightperSqaure) + 5));
            Crop SixthLine2 = new Crop(new Rectangle(width, heightperSqaure * 5, width, (heightperSqaure) + 5));
            Crop SixthLine3 = new Crop(new Rectangle(width * 2, heightperSqaure * 5, width, (heightperSqaure) + 5));
            Crop SixthLine4 = new Crop(new Rectangle(width * 3, heightperSqaure * 5, width, (heightperSqaure) + 5));
            Crop SixthLine5 = new Crop(new Rectangle(width * 4, heightperSqaure * 5, width, (heightperSqaure) + 5));
            Crop SixthLine6 = new Crop(new Rectangle(width * 5, heightperSqaure * 5, width, (heightperSqaure) + 5));
            Crop SixthLine7 = new Crop(new Rectangle(width * 6, heightperSqaure * 5, width, (heightperSqaure) + 5));
            Crop SixthLine8 = new Crop(new Rectangle(width * 7, heightperSqaure * 5, width, (heightperSqaure) + 5));
            Crop SixthLine9 = new Crop(new Rectangle(width * 8, heightperSqaure * 5, width, (heightperSqaure) + 5));





            Bitmap Si = SixthLine1.Apply(fullBmp);
            Bitmap Si_1 = SixthLine2.Apply(fullBmp);
            Bitmap Si_2 = SixthLine3.Apply(fullBmp);
            Bitmap Si_3 = SixthLine4.Apply(fullBmp);
            Bitmap Si_4 = SixthLine5.Apply(fullBmp);
            Bitmap Si_5 = SixthLine6.Apply(fullBmp);
            Bitmap Si_6 = SixthLine7.Apply(fullBmp);
            Bitmap Si_7 = SixthLine8.Apply(fullBmp);
            Bitmap Si_8 = SixthLine9.Apply(fullBmp);


            images.Add(Si);
            images.Add(Si_1);
            images.Add(Si_2);
            images.Add(Si_3);
            images.Add(Si_4);
            images.Add(Si_5);
            images.Add(Si_6);
            images.Add(Si_7);
            images.Add(Si_8);



            //end of the Sixth row


            //Start of Seven row
            Crop SevenLine1 = new Crop(new Rectangle(0, heightperSqaure * 6, width, (heightperSqaure) + 5));
            Crop SevenLine2 = new Crop(new Rectangle(width, heightperSqaure * 6, width, (heightperSqaure) + 5));
            Crop SevenLine3 = new Crop(new Rectangle(width * 2, heightperSqaure * 6, width, (heightperSqaure) + 5));
            Crop SevenLine4 = new Crop(new Rectangle(width * 3, heightperSqaure * 6, width, (heightperSqaure) + 5));
            Crop SevenLine5 = new Crop(new Rectangle(width * 4, heightperSqaure * 6, width, (heightperSqaure) + 5));
            Crop SevenLine6 = new Crop(new Rectangle(width * 5, heightperSqaure * 6, width, (heightperSqaure) + 5));
            Crop SevenLine7 = new Crop(new Rectangle(width * 6, heightperSqaure * 6, width, (heightperSqaure) + 5));
            Crop SevenLine8 = new Crop(new Rectangle(width * 7, heightperSqaure * 6, width, (heightperSqaure) + 5));
            Crop SevenLine9 = new Crop(new Rectangle(width * 8, heightperSqaure * 6, width, (heightperSqaure) + 5));





            Bitmap Se = SevenLine1.Apply(fullBmp);
            Bitmap Se_1 = SevenLine2.Apply(fullBmp);
            Bitmap Se_2 = SevenLine3.Apply(fullBmp);
            Bitmap Se_3 = SevenLine4.Apply(fullBmp);
            Bitmap Se_4 = SevenLine5.Apply(fullBmp);
            Bitmap Se_5 = SevenLine6.Apply(fullBmp);
            Bitmap Se_6 = SevenLine7.Apply(fullBmp);
            Bitmap Se_7 = SevenLine8.Apply(fullBmp);
            Bitmap Se_8 = SevenLine9.Apply(fullBmp);


            images.Add(Se);
            images.Add(Se_1);
            images.Add(Se_2);
            images.Add(Se_3);
            images.Add(Se_4);
            images.Add(Se_5);
            images.Add(Se_6);
            images.Add(Se_7);
            images.Add(Se_8);



            //end of the Sixth row




            return images;
        }





        public Bitmap ResizeImage()
        {
            ResizeNearestNeighbor filter = new ResizeNearestNeighbor(BitmapToBeResized.Width * 5, BitmapToBeResized.Height * 5);


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


            shapeChecker.RelativeDistortionLimit = 0.03f;

            for (int i = 0, n = blobs.Length; i < n; i++)
            {

                //Console.WriteLine("blob info is  " + blobs[i].CenterOfGravity);


                List<IntPoint> edgePoints = blobCounter.GetBlobsEdgePoints(blobs[i]);

                AForge.Point center;
                float radius;


                // Console.WriteLine("The count is" + edgePoints.Count);



                if (shapeChecker.IsCircle(edgePoints, out center, out radius))
                {



                    circleX.Add((int)center.X);
                    circleY.Add((int)center.Y);
                    circleRadius.Add((int)radius);


                }
            }

            try
            {

                

                return getLoctaionOfCircle(bits, circleX, circleY, circleRadius);
            }


            catch (ArgumentOutOfRangeException)
            {


                circleX.Add(0);
                circleY.Add(0);
                circleRadius.Add(0);

                return getLoctaionOfCircle(bits, circleX, circleY, circleRadius);
            }




        }




        List<int> getLoctaionOfCircle(Bitmap bitmp, List<int> circleX, List<int> circle1Y, List<int> radius1)
        {
            try
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


                    //Debug.WriteLine(rad);

                }

                Debug.WriteLine("\n");

                Debug.WriteLine("leftMostItem:" + leftMostItem );

                Debug.WriteLine("highestDot:" + highestDot );

                Debug.WriteLine("biggestRadius:" + biggestRadius);


                List<int> LeftPointDetails = new List<int>()
            {
                leftMostItem,
                highestDot,
                biggestRadius
            };
                //Debug.WriteLine("The highest point is " + highestDot);

                //Debug.WriteLine("The LeftMost point is " + leftMostItem);


                //Debug.WriteLine("The Biggest radius is " + biggestRadius);



                return LeftPointDetails;

            }


            catch (ArgumentOutOfRangeException ex)
            {


                int leftMostItem = 200;
                int highestDot = 200;
                int biggestRadius = 50;





                List<int> LeftPointDetails = new List<int>()
            {
                leftMostItem,
                highestDot,
                biggestRadius
            };
                //Debug.WriteLine("The highest point is " + highestDot);

                //Debug.WriteLine("The LeftMost point is " + leftMostItem);


                //Debug.WriteLine("The Biggest radius is " + biggestRadius);



                return LeftPointDetails;

            }

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
             
                return nn;

            }

            catch (AForge.Imaging.UnsupportedImageFormatException)
            {
                // create filter
                CannyEdgeDetector filter = new CannyEdgeDetector();
                // apply the filter
                filter.ApplyInPlace(n);
       
                return n;
            }
        }







        public List<Bitmap> cropfromPhotedImage(Bitmap bitmap, int leftpoint, int highpoint, int radius)
        {
            List<Bitmap> m = new List<Bitmap>();

            Crop TopLeft = new Crop(new Rectangle((leftpoint - radius), (highpoint - radius), (radius * 3), (radius * 3) + 5));
            Crop TopRight = new Crop(new Rectangle((leftpoint + radius), (highpoint - radius), (radius * 3), (radius * 3) + 5));


            Crop MidLeft = new Crop(new Rectangle((leftpoint - radius), (highpoint + radius), (radius * 3), (radius * 3) + 5));
            Crop MidRight = new Crop(new Rectangle((leftpoint + radius), (highpoint + radius), (radius * 3), (radius * 3) + 5));


            Crop BottomLeft = new Crop(new Rectangle((leftpoint - radius), (highpoint + radius * 6), (radius * 3), (radius * 3) + 5));
            Crop BottomRight = new Crop(new Rectangle((leftpoint + radius), (highpoint + radius * 6), (radius * 3), (radius * 3) + 5));

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