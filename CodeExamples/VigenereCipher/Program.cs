using System;

namespace VigenereCipher
{
    class Program
    {
        static void Main(string[] args)
        {
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            VigenereCipher cipher = new VigenereCipher(alphabet);

            string key = "battista";
            string plainText = "this is a test message.";
            string cipherText = cipher.Encrypt(plainText, key);
            string decipheredText = cipher.Decrypt(cipherText, key);

            Console.WriteLine($"Plaintext: {plainText}");
            Console.WriteLine($"Key: {key}");
            Console.WriteLine($"Lengthen Key: {cipher.LengthenKey(key, plainText)}");
            Console.WriteLine($"Ciphertext: {cipherText}");
            Console.WriteLine($"Deciphered: {decipheredText}");

        }
    }
}
