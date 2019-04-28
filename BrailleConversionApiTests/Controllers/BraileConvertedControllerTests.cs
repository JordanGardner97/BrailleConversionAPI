using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrailleConversionApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using BrailleConversionApi.Classes;
using BrailleConversionApi.Models;

namespace BrailleConversionApi.Controllers.Tests
{
    [TestClass()]
    public class BraileConvertedControllerTests
    {
      

        [TestMethod()]
        public void PostTest()
        {

            BraileImages  bmpl = new BraileImages();
            List<Bitmap> bmp = bmpl.BitmapList;


            Bitmap bmps = new Bitmap(bmp[0]);

            ImageCoversionClass normalPicture = new ImageCoversionClass(bmps);



            List<Bitmap> croppedPhotos = normalPicture.cropImageIntoSegments(normalPicture.edgedetection(bmps));

            CircleDetectionClass circlesDection = new CircleDetectionClass(croppedPhotos);

            List<bool> circleThereList = circlesDection.GetBoolList();

            LetterDector dectoring = new LetterDector(circleThereList);

            

            String Letter =  dectoring.checkLetter();

            Assert.AreEqual("A", Letter);


        }



        [TestMethod()]
        public void PostTestB()
        {

            BraileImages bmpl = new BraileImages();
            List<Bitmap> bmp = bmpl.BitmapList;


            Bitmap bmps = new Bitmap(bmp[1]);

            ImageCoversionClass normalPicture = new ImageCoversionClass(bmps);



            List<Bitmap> croppedPhotos = normalPicture.cropImageIntoSegments(normalPicture.edgedetection(bmps));

            CircleDetectionClass circlesDection = new CircleDetectionClass(croppedPhotos);

            List<bool> circleThereList = circlesDection.GetBoolList();

            LetterDector dectoring = new LetterDector(circleThereList);



            String Letter = dectoring.checkLetter();

            Assert.AreEqual("B", Letter);


        }











        [TestMethod()]
        public void PostTestC()
        {

            BraileImages bmpl = new BraileImages();
            List<Bitmap> bmp = bmpl.BitmapList;


            Bitmap bmps = new Bitmap(bmp[2]);

            ImageCoversionClass normalPicture = new ImageCoversionClass(bmps);



            List<Bitmap> croppedPhotos = normalPicture.cropImageIntoSegments(normalPicture.edgedetection(bmps));

            CircleDetectionClass circlesDection = new CircleDetectionClass(croppedPhotos);

            List<bool> circleThereList = circlesDection.GetBoolList();

            LetterDector dectoring = new LetterDector(circleThereList);



            String Letter = dectoring.checkLetter();

            Assert.AreEqual("C", Letter);


        }





        [TestMethod()]
        public void PostTestD()
        {

            BraileImages bmpl = new BraileImages();
            List<Bitmap> bmp = bmpl.BitmapList;


            Bitmap bmps = new Bitmap(bmp[1]);

            ImageCoversionClass normalPicture = new ImageCoversionClass(bmps);



            List<Bitmap> croppedPhotos = normalPicture.cropImageIntoSegments(normalPicture.edgedetection(bmps));

            CircleDetectionClass circlesDection = new CircleDetectionClass(croppedPhotos);

            List<bool> circleThereList = circlesDection.GetBoolList();

            LetterDector dectoring = new LetterDector(circleThereList);



            String Letter = dectoring.checkLetter();

           
            Assert.AreNotEqual("D", Letter);

        }




        [TestMethod()]
        public void PostTestE()
        {

            BraileImages bmpl = new BraileImages();
            List<Bitmap> bmp = bmpl.BitmapList;


            Bitmap bmps = new Bitmap(bmp[4]);

            ImageCoversionClass normalPicture = new ImageCoversionClass(bmps);



            List<Bitmap> croppedPhotos = normalPicture.cropImageIntoSegments(normalPicture.edgedetection(bmps));

            CircleDetectionClass circlesDection = new CircleDetectionClass(croppedPhotos);

            List<bool> circleThereList = circlesDection.GetBoolList();

            LetterDector dectoring = new LetterDector(circleThereList);



            String Letter = dectoring.checkLetter();


            Assert.AreNotEqual("A", Letter);

        }












    }
}