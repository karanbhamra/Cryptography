using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FrequencyAnalysis
{
    public class FrequencyAnalysis
    {
        public static Dictionary<char, int> Analyze(string input)
        {
            //string alphabet = "abcdefghijklmnopqrstuvwxyz";

            //Dictionary<char, int> analysis = new Dictionary<char, int>();

            //foreach (var letter in input)
            //{
            //    if (!analysis.ContainsKey(letter))
            //    {
            //        analysis[letter] = 0;
            //    }

            //    analysis[letter] += 1;
            //}


            //return analysis;

            string alphabet = "abcdefghijklmnopqrstuvwxyz";

            Dictionary<char, int> mapping = new Dictionary<char, int>();

            for (int i = 0; i < alphabet.Length; i++)
            {
                mapping[alphabet[i]] = 0;
            }

            foreach (var character in input)
            {
                var lower = char.ToLower(character);
                if (mapping.ContainsKey(lower))
                {
                    mapping[lower]++;
                }
            }

            return SortedByCount(mapping);
        }
        public static Dictionary<char, int> AnalyzeFile(string filename)
        {
            var text = File.ReadAllText(filename);

            return Analyze(text);
            //string alphabet = "abcdefghijklmnopqrstuvwxyz";

            //Dictionary<char, int> mapping = new Dictionary<char, int>();

            //for (int i = 0; i < alphabet.Length; i++)
            //{
            //    mapping[alphabet[i]] = 0;
            //}

            //foreach (var character in text)
            //{
            //    var lower = char.ToLower(character);
            //    if (mapping.ContainsKey(lower))
            //    {
            //        mapping[lower]++;
            //    }
            //}

            //return SortedByCount(mapping);
        }

        private static Dictionary<char, int> SortedByCount(Dictionary<char, int> mapping)
        {
            return mapping.OrderByDescending(x => x.Value).ThenBy(x=>x.Key).ToDictionary(x => x.Key, x => x.Value);
        }

        public static string DictionaryKeysToString(Dictionary<char, int> mapping)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var key in mapping.Keys)
            {
                stringBuilder.Append(key);
            }

            return stringBuilder.ToString();
        }

    }
}
