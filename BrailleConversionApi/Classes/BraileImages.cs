using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Reflection;
using System.IO;

namespace BrailleConversionApi.Classes
{
    public class BraileImages
    {
        





        public List<Bitmap> BitmapList
        {
            get
            {
                return LettersImageList;
            }
        }







        private List<Bitmap> LettersImageList = new List<Bitmap>()
        {
            Resource1.LetterA,
            Resource1.LetterB,
            Resource1.LetterC,
            Resource1.LetterD,
            Resource1.LetterE,
            Resource1.LetterF,
            Resource1.LetterG,
            Resource1.LetterH,
            Resource1.LetterI,
            Resource1.LetterJ,
            Resource1.LetterK,
            Resource1.LetterL,
            Resource1.LetterM,
            Resource1.LetterN,
            Resource1.LetterO,
            Resource1.LetterP,
            Resource1.LetterQ,
            Resource1.LetterR,
            Resource1.LetterS,
            Resource1.LetterT,
            Resource1.LetterU,
            Resource1.LetterV,
            Resource1.LetterW,
            Resource1.LetterX,
            Resource1.LetterY,
            Resource1.LetterZ


        };
    }
}