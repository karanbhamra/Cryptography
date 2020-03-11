using System;
using System.Collections.Generic;
using System.Text;

namespace OneTimePad
{
    public static class OneTimeKey
    {
        static Random gen = new Random();

        public static string GenerateKey(int length)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                // ASCII value between a and z
                int val = gen.Next(97, 97 + 26);
                char ch = (char)val;

                builder.Append(ch);
            }

            return builder.ToString();
        }

        public static string GenerateKey(string plaintext)
        {
            return GenerateKey(plaintext.Length);
        }
    }
}
