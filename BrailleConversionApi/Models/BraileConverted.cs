using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace BrailleConversionApi.Models
{
    public class BraileConverted
    {
        public BraileConverted()
        {
            CovertedBrailleLetter = "unknown";
        }

        public BraileConverted(String letter)
        {
            CovertedBrailleLetter = letter;
        }
        public string CovertedBrailleLetter {get; set;}

       
        
    }
}