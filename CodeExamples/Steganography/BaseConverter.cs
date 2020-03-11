using System;
using System.Collections.Generic;
using System.Text;

namespace Steganography
{
    public class BaseConverter
    {
        static string hexalphabet = "0123456789ABCDEF";
        public static string DecimalToBinary(int number, int padTo = 8)
        {
            StringBuilder output = new StringBuilder();

            if (number == 0)
            {
                output.Append(number);
            }

            while (number != 0)
            {
                int remainder = number % 2;

                output.Insert(0, remainder);

                number /= 2;
            }

            while (output.Length < padTo)
            {
                output.Insert(0, 0);
            }


            return output.ToString();
        }

        public static int BinaryToDecimal(string binary)
        {
            int num = 0;

            int mult = 1;
            for (int i = binary.Length - 1; i >= 0; i--)
            {
                if (binary[i] == '1')
                {
                    num += mult;
                }

                mult *= 2;
            }


            return num;
        }

        public static string DecimalToHexadecimal(int num)
        {
            StringBuilder sb = new StringBuilder();

            if (num == 0)
            {
                sb.Append(num);
            }

            while (num != 0)
            {
                int remainder = num % 16;

                sb.Insert(0, hexalphabet[remainder]);

                num /= 16;
            }


            return sb.ToString();
        }

        public static int HexadecimalToDecimal(string hex)
        {
            int val = 0;
            int mult = 1;

            for (int i = hex.Length - 1; i >= 0; i--)
            {
                val += hexalphabet.IndexOf(hex[i]) * mult;

                mult *= 16;
            }

            return val;
        }

        public static int DecimalToOctal(int num)
        {
            StringBuilder sb = new StringBuilder();

            if (num == 0)
            {
                sb.Append(num);
            }

            while (num != 0)
            {
                int remainder = num % 8;

                sb.Insert(0, remainder);

                num /= 8;

            }

            return int.Parse(sb.ToString());
        }


        public static int OctalToDecimal(int num)
        {
            int val = 0;
            int mult = 1;
            while (num != 0)
            {
                int remainder = num % 10;

                val += remainder * mult;

                num /= 10;

                mult *= 8;
            }


            return val;
        }
    }
}
