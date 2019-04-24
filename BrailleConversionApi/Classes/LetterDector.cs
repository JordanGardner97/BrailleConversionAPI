using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrailleConversionApi.Classes
{
    public class LetterDector
    {

        public List<bool> CircleThere { get; set; }

        public LetterDector(List<bool> circleThere)
        {
            this.CircleThere = circleThere;
        }


        public string checkLetter()
        {

            if (LetterA())
            {
                return "A";
            }


            else if (LetterB())
            {
                return "B";
            }

            else if (LetterC())
            {
                return "C";
            }

            else if (LetterD())
            {
                return "D";
            }

            else if (LetterE())
            {
                return "E";
            }

            else if (LetterF())
            {
                return "F";
            }

            else if (LetterG())
            {
                return "G";
            }

            else if (LetterH())
            {
                return "H";
            }

            else if (LetterI())
            {
                return "I";
            }

            else if (LetterJ())
            {
                return "J";
            }

            else if (LetterK())
            {
                return "K";
            }

            else if (LetterL())
            {
                return "L";
            }


            else if (LetterM())
            {
                return "M";
            }

            else if (LetterN())
            {
                return "N";
            }


            else if (LetterO())
            {
                return "O";
            }

            else if (LetterP())
            {
                return "P";
            }

            else if (LetterQ())
            {
                return "Q";
            }

            else if (LetterR())
            {
                return "R";
            }

            else if (LetterS())
            {
                return "S";
            }

            else if (LetterT())
            {
                return "T";
            }

            else if (LetterU())
            {
                return "U";
            }

            else if (LetterV())
            {
                return "V";
            }

            else if (LetterW())
            {
                return "W";
            }

            else if (LetterX())
            {
                return "X";
            }

            else if (LetterY())
            {
                return "Y";
            }

            else if (LetterZ())
            {
                return "Z";
            }


            else if (Space())
            {
                return " ";
            }

            else
            {
                return "Unknown";
            }


        }









        //1 Letter A
        private bool LetterA()
        {
            // topLeft Y          topRight N          midLeft N          midRight N         bottomLeft N       bottomRight N
            if (CircleThere[0] && !CircleThere[1] && !CircleThere[2] && !CircleThere[3] && !CircleThere[4] && !CircleThere[5])
            {
                return true;
            }

            else
            {
                return false;
            }

        }



        //2 Letter B
        private bool LetterB()
        {
            // topLeft Y          topRight N         midLeft Y         midRight N         bottomLeft N       bottomRight N
            if (CircleThere[0] && !CircleThere[1] && CircleThere[2] && !CircleThere[3] && !CircleThere[4] && !CircleThere[5])
            {
                return true;
            }

            else
            {
                return false;
            }

        }

        //3 Letter C
        private bool LetterC()
        {
            //  topLeft Y          topRight Y         midLeft N         midRight N         bottomLeft N       bottomRight N
            if (CircleThere[0] && CircleThere[1] && !CircleThere[2] && !CircleThere[3] && !CircleThere[4] && !CircleThere[5])
            {
                return true;
            }

            else
            {
                return false;
            }

        }


        //4 Letter D
        private bool LetterD()
        {
             
            if (CircleThere[0] && CircleThere[1] && !CircleThere[2] && CircleThere[3] && !CircleThere[4] && !CircleThere[5])
            {
                return true;
            }

            else
            {
                return false;
            }

        }




        //5 Letter E
        private bool LetterE()
        {
               //topLeft  Y         topRight N         midLeft N        midRight Y         bottomLeft  N      bottomRight N
            if (CircleThere[0] && !CircleThere[1] && !CircleThere[2] && CircleThere[3] && !CircleThere[4] && !CircleThere[5])
            {
                return true;
            }

            else
            {
                return false;
            }

        }



        //6 Letter F
        private bool LetterF()
        {
              //topLeft           topRight          midLeft            midRight           bottomLeft         bottomRight
            if (CircleThere[0] && CircleThere[1] && CircleThere[2] && !CircleThere[3] && !CircleThere[4] && !CircleThere[5])
            {
                return true;
            }

            else
            {
                return false;
            }

        }


        //7 Letter G
        private bool LetterG()
        {
               //topLeft          topRight          midLeft           midRight          bottomLeft        bottomRight
            if (CircleThere[0] && CircleThere[1] && CircleThere[2] && CircleThere[3] && !CircleThere[4] && !CircleThere[5])
            {
                return true;
            }

            else
            {
                return false;
            }

        }


        //8 Letter H
        private bool LetterH()
        {
               //topLeft           topRight          midLeft           midRight          bottomLeft        bottomRight
            if (CircleThere[0] && !CircleThere[1] && CircleThere[2] && CircleThere[3] && !CircleThere[4] && !CircleThere[5])
            {
                return true;
            }

            else
            {
                return false;
            }

        }



        //9 Letter I
        private bool LetterI()
        {
               //topLeft          topRight          midLeft           midRight          bottomLeft        bottomRight
            if (!CircleThere[0] && CircleThere[1] && CircleThere[2] && !CircleThere[3] && !CircleThere[4] && !CircleThere[5])
            {
                return true;
            }

            else
            {
                return false;
            }

        }


        //10 Letter J
        private bool LetterJ()
        {
              //topLeft           topRight          midLeft           midRight          bottomLeft        bottomRight
            if (!CircleThere[0] && CircleThere[1] && CircleThere[2] && CircleThere[3] && !CircleThere[4] && !CircleThere[5])
            {
                return true;
            }

            else
            {
                return false;
            }

        }

        //11 Letter K
        private bool LetterK()
        {
              //topLeft           topRight          midLeft           midRight          bottomLeft        bottomRight
            if (CircleThere[0] && !CircleThere[1] && !CircleThere[2] && !CircleThere[3] && CircleThere[4] && !CircleThere[5])
            {
                return true;
            }

            else
            {
                return false;
            }

        }



        //12 Letter L
        private bool LetterL()
        {
               //topLeft           topRight          midLeft            midRight          bottomLeft        bottomRight
            if (CircleThere[0] && !CircleThere[1] && CircleThere[2] && !CircleThere[3] && CircleThere[4] && !CircleThere[5])
            {
                return true;
            }

            else
            {
                return false;
            }

        }




        //13 Letter M
        private bool LetterM()
        {
              //topLeft           topRight          midLeft            midRight          bottomLeft        bottomRight
            if (CircleThere[0] && CircleThere[1] && !CircleThere[2] && !CircleThere[3] && CircleThere[4] && !CircleThere[5])
            {
                return true;
            }

            else
            {
                return false;
            }

        }

        //14 Letter N
        private bool LetterN()
        {
              //topLeft           topRight          midLeft            midRight          bottomLeft        bottomRight
            if (CircleThere[0] && CircleThere[1] && !CircleThere[2] && CircleThere[3] && CircleThere[4] && !CircleThere[5])
            {
                return true;
            }

            else
            {
                return false;
            }

        }

        //15 Letter O
        private bool LetterO()
        {
               //topLeft           topRight          midLeft            midRight          bottomLeft        bottomRight
            if (CircleThere[0] && !CircleThere[1] && !CircleThere[2] && CircleThere[3] && CircleThere[4] && !CircleThere[5])
            {
                return true;
            }

            else
            {
                return false;
            }

        }

        //16 Letter P
        private bool LetterP()
        {
              //topLeft           topRight          midLeft            midRight          bottomLeft        bottomRight
            if (CircleThere[0] && CircleThere[1] && CircleThere[2] && !CircleThere[3] && CircleThere[4] && !CircleThere[5])
            {
                return true;
            }

            else
            {
                return false;
            }

        }

        //17 Letter Q
        private bool LetterQ()
        {
              //topLeft           topRight          midLeft            midRight          bottomLeft        bottomRight
            if (CircleThere[0] && CircleThere[1] && CircleThere[2] && CircleThere[3] && CircleThere[4] && !CircleThere[5])
            {
                return true;
            }

            else
            {
                return false;
            }

        }

        //18 Letter R
        private bool LetterR()
        {
              //topLeft           topRight          midLeft            midRight         bottomLeft        bottomRight
            if (CircleThere[0] && !CircleThere[1] && CircleThere[2] && CircleThere[3] && CircleThere[4] && !CircleThere[5])
            {
                return true;
            }

            else
            {
                return false;
            }

        }

        //19 Letter S
        private bool LetterS()
        {
               //topLeft           topRight          midLeft            midRight          bottomLeft        bottomRight
            if (!CircleThere[0] && CircleThere[1] && !CircleThere[2] && CircleThere[3] && CircleThere[4] && !CircleThere[5])
            {
                return true;
            }

            else
            {
                return false;
            }

        }

        //20 Letter T
        private bool LetterT()
        {
              //topLeft           topRight          midLeft            midRight          bottomLeft        bottomRight
            if (!CircleThere[0] && CircleThere[1] && CircleThere[2] && CircleThere[3] && CircleThere[4] && !CircleThere[5])
            {
                return true;
            }

            else
            {
                return false;
            }

        }

        //21 Letter U
        private bool LetterU()
        {
              //topLeft             topRight          midLeft            midRight          bottomLeft        bottomRight
            if (CircleThere[0] && !CircleThere[1] && !CircleThere[2] && !CircleThere[3] && CircleThere[4] && CircleThere[5])
            {
                return true;
            }

            else
            {
                return false;
            }

        }

        //22 Letter V
        private bool LetterV()
        {
              //topLeft           topRight          midLeft            midRight          bottomLeft        bottomRight
            if (CircleThere[0] && !CircleThere[1] && CircleThere[2] && !CircleThere[3] && CircleThere[4] && CircleThere[5])
            {
                return true;
            }

            else
            {
                return false;
            }

        }


        //23 Letter W
        private bool LetterW()
        {
               //topLeft           topRight          midLeft            midRight          bottomLeft        bottomRight
            if (!CircleThere[0] && CircleThere[1] && CircleThere[2] && CircleThere[3] && !CircleThere[4] && CircleThere[5])
            {
                return true;
            }

            else
            {
                return false;
            }

        }

        //24 Letter X
        private bool LetterX()
        {
              //topLeft           topRight          midLeft            midRight          bottomLeft        bottomRight
            if (CircleThere[0] && CircleThere[1] && !CircleThere[2] && !CircleThere[3] && CircleThere[4] && CircleThere[5])
            {
                return true;
            }

            else
            {
                return false;
            }

        }

        //25 Letter Y
        private bool LetterY()
        {
               //topLeft           topRight          midLeft            midRight          bottomLeft        bottomRight
            if (CircleThere[0] && CircleThere[1] && !CircleThere[2] && CircleThere[3] && CircleThere[4] && CircleThere[5])
            {
                return true;
            }

            else
            {
                return false;
            }

        }

        //26 Letter Z
        private bool LetterZ()
        {
              //topLeft           topRight          midLeft            midRight          bottomLeft        bottomRight
            if (CircleThere[0] && !CircleThere[1] && !CircleThere[2] && CircleThere[3] && CircleThere[4] && CircleThere[5])
            {
                return true;
            }

            else
            {
                return false;
            }

        }

        private bool Space()
        {
            //topLeft           topRight          midLeft            midRight          bottomLeft        bottomRight
            if (!CircleThere[0] && !CircleThere[1] && !CircleThere[2] && !CircleThere[3] && !CircleThere[4] && !CircleThere[5])
            {
                return true;
            }

            else
            {
                return false;
            }

        }






    }
}