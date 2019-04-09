using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math.Geometry;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace BrailleConversionApi.Classes
{
    public class CircleDetectionClass

    {

        private List<Bitmap> bitmapList { get; set; }

        public CircleDetectionClass(List<Bitmap> bitmapList)
        {
            this.bitmapList = bitmapList;
        }





        public List<bool> GetBoolList()
        {

          
            List<bool> boolList = new List<bool>();

            foreach (var i in bitmapList)
            {
                boolList.Add(getCirlces(i));
                Debug.WriteLine("The circle is there:" + getCirlces(i).ToString());
            }

            return boolList;
        }
        


        public bool getCirlces(Bitmap bitmap)
        {
            bool isCirclePresent = false;
            // locate objects using blob counter
            BlobCounter blobCounter = new BlobCounter();
            blobCounter.ProcessImage(bitmap);
            Blob[] blobs = blobCounter.GetObjectsInformation();
            // create Graphics object to draw on the image and a pen
            SimpleShapeChecker shapeChecker = new SimpleShapeChecker();

            shapeChecker.RelativeDistortionLimit = 00.03F;
            // check each object and draw circle around objects, which
            // are recognized as circles
            for (int i = 0, n = blobs.Length; i < n; i++)
            {

                

                List<IntPoint> edgePoints = blobCounter.GetBlobsEdgePoints(blobs[i]);

                AForge.Point center;
                float radius;

          
                if (shapeChecker.IsCircle(edgePoints))
                {
                   
                    isCirclePresent = true;
                }
            }




            return isCirclePresent;
        }


        Bitmap getLoctaionOfCircle(Bitmap bitmp, List<int> circleX, List<int> circle1Y, List<int> radius1, int count)
        {
            int leftMostItem = circleX[0];
            int highestDot = circle1Y[0];

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
            Debug.WriteLine("The highest point is " + highestDot);

            Debug. WriteLine("The LeftMost point is " + leftMostItem);

            int topPoint = (highestDot - radius1[0]) - 1;
            int leftMostPoint = (leftMostItem - (radius1[0]));
            int diameter = (radius1[0] * 3);

            Crop topLeftCorner = new Crop(new Rectangle(leftMostPoint, topPoint, diameter * 2, diameter * 4));
            Bitmap m = topLeftCorner.Apply(bitmp);



           
          
            return topLeftCorner.Apply(bitmp);



        }







    }
}