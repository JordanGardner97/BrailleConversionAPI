using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace BrailleConversionApi.Models
{
    public class BraileConverted
    {
        
        public string CovertedBrailleLetter {get; set;}

       
        public Stream ImageToBeConverted { get; set; }
    }
}