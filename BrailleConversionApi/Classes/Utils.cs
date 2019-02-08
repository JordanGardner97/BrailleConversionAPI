using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace BrailleConversionApi
{
    public class Utils
    {
        public Utils()
        {

        }
        public byte[] ReadStream(Stream stream, int initialLength)
        {
            if (initialLength < 1)
            {
                Debug.WriteLine("Here I am 1");
                initialLength = 32768;
            }
            byte[] buffer = new byte[initialLength];
            Debug.WriteLine("Here I am 2");
            int read = 0;
            Debug.WriteLine("Here I am 3");
            int chunk;
            Debug.WriteLine("Here I am 4");
            while ((chunk = stream.Read(buffer, read, buffer.Length - read)) > 0)
            {
                Debug.WriteLine("Here I am 5");
                read += chunk;
                Debug.WriteLine("Here I am 6");
                if (read == buffer.Length)
                {
                    Debug.WriteLine("Here I am 7");
                    int nextByte = stream.ReadByte();
                    Debug.WriteLine("Here I am 8");
                    if (nextByte == -1)
                    {
                        Debug.WriteLine("Here I am 9");
                        return buffer;
                    }
                    Debug.WriteLine("Here I am 10");
                    byte[] newBuffer = new byte[buffer.Length * 2];
                    Debug.WriteLine("Here I am 11");
                    Array.Copy(buffer, newBuffer, buffer.Length);
                    Debug.WriteLine("Here I am 12");
                    newBuffer[read] = (byte)nextByte;
                    Debug.WriteLine("Here I am 13");
                    buffer = newBuffer;
                    Debug.WriteLine("Here I am 14");
                    read++;
                }
            }
            byte[] bytes = new byte[read];
            Debug.WriteLine("Here I am 15");
            Array.Copy(buffer, bytes, read);
            Debug.WriteLine("Here I am 16");
            return bytes;
        }
    }
}