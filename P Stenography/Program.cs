using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace P_Stenography
{
    class Program //It fucking works now
    {
        static void Main(string[] args)
        {
            defaultProgram();

            Debug.WriteLine("Main ended");
            Console.ReadLine();
        }

        private static void defaultProgram()
        {
            string exeBaseDir = AppDomain.CurrentDomain.BaseDirectory + "\\Steno Extra";
            Console.WriteLine("Directory is " + exeBaseDir + "\nCreating it now. Please take a look, its in the same folder as the exe");
            System.IO.Directory.CreateDirectory(exeBaseDir);
            string filePath = exeBaseDir + "\\startImage.jpg";
            string filePathModified = exeBaseDir + "\\modifiedImage.png";
            string[] readmeText = { "Run the program, before you do anything more make sure that startImage or modifiedImage are present in the Steno Extra folder"
            , "startImage.jpg 		(The original file, the encrypted file will be based on, you need to place it yourself)"
            , "modifiedImage.png 	(The new encrypted file the program will generate. This is also the one you need to decode)"};

            System.IO.File.WriteAllLines(exeBaseDir + "\\README.txt", readmeText);


            Debug.WriteLine("____ PROGRAM STARTED NOW ____");
            Console.WriteLine("[1] -> Encode  \n[2] -> Decode");
            string choiceInput = Console.ReadLine();
            Console.WriteLine("");

            if (choiceInput.Equals("1"))
            {
                Console.WriteLine("\nENCODE\nYour secret message to be hidden in the image");
                string secretMessage = Console.ReadLine() + "/-/"; //So i know when the message has ended
                string secretBinary = Utility.textToBinary(secretMessage);
                Debug.WriteLine("Secret binary = " + secretBinary);

                writeToBitmap(filePath, secretBinary).Save(filePathModified, ImageFormat.Png); //Whatever

            }
            else if (choiceInput.Equals("2"))
            {
                Console.WriteLine("\nDECODE\n");

                string binary = readFromBitmap(filePathModified);
                Debug.WriteLine("The returned binary is = " + binary);
                Console.WriteLine("So the secret message is:");
                string result = Utility.BinaryToString(binary);
                result = result.Substring(0, result.Length - 3);
                Console.WriteLine(result);

            }
            else
            {
                Environment.Exit(0);
            }

            Console.WriteLine("Default program ended");
        }

        public static Bitmap writeToBitmap(string filePath, string secretBin)
        {
            Bitmap bitmap = new Bitmap(filePath);
            int x, y, looped = 0;



            for (y = 0; y < bitmap.Height; y++)                 //Loop through the images pixels
            {
                for (x = 0; x < bitmap.Width; x++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);   // pixelColor.R

                    if (looped < secretBin.Length)
                    {
                        string currentLastDigit = secretBin[looped].ToString();

                        if (!currentLastDigit.Equals(" "))
                        {
                            byte newRedByte = Utility.applyLastDigit(pixelColor.R, currentLastDigit);

                            Color newColor = Color.FromArgb(pixelColor.A, newRedByte, pixelColor.G, pixelColor.B);

                            bitmap.SetPixel(x, y, newColor);
                        }
                    }
                    else
                    {
                        //Console.WriteLine("Not out of index");
                    }
                    looped++;
                }
            }
            return bitmap;
        }


        public static string readFromBitmap(string filePath)
        {
            Debug.WriteLine("Read from bitmap started");
            Bitmap bitmap = new Bitmap(filePath);
            int x, y;
            int count = 0;
            string result = "";
            for (y = 0; y < bitmap.Height; y++)                 //Loop through the images pixels
            {
                for (x = 0; x < bitmap.Width; x++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);   // pixelColor.R

                    int length = pixelColor.R.ToString().Length;
                    string lastDigit = pixelColor.R.ToString();
                    lastDigit = lastDigit.Substring(length - 1, 1);

                    Debug.WriteLine("Count = " + count + ". X-Y " + x + " " + y);
                    count++;

                    result = result + lastDigit;
                    if (result.Length % 8 == 0)
                    {
                        if (Utility.BinaryToString(result).Contains("/-/"))
                        {
                            Debug.WriteLine("ReadFromBitmap from if statement");
                            return result;
                        }
                    }

                }
            }
            Debug.WriteLine("ReadFromBitmap ended");
            return result;
        }



    }
}