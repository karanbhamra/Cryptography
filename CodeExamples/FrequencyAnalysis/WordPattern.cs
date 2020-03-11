using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FrequencyAnalysis
{
    public class WordPattern
    {
        private static StringBuilder stringBuilder = new StringBuilder();

        private string dictionaryFileName;
        private Dictionary<string, string> wordsAndPattern;
        private Dictionary<string, List<string>> wordsMatchingPatterns;
        public WordPattern(string dictionaryFileName)
        {
            this.dictionaryFileName = dictionaryFileName;
            wordsAndPattern = new Dictionary<string, string>();
            wordsMatchingPatterns = new Dictionary<string, List<string>>();
        }
        public static string GetPattern(string input, char delimiter = '.')
        {
            Dictionary<char, int> dict = new Dictionary<char, int>();
            int count = 0;

            stringBuilder.Clear();

            foreach (var letter in input)
            {
                if (!dict.ContainsKey(letter))
                {
                    dict[letter] = count;
                    count++;
                }

                stringBuilder.Append(dict[letter]);
                stringBuilder.Append(delimiter);
            }

            return stringBuilder.ToString();

        }

        public Dictionary<char, List<char>> GetCipherLetterMapping(string input)
        {
            Dictionary<char, List<char>> mappings = new Dictionary<char, List<char>>();

            var allwords = GetAllDictionaryWordsMatchingWord(input);

            if (allwords == null)
            {
                throw new Exception($"No possible word matches found for {input}");
            }

            for (int i = 0; i < input.Length; i++)
            {
                var current = input[i];

                if (!mappings.ContainsKey(current))
                {
                    mappings[current] = new List<char>();
                }

                foreach (var word in allwords)
                {
                    if (!mappings[current].Contains(word[i]))
                        mappings[current].Add(word[i]);
                }

            }

            return mappings;
        }

        public Dictionary<string, string> GetDictionaryOfWordAndPattern()
        {
            // if it was already loaded and someone called the method subsequent times
            if (wordsAndPattern.Count > 0)
            {
                return wordsAndPattern;
            }

            var fileLines = LoadFile(dictionaryFileName);

            foreach (var word in fileLines)
            {
                string wordPattern = GetPattern(word);

                wordsAndPattern[word] = wordPattern;
            }

            return wordsAndPattern;
        }

        public Dictionary<string, List<string>> GetDictionaryOfPatternWithAllMatchingWords()
        {
            if (wordsAndPattern.Count == 0)
            {
                GetDictionaryOfWordAndPattern();
            }

            foreach (var (word, pattern) in wordsAndPattern)
            {
                if (!wordsMatchingPatterns.ContainsKey(pattern))
                {
                    wordsMatchingPatterns[pattern] = new List<string>();
                }

                wordsMatchingPatterns[pattern].Add(word);
            }

            return wordsMatchingPatterns;
        }

        public List<string> GetAllDictionaryWordsMatchingPattern(string pattern)
        {
            if (pattern.Length == 0)
            {
                return null;
            }
            if (pattern[pattern.Length - 1] != '.')
            {
                pattern += ".";
            }

            if (wordsMatchingPatterns.Count == 0)
            {
                GetDictionaryOfPatternWithAllMatchingWords();
            }

            if (!wordsMatchingPatterns.ContainsKey(pattern))
            {
                return null;
            }

            return wordsMatchingPatterns[pattern];
        }

        public List<string> GetAllDictionaryWordsMatchingWord(string word)
        {
            return GetAllDictionaryWordsMatchingPattern(GetPattern(word));
        }

        private List<string> LoadFile(string dictionaryFileName)
        {
            var list = File.ReadAllLines(dictionaryFileName);

            return list.ToList();
        }
    }
}
