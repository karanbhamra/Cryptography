using System;
using System.Collections.Generic;
using System.Text;

namespace OneTimePad
{
    public static class Pad
    {
        static string alphabet = "abcdefghijklmnopqrstuvwxyz";

        public static string Encrypt(string plaintext, string key)
        {

            if (plaintext.Length != key.Length || plaintext.Length == 0)
            {
                throw new Exception($"Key must be of the same length as the plaintext.");
            }

            plaintext = plaintext.ToLower();

            StringBuilder build = new StringBuilder();

            for (int i = 0; i < plaintext.Length; i++)
            {
                int plainVal = alphabet.IndexOf(plaintext[i]);
                int padVal = alphabet.IndexOf(key[i]);
                int newVal = (plainVal + padVal) % alphabet.Length;

                char newChar = alphabet[newVal];

                build.Append(newChar);
            }

            return build.ToString();
        }

        public static string Decrypt(string cipherText, string key)
        {
            if (cipherText.Length != key.Length || cipherText.Length == 0)
            {
                throw new Exception($"Key must be of the same length as the ciphertext.");
            }
            StringBuilder build = new StringBuilder();

            for (int i = 0; i < cipherText.Length; i++)
            {
                int cipherVal = alphabet.IndexOf(cipherText[i]);
                int padVal = alphabet.IndexOf(key[i]);
                int newVal = (cipherVal - padVal);

                if (newVal < 0)
                {
                    newVal = alphabet.Length + newVal;
                }

                newVal = newVal % alphabet.Length;

                char newChar = alphabet[newVal];

                build.Append(newChar);

            }

            return build.ToString();
        }
    }
}
