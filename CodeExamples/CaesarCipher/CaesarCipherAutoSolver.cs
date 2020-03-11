using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace CodeExamples
{
    class CaesarCipherAutoSolver
    {
        public string EncryptedText { get; private set; }
        public string DictionaryPath { get; private set; }
        List<string> dictionaryWords;

        // Used to get words that are longer than threshold size.
        public int LengthThreshold { get; private set; }

        public CaesarCipherAutoSolver(string encryptedText, string dictionaryPath, int lengthThreshold = 4)
        {
            EncryptedText = encryptedText;
            DictionaryPath = dictionaryPath;
            LengthThreshold = lengthThreshold;
            LoadDictionaryWords();
        }

        /// <summary>
        /// Reads the dictionary file at the given path and loads it into a list.
        /// </summary>
        private void LoadDictionaryWords()
        {
            if (!File.Exists(DictionaryPath))
            {
                throw new FileNotFoundException($"{DictionaryPath} file does not exist.");
            }

            dictionaryWords = File.ReadAllLines(DictionaryPath).ToList();
        }

        /// <summary>
        /// Tries to solve a given cipher text by checking all decrypted combinations and seeing if any contain dictionary words.
        /// </summary>
        /// <returns>A list of decrypted strings containing matched dictionary words.</returns>
        public List<string> AutoSolve()
        {
            List<string> allDecryptedCombinations = CaesarCipher.DecryptAllKeyCombinations(EncryptedText);

            List<string> filteredDecryptedTexts = new List<string>();

            foreach (string possibleText in allDecryptedCombinations)
            {
                // Decided to split words on just spaces for simplicity.
                List<string> words = possibleText.Split(' ').ToList();

                foreach (string word in words)
                {
                    // Check if word is long enough and if it is inside our list of dictionary words.
                    if (word.Length > LengthThreshold)
                    {
                        bool result = dictionaryWords.Contains(word);

                        // If word was found, we will add the entire sentence to the list of possible decrypted values.
                        if (result)
                        {
                            filteredDecryptedTexts.Add(possibleText);
                            break;
                        }
                    }
                }
            }

            return filteredDecryptedTexts;
        }

    }
}
