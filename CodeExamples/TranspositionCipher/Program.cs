using System;

namespace TranspositionCipher
{
    class Program
    {
        static void Main(string[] args)
        {
            //Single Transposition Cipher
            string plainText = "Common sense is not so common.";
            int key = 8;
            var cipher = new TranspositionCipher();
            string encryptedText = cipher.Encrypt(plainText, key);

            cipher.PrintEncryptedTable();

            Console.WriteLine(new string('-', 100));

            string decryptedText = cipher.Decrypt(encryptedText, key);

            cipher.PrintDecryptedTable();

            Console.WriteLine($"Plaintext: {plainText}");
            Console.WriteLine($"Key: {key}");
            Console.WriteLine($"Encrypted: {encryptedText}");
            Console.WriteLine($"Decrypted: {decryptedText}");


            // PBS Double Transposition Cipher
            //string plainText = "this is a test sample";
            //string keyOne = "describe";
            //string keyTwo = "coastline";
            //var doubleTranspo = new DoubleTranspositionCipher(keyOne, keyTwo);
            //var cipherText = doubleTranspo.Encrypt(plainText);
            //Console.WriteLine($"Plaintex: {plainText}");
            //Console.WriteLine($"Key One: {keyOne}");
            //Console.WriteLine($"Key Two: {keyTwo}");
            //Console.WriteLine($"CipherText: {cipherText}");
            //Console.WriteLine($"Readable CipherText: {doubleTranspo.StringAddBreaks(cipherText, 5, ' ')}");
        }
    }
}
