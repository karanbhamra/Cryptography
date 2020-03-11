using System;
using System.Collections.Generic;
using System.Text;

namespace TranspositionCipher
{
    public class LetterInfo
    {
        public char Letter;
        public int LetterPositionInKey;
        public int NumOrder;

        public override string ToString()
        {
            return $"Letter: {Letter}, Ordering: {NumOrder}, Position: {LetterPositionInKey}";
        }
    }

    public class DoubleTranspositionCipher
    {
        string keyOne;
        string keyTwo;
        public DoubleTranspositionCipher(string keyOne, string keyTwo)
        {
            this.keyOne = keyOne;
            this.keyTwo = keyTwo;
        }

        private string GetTableColumn(char[,] table, int col)
        {
            if (col < 0 || col >= table.GetLength(1))
            {
                throw new IndexOutOfRangeException($"Col: {col} is out of range.");

            }

            StringBuilder stringBuilder = new StringBuilder();

            for (int row = 0; row < table.GetLength(0); row++)
            {
                if (table[row, col] != default(char))
                {
                    stringBuilder.Append(table[row, col]);
                }
            }

            return stringBuilder.ToString();
        }

        public char[,] BuildTable(string input, string key)
        {
            int numCols = key.Length;
            int numRows = (int)Math.Ceiling(input.Length / (double)numCols);

            var table = new char[numRows, numCols];
            int processed = 0;
            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    if (input.Length > processed)
                    {
                        table[row, col] = input[processed];
                        processed++;
                    }
                }
            }

            return table;
        }

        public List<LetterInfo> GetKeyLetterInfo(string input)
        {
            var list = new List<LetterInfo>();

            var letterArray = input.ToCharArray();

            Array.Sort(letterArray);

            var addedIndices = new List<int>();
            int count = 0;
            foreach (var letter in letterArray)
            {
                int indexInInput = input.IndexOf(letter);

                while (addedIndices.Contains(indexInInput))
                {
                    indexInInput = input.IndexOf(letter, indexInInput + 1);
                }

                addedIndices.Add(indexInInput);

                LetterInfo temp = new LetterInfo()
                {
                    Letter = letter,
                    LetterPositionInKey = indexInInput,
                    NumOrder = count
                };

                count++;

                list.Add(temp);
            }

            return list;
        }

        public void PrintTable(char[,] table)
        {
            Console.WriteLine(new string('-', 100));
            for (int row = 0; row < table.GetLength(0); row++)
            {
                for (int col = 0; col < table.GetLength(1); col++)
                {
                    Console.Write($"{table[row, col]}\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine(new string('-', 100));
        }

        public string StringAddBreaks(string input, int count, char newchar)
        {
            StringBuilder output = new StringBuilder();

            int processed = 0;
            for (int i = 0; i < input.Length; i++)
            {
                var current = input[i];
                output.Append(current);
                processed++;

                if (processed > 0 && processed % count == 0)
                {
                    output.Append(newchar);
                }

            }

            return output.ToString();
        }

        public string Encrypt(string plainText)
        {
            StringBuilder output = new StringBuilder();

            var firstMatrix = BuildTable(plainText, keyOne);

            var letterInfo = GetKeyLetterInfo(keyOne);

            foreach (var letter in letterInfo)
            {
                output.Append(GetTableColumn(firstMatrix, letter.LetterPositionInKey));
            }

            string firstTranspoOutput = output.ToString();

            PrintTable(firstMatrix);

            output.Clear();

            var secondMatrix = BuildTable(firstTranspoOutput, keyTwo);

            letterInfo = GetKeyLetterInfo(keyTwo);

            foreach (var letter in letterInfo)
            {
                output.Append(GetTableColumn(secondMatrix, letter.LetterPositionInKey));
            }

            PrintTable(secondMatrix);
            return output.ToString();
        }
    }
}
