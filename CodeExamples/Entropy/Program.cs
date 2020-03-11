using System;
using System.Collections.Generic;

namespace Entropy
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> inputs = new List<string>()
            {
                "abcd",
                "aabbccdd",
                "abbcccdddd",
                "aaaabbbbccccdddd",
                "aaaaaaaabbbbbbbbbbbbbbcccccccccccccccccccccccccccccccdddddddddddddddddddddddddd",
                "aaaabcccdddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd"
            };

            Console.WriteLine("Compressing");
            foreach (var input in inputs)
            {
                string output = Compression.CompressString(input);

                Console.WriteLine($"Input: {input}, Length = {input.Length}");
                Console.WriteLine($"Output: {output}, Length = {output.Length}");
                Console.WriteLine();
            }
            Console.WriteLine("----------------------------------");

            inputs.Clear();

            inputs.AddRange(new string[]
            {
                "a15b1c1d1",
                "a4b1c3d100"
            });

            Console.WriteLine("Decompressing");
            foreach (var input in inputs)
            {
                string output = Compression.DecompressString(input);
                Console.WriteLine($"Input: {input}, Length = {input.Length}");
                Console.WriteLine($"Output: {output}, Length = {output.Length}");
                Console.WriteLine();
            }
            Console.WriteLine("----------------------------------");


        }
    }
}
