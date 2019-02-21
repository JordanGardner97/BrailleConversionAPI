using AForge;
using AForge.Imaging;
using AForge.Math.Geometry;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Web;

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



        private bool getCirlces(Bitmap bitmap)
        {
            bool isCirclePresent = false;
            // locate objects using blob counter
            BlobCounter blobCounter = new BlobCounter();
            blobCounter.ProcessImage(bitmap);
            Blob[] blobs = blobCounter.GetObjectsInformation();
            // create Graphics object to draw on the image and a pen


            // check each object and draw circle around objects, which
            // are recognized as circles
            for (int i = 0, n = blobs.Length; i < n; i++)
            {
                
                List<IntPoint> edgePoints = blobCounter.GetBlobsEdgePoints(blobs[i]);

                AForge.Point center;
                float radius;
                SimpleShapeChecker shapeChecker = new SimpleShapeChecker();

                if (shapeChecker.IsCircle(edgePoints, out center, out radius))
                {
                    
                    isCirclePresent = true;
                }
            }


          

            return isCirclePresent;
        }
    }
}