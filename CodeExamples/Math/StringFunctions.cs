using System;
using System.Collections.Generic;
using System.Text;

namespace MathExamples
{
    class StringFunctions
    {
        public static List<string> Permute(string input)
        {
            List<string> permutations = new List<string>();
            StringBuilder builder = new StringBuilder(input);
            Permute(builder, permutations, 0, input.Length - 1);

            return permutations;
        }

        private static void Permute(StringBuilder input, List<string> allcombos, int left, int right)
        {
            if (left == right)
            {
                allcombos.Add(input.ToString());
            }
            else
            {
                for (int i = left; i <= right; i++)
                {
                    Swap(input, left, i);
                    Permute(input, allcombos, left + 1, right);
                    Swap(input, left, i);

                }
            } 
        }

        public static void Swap(StringBuilder str, int first, int second)
        {
            char temp = str[first];
            str[first] = str[second];
            str[second] = temp;
        }

        public static string RotateString(string input)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 1; i < input.Length; i++)
            {
                sb.Append(input[i]);
            }
            sb.Append(input[0]);

            return sb.ToString();

        }

    }
}
