using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_Stenography
{
    class Utility
    {
        public static byte applyLastDigit(byte myByte, string toApply)
        {
            string newByte = myByte.ToString();
            newByte = newByte.Substring(0, newByte.Length - 1) + toApply; //Change the last digit to toApply
            int thing = Int32.Parse(newByte);

            Byte[] bytes = BitConverter.GetBytes(thing);


            return bytes[0];
        }


        public static string textToBinary(string inputString)
        {
            string result = "";
            for (int i = 0; i < inputString.Length; i++)
            {
                char c = inputString[i];
                int integer = Convert.ToInt32(c);
                string binary = Convert.ToString(integer, 2);

                Debug.WriteLine("Adding 0's");
                for (int ii = 0; ii <= (8 - binary.Length); ii++) //Ensure that it is 8 bit, and not 7 bit like it outputs otherwise with lower numbers
                {
                    binary = "0" + binary;
                }
                result = result + binary;
            }
            return result;
        }

        public static string BinaryToString(String binary)
        {
            binary = binary.Replace(" ", String.Empty);

            var list = new List<Byte>();

            for (int i = 0; i < binary.Length; i += 8)
            {
                String t = binary.Substring(i, 8);

                list.Add(Convert.ToByte(t, 2));
            }

            var data = list.ToArray();
            string text = Encoding.ASCII.GetString(data);
            return text;
        }

    }
}
