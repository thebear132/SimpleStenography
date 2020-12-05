using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_Stenography
{
    class Test
    {
        public static void testProgram()
        {
            string filePath = @"C:\Users\Bear\Dropbox\WSE\P Stenography\sun.jpg";
            string filePathModified = @"C:\Users\Bear\Dropbox\WSE\P Stenography\sun_modified.png";

            Bitmap bitmap = new Bitmap(filePath);
            int x, y, looped = 0;

            for (y = 0; y < bitmap.Height; y++)                 //Loop through the images pixels
            {
                for (x = 0; x < bitmap.Width; x++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);   // pixelColor.R
                    Color newColor = Color.FromArgb(pixelColor.A, 0, pixelColor.G, pixelColor.B); //HERE I CHANGE THE PIXEL COLOR RED TO 0
                    bitmap.SetPixel(x, y, newColor);


                    pixelColor = bitmap.GetPixel(x, y); //just testing

                    //Printing the color values from (0,500) to (10,500)
                    if (y == 500 & looped < 10)
                    {
                        Console.WriteLine(looped + " Coords (" + x + "," + y + ") New pixel being set ARGB=" + newColor.A + " " + newColor.R + " " + newColor.G + " " + newColor.B);


                        Console.WriteLine("    RIGHT AFTER SET PIXEL READING AGAIN = " + pixelColor.A + " " + pixelColor.R + " " + pixelColor.G + " " + pixelColor.B);
                        Console.WriteLine();

                        looped++;
                    }
                }
            }

            bitmap.Save(filePathModified, ImageFormat.Png);
            //SaveWithJpegQuality(bitmap, filePathModified);



            Console.WriteLine("\n\nReading now\n\n");

            //NOW READING THE MODIFIED IMAGE
            Bitmap bitmap2 = new Bitmap(Image.FromFile(filePathModified));
            int x2, y2, looped2 = 0;

            for (y2 = 0; y2 < bitmap2.Height; y2++)                 //Loop through the images pixels
            {
                for (x2 = 0; x2 < bitmap2.Width; x2++)
                {
                    Color pixelColor = bitmap2.GetPixel(x2, y2);

                    //Printing the first 10 red color values
                    if (y2 == 500 & looped2 < 10)
                    {
                        Console.WriteLine(looped2 + " Coords (" + x2 + "," + y2 + ") Pixel from modified ARGB=" + pixelColor.A + " " + pixelColor.R + " " + pixelColor.G + " " + pixelColor.B);
                        looped2++;
                    }
                }
            }
        }


        static void printBitmap(Bitmap b)
        {
            int size = b.Width * b.Height
                , looped2 = 0;

            for (int i = 0; i < size; i++)
            {
                Color pixelColor = b.GetPixel(i % b.Width, i / b.Width);

                Console.WriteLine(looped2 + " Coords (" + i % b.Width + "," + (int)i / b.Width + ") Pixel from modified ARGB=" + pixelColor.A + " " + pixelColor.R + " " + pixelColor.G + " " + pixelColor.B);
            }
        }




        static void SaveWithJpegQuality(Bitmap I, string location)
        {
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;
            ImageCodecInfo myImageCodecInfo;
            System.Drawing.Imaging.Encoder myEncoder;
            myEncoder = System.Drawing.Imaging.Encoder.Quality;
            // Save the bitmap as a JPEG file with quality level 25.
            myImageCodecInfo = GetEncoderInfo("image/jpeg");
            myEncoderParameter = new EncoderParameter(myEncoder, 25L);
            myEncoderParameters = new EncoderParameters(1);
            myEncoderParameters.Param[0] = myEncoderParameter;
            I.Save(location, myImageCodecInfo, myEncoderParameters);
        }
        private static Func<string, ImageCodecInfo> GetEncoderInfo = (mimeType)
            => ImageCodecInfo.GetImageEncoders().ToList().Find((v) => v.MimeType == mimeType);
    }
}
