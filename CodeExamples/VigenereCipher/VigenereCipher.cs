using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace VigenereCipher
{
    public class VigenereCipher
    {
        private string alphabet;
        StringBuilder stringBuilder;

        char[,] table;
        public VigenereCipher(string alphabet)
        {
            this.alphabet = alphabet;
            stringBuilder = new StringBuilder();
            table = BuildTabulaRecta();
            DisplayTabulaRecta(table);
        }

        public string Encrypt(string plaintext, string key)
        {
            plaintext = plaintext.ToLower();

            string properKey = LengthenKey(key, plaintext).ToLower();

            stringBuilder.Clear();

            int keyIndex = 0;
            for (int i = 0; i < plaintext.Length; i++)
            {
                int pos = alphabet.IndexOf(plaintext[i]);

                if (pos == -1)
                {
                    stringBuilder.Append(plaintext[i]);
                }
                else
                {
                    char keyLetter = properKey[keyIndex];

                    char plainLetter = plaintext[i];

                    int row = alphabet.IndexOf(plainLetter);
                    int col = alphabet.IndexOf(keyLetter);


                    char encodedChar = table[row, col];

                    stringBuilder.Append(encodedChar);

                    keyIndex++;

                }

            }

            return stringBuilder.ToString();
        }
        public string Decrypt(string ciphertext, string key)
        {
            ciphertext = ciphertext.ToLower();

            string properKey = LengthenKey(key, ciphertext).ToLower();

            stringBuilder.Clear();

            string properKeyMatchInput = MatchKeys(ciphertext, properKey);

            // loop over the columns to find where the letter of the keystream occurs
            //for (int i = 0; i < properKey.Length; i++)
            //{
            //    for (int col = 0; col < table.GetLength(1); col++)
            //    {
            //        if (table[0, col] == properKey[i])
            //        {
            //            // look down this column until you get the ciphertext, the row that contained it is the letter we need
            //            for (int row = 0; row < table.GetLength(0); row++)
            //            {
            //                if (table[row, col] == ciphertext[i])
            //                {
            //                    stringBuilder.Append(table[row, 0]);
            //                }
            //            }
            //        }
            //    }
            //}

            for (int i = 0; i < properKeyMatchInput.Length; i++)
            {
                if (alphabet.Contains(properKeyMatchInput[i]))
                {
                    for (int col = 0; col < table.GetLength(1); col++)
                    {
                        if (table[0, col] == properKeyMatchInput[i])
                        {
                            // look down this column until you get the ciphertext, the row that contained it is the letter we need
                            for (int row = 0; row < table.GetLength(0); row++)
                            {
                                if (table[row, col] == ciphertext[i])
                                {
                                    stringBuilder.Append(table[row, 0]);
                                }
                            }
                        }
                    }
                }

                else
                {
                    stringBuilder.Append(properKeyMatchInput[i]);
                }
            }

            return stringBuilder.ToString();
        }

        private string MatchKeys(string ciphertext, string properKey)
        {
            string output = "";
            int count = 0;
            for (int i = 0; i < ciphertext.Length; i++)
            {
                if (alphabet.Contains(ciphertext[i]))
                {
                    output += properKey[count];
                    count++;
                }
                else
                {
                    output += ciphertext[i];
                }
            }

            return output;
        }

        private char[,] BuildTabulaRecta()
        {
            char[,] table = new char[alphabet.Length, alphabet.Length];

            for (int row = 0; row < table.GetLength(0); row++)
            {
                string shiftedAlphabet = ShiftAlphabetToLeft(row);
                int count = 0;

                for (int col = 0; col < table.GetLength(1); col++)
                {
                    table[row, col] = shiftedAlphabet[count];
                    count++;
                }
            }
            return table;
        }

        public void DisplayTabulaRecta(char[,] table)
        {
            for (int row = 0; row < table.GetLength(0); row++)
            {
                for (int col = 0; col < table.GetLength(1); col++)
                {
                    Console.Write($"{table[row, col]}  ");
                }
                Console.WriteLine();
            }
        }

        private string ShiftOnce(string text)
        {
            return text.Substring(1, text.Length - 1) + text[0];
        }

        public string ShiftAlphabetToLeft(int n)
        {
            string shifted = alphabet;
            for (int i = 0; i < n; i++)
            {
                shifted = ShiftOnce(shifted);
            }

            return shifted;
        }


        public string LengthenKey(string key, string plainText)
        {
            plainText = StripPunctuation(plainText);
            stringBuilder.Clear();

            for (int i = 0; i < plainText.Length; i++)
            {
                var letter = key[i % key.Length];
                stringBuilder.Append(letter);
            }

            return stringBuilder.ToString();
        }

        public string StripPunctuation(string input)
        {
            stringBuilder.Clear();

            input = input.ToLower();

            foreach (var letter in input)
            {
                if (alphabet.Contains(letter))
                {
                    stringBuilder.Append(letter);
                }
            }

            return stringBuilder.ToString();
        }
    }
}
