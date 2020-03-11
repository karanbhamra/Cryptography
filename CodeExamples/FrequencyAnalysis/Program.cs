using System;
using System.Collections.Generic;
using System.IO;
using SubstitutionCipher;

namespace FrequencyAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {

           // read a dictionary text file and load compute word patterns for everyone
           var wordPattern = new WordPattern("words_alpha.txt");

           /**
           //var words = wordPattern.GetAllDictionaryWordsMatchingPattern("0.0.0.");

           //var patterns = wordPattern.GetDictionaryOfWordAndPattern();

           //var allWordsForPattern = wordPattern.GetDictionaryOfPatternWithAllMatchingWords();
           **/

           Console.WriteLine("enter a word");
            string input = Console.ReadLine();
            var allwords = wordPattern.GetAllDictionaryWordsMatchingWord(input);

            Console.WriteLine(new string('-', 100));
            if (allwords == null)
            {
                Console.WriteLine("No word found");
            }
            else
            {
                Console.WriteLine($"Found {allwords.Count} possible word matches");
                foreach (var word in allwords)
                {
                    Console.WriteLine(word);
                }
            }

            Dictionary<char, List<char>> cipherLetters = wordPattern.GetCipherLetterMapping(input);

            foreach (var (letter, cipherLettersList) in cipherLetters)
            {
                Console.WriteLine($"{letter}:");
                foreach (var cipherLetter in cipherLettersList)
                {
                    Console.WriteLine($"\t{cipherLetter}");
                }
            }

            var frequencies = FrequencyAnalysis.Analyze("karan");
            var textFreq = FrequencyAnalysis.AnalyzeFile("prideandprejudice.txt");

            var plaintext = File.ReadAllText("encrypted.txt");
            textFreq = FrequencyAnalysis.AnalyzeFile("encrypted.txt");
            var key = FrequencyAnalysis.DictionaryKeysToString(textFreq);


            ;
        }
    }
}
