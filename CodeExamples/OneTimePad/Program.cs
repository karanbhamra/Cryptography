using System;

namespace OneTimePad
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "thisistheonetimepadworking";
            string key = OneTimeKey.GenerateKey(input);
            string output = Pad.Encrypt(input, key);    
            string decrypted = Pad.Decrypt(output, key);

            Console.WriteLine(input);
            Console.WriteLine(key);
            Console.WriteLine(output);
            Console.WriteLine(decrypted);
        }
    }
}
