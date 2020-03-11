using System;
using System.Collections.Generic;
using System.IO;

namespace CodeExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read the encrypted text from the given file and then run AutoSolve on it to decrypt it.
            string input = File.ReadAllText("CaesarEncrypted.txt");
            CaesarCipherAutoSolver solver = new CaesarCipherAutoSolver(input, "words_alpha.txt");
            List<string> result = solver.AutoSolve();

            foreach (string solvedText in result)
            {
                Console.WriteLine(solvedText);
            }

            Console.ReadKey();
        }
    }
}
