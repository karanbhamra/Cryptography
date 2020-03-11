using System;
using System.Collections.Generic;
using System.Text;

namespace Entropy
{
    public class Compression
    {
        public static string alphabet = "abcdefghijklmnopqrstuvwxyz";
        public static string CompressString(string input)
        {
            StringBuilder sb = new StringBuilder();

            int start = 0;

            // start with the next character from the current character
            // count how many times its apppearing until you encounter a new character
            // move the current character we are looking up by the adding the amount of repeated that occured
            // add the character and the counter
            while (start < input.Length)
            {
                char current = input[start];
                int letterCounter = 1;

                int i = start + 1;

                while (i < input.Length && input[i] == current)
                {
                    i++;
                    letterCounter++;
                }

                if (letterCounter != 1)
                {
                    start += letterCounter;
                }
                else
                {
                    start++;
                }

                sb.Append(current);
                sb.Append(letterCounter);

            }

            return sb.ToString();
        }

        public static string DecompressString(string input)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                char current = input[i];
                
                if (alphabet.Contains(current))
                {
                    StringBuilder strVal = new StringBuilder();
                    //string strVal = "";

                    int j = i + 1;

                    while (j < input.Length && !alphabet.Contains(input[j]))
                    {
                        strVal.Append(input[j]);
                        //strVal += input[j];
                        j++;
                    }

                    int mult = int.Parse(strVal.ToString());

                    for (int k = 0; k < mult; k++)
                    {
                        sb.Append(current);
                    }
                    ;
                }

            }
            return sb.ToString();
        }
    }
}
