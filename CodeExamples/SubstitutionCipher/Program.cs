using System;

namespace SubstitutionCipher
{
    class Program
    {
        static void Main(string[] args)
        {
            string alphabet = "abcdefghijklmnopqrstuvwxyz";

            var subCipher = new SubstitutionCipher(alphabet);

            string plaintext = "flee at once. we are discovered!";
            string key = subCipher.GetPermutedKey();
            string cipherText = subCipher.Encrypt(key, plaintext);
            string decryptedText = subCipher.Decrypt(key, cipherText);

            Console.WriteLine($"Plaintext: {plaintext}");
            Console.WriteLine($"Key: {key}");
            Console.WriteLine($"CipherText: {cipherText}");
            Console.WriteLine($"DecryptedText: {decryptedText}");

        }
    }
}
