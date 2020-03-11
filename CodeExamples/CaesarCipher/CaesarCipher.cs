using System;
using System.Collections.Generic;
using System.Text;

namespace CodeExamples
{
    class CaesarCipher
    {
        // For the sake of simplicity, our alphabet is lowercase.
        static string alphabet = "abcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// Encrypts the plaintext by specified key amount, invalid characters are appended as is.
        /// </summary>
        /// <param name="plainText">The plain text string input.</param>
        /// <param name="key">The right shift amount.</param>
        /// <returns>A string representing the plaintext shifted by key amount.</returns>
        public static string Encrypt(string plainText, int key)
        {
            plainText = plainText.ToLower();

            StringBuilder cipherText = new StringBuilder();

            for (int i = 0; i < plainText.Length; i++)
            {
                char current = plainText[i];
                int currentIndexInAlphabet = IndexOf(alphabet, current);

                // If current character is not found, we will append it and move on to the next character.
                if (currentIndexInAlphabet == -1)
                {
                    cipherText.Append(current);
                    continue;
                }

                // Calculate out the new character, mod by length of alphabet in case it goes past the end of alphabet.
                int newIndexInAlphabet = (currentIndexInAlphabet + key) % alphabet.Length;

                char newEncryptedCharacter = alphabet[newIndexInAlphabet];

                cipherText.Append(newEncryptedCharacter);
            }

            return cipherText.ToString();
        }

        /// <summary>
        /// Decrypts the ciphertext by the specified key amount, invalid characters are appended as is.
        /// </summary>
        /// <param name="encryptedText">The encrypted text string input.</param>
        /// <param name="key">The left shift amount.</param>
        /// <returns>A string representing the plain text.</returns>
        public static string Decrypt(string encryptedText, int key)
        {
            encryptedText = encryptedText.ToLower();

            StringBuilder plainText = new StringBuilder();

            for (int i = 0; i < encryptedText.Length; i++)
            {
                char current = encryptedText[i];
                int currentIndexInAlphabet = IndexOf(alphabet, current);

                if (currentIndexInAlphabet == -1)
                {
                    plainText.Append(current);
                    continue;
                }

                int newIndexInAlphabet = (currentIndexInAlphabet - key) % alphabet.Length;

                // If the newIndex goes past the left side, we calculate the new index so it properly wraps around.
                if (newIndexInAlphabet < 0)
                {
                    newIndexInAlphabet = (alphabet.Length - Math.Abs(newIndexInAlphabet)) % 26;
                }

                char newDecryptedCharacter = alphabet[newIndexInAlphabet];

                plainText.Append(newDecryptedCharacter);
            }

            return plainText.ToString();
        }

        /// <summary>
        /// Decrypts the ciphertext by calling encrypt with key calculated as length of alphabet minus key.
        /// </summary>
        /// <param name="encryptedText">The encrypted text string input.</param>
        /// <param name="key">The left shift amount.</param>
        /// <returns>A string representing the plain text.</returns>
        public static string ShorterDecrypt(string encryptedText, int key)
        {
            // Due to the way Caesar Cipher words, to decrypt some text, we can call Encrypt again but with 
            // new key calculated from length of alphabet minus the key. 
            return Encrypt(encryptedText, alphabet.Length - key);
        }

        /// <summary>
        /// Finds the index of the character in a string.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="current">Character to look for.</param>
        /// <returns>An int of where the character occurs in string, -1 if not found.</returns>
        private static int IndexOf(string input, char current)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (current == input[i])
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Generates all plaintext combinations within a key range.
        /// </summary>
        /// <param name="encryptedText">The input encrypted string.</param>
        /// <param name="start">Key start value.</param>
        /// <param name="end">Key end value.</param>
        /// <returns>A list of strings with all the possible decrypted values.</returns>
        public static List<string> DecryptAllKeyCombinations(string encryptedText, int start = 0, int end = 26)
        {
            List<string> combinations = new List<string>();

            // Add all decrypted text string to list for every key.
            for (int key = start; key < end; key++)
            {
                combinations.Add(Decrypt(encryptedText, key));
            }

            return combinations;
        }

    }
}
