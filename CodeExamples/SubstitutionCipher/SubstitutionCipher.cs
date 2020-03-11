using System;
using System.Collections.Generic;
using System.Text;

namespace SubstitutionCipher
{
    public class SubstitutionCipher
    {
        private string alphabet;
        private Random random;

        public SubstitutionCipher(string alphabet)
        {
            this.alphabet = alphabet;
            random = new Random();
        }

        public string GetPermutedKey()
        {
            var indices = GetUniqueRandomNumbers(alphabet.Length);

            StringBuilder stringBuilder = new StringBuilder();

            foreach (var index in indices)
            {
                stringBuilder.Append(alphabet[index]);
            }

            return stringBuilder.ToString();
        }

        private List<int> GetUniqueRandomNumbers(int length)
        {
            List<int> list = new List<int>();

            for (int i = 0; i < length; i++)
            {
                int temp;

                do
                {
                    temp = random.Next(length);
                } while (list.Contains(temp));

                list.Add(temp);

            }

            return list;
        }

        public string Encrypt(string key, string plaintext)
        {
            StringBuilder output = new StringBuilder();

            for (int i = 0; i < plaintext.Length; i++)
            {
                var current = plaintext[i];

                int oldIndex = alphabet.IndexOf(current);

                if (oldIndex == -1)
                {
                    output.Append(current);
                }
                else
                {
                    var newLetter = key[oldIndex];
                    output.Append(newLetter);
                }

            }

            return output.ToString();
        }

        public string Decrypt(string key, string cipherText)
        {
            StringBuilder output = new StringBuilder();

            for (int i = 0; i < cipherText.Length; i++)
            {
                var current = cipherText[i];

                int oldIndex = key.IndexOf(current);

                if (oldIndex == -1)
                {
                    output.Append(current);
                }
                else
                {
                    var newLetter = alphabet[oldIndex];
                    output.Append(newLetter);
                }

            }

            return output.ToString();
        }
    }
}
