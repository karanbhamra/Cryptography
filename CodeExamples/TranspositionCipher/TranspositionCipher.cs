using System;
using System.Collections.Generic;
using System.Text;

namespace TranspositionCipher
{
    public class TranspositionCipher
    {
        char[,] encryptedTable;
        char[,] decryptedTable;
        char emptyChar;
        char spaceChar;
        bool builtEncryptedTable;
        bool builtDecryptedTable;

        public TranspositionCipher(char emptyChar = '*', char spaceChar = '$')
        {
            this.emptyChar = emptyChar;
            this.spaceChar = spaceChar;
            builtEncryptedTable = false;
        }

        // Print the Decrypted Table once it has been constructed
        public void PrintDecryptedTable()
        {
            if (builtDecryptedTable == false || decryptedTable == null)
            {
                throw new NullReferenceException("Build the table before printing the table.");
            }
            Console.WriteLine(new string('-', 100));
            Console.WriteLine($"Decrypted Table");
            for (int row = 0; row < decryptedTable.GetLength(0); row++)
            {
                for (int col = 0; col < decryptedTable.GetLength(1); col++)
                {
                    Console.Write($"{decryptedTable[row, col]}\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine(new string('-', 100));

        }

        // Returns the built decrypted string after the decrypted table is created and read in row major form 
        public string Decrypt(string encryptedText, int key)
        {
            builtDecryptedTable = BuildDecryptedTable(encryptedText, key);


            return ReadRowMajorTable();
        }

        // Starts by inserting the required empty chars in reverse so the proper table is built and displaye when we try and 
        // visualize it. Then the encrypted string is processed and added to the table and empty characters are added as necessary
        private bool BuildDecryptedTable(string encryptedText, int key)
        {
            int numRows = key;
            int numCols = (int)Math.Ceiling(encryptedText.Length / (double)key);

            int leftoverSpots = (numRows * numCols) - encryptedText.Length;

            decryptedTable = new char[numRows, numCols];
            // before we can fill it up with the characters, we have to fill the leftover spots with the empty char in reverse

            int revRow = numRows - 1;
            int revCol = numCols - 1;

            for (int i = 0; i < leftoverSpots; i++)
            {
                decryptedTable[revRow, revCol] = emptyChar;

                revRow--;

                if (revRow < 0)
                {
                    revCol--;
                    revRow = numRows - 1;
                }

            }

            int processed = 0;

            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    if (decryptedTable[row, col] == emptyChar)
                    {
                        continue;
                    }
                    else
                    {
                        if (processed < encryptedText.Length)
                        {
                            var current = encryptedText[processed];
                            if (current == ' ')
                            {
                                decryptedTable[row, col] = spaceChar;
                            }
                            else
                            {
                                decryptedTable[row, col] = current;
                            }
                            processed++;
                        }
                    }
                }

            }

            return true;
        }

        // Simliar to decrypt
        public string Encrypt(string plainText, int key)
        {
            builtEncryptedTable = BuildEncryptedTable(plainText, key);

            return ReadColMajorTable();
        }

        // Read the table in Column major form
        public string ReadColMajorTable()
        {
            if (encryptedTable == null)
            {
                throw new NullReferenceException("Build the table before reading the table.");
            }

            StringBuilder stringBuilder = new StringBuilder();

            for (int col = 0; col < encryptedTable.GetLength(1); col++)
            {
                for (int row = 0; row < encryptedTable.GetLength(0); row++)
                {
                    var letter = encryptedTable[row, col];

                    if (letter == emptyChar)
                    {
                        continue;
                    }
                    else if (letter == spaceChar)
                    {
                        stringBuilder.Append(' ');
                    }
                    else
                    {
                        stringBuilder.Append(letter);
                    }

                }
            }

            return stringBuilder.ToString();
        }

        // Read table in Row Major form
        public string ReadRowMajorTable()
        {
            if (encryptedTable == null)
            {
                throw new NullReferenceException("Build the table before reading the table.");
            }

            StringBuilder stringBuilder = new StringBuilder();
            for (int row = 0; row < encryptedTable.GetLength(0); row++)
            {
                for (int col = 0; col < encryptedTable.GetLength(1); col++)
                {
                    var current = encryptedTable[row, col];

                    if (current != emptyChar)
                    {
                        if (current == spaceChar)
                        {
                            stringBuilder.Append(' ');
                        }
                        else
                        {
                            stringBuilder.Append(current);
                        }
                    }
                }
            }

            return stringBuilder.ToString();
        }

        // Similar to decrypted with empty chars being added in the end
        private bool BuildEncryptedTable(string plainText, int key)
        {
            int numCols = key;
            int numRows = (int)Math.Ceiling(plainText.Length / (double)key);

            encryptedTable = new char[numRows, numCols];

            int processedCount = 0;

            for (int row = 0; row < numRows; row++)
            {

                for (int col = 0; col < numCols; col++)
                {
                    if (processedCount < plainText.Length)
                    {
                        var current = plainText[processedCount];

                        if (current == ' ')
                        {
                            encryptedTable[row, col] = spaceChar;

                        }
                        else
                        {
                            encryptedTable[row, col] = plainText[processedCount];
                        }
                        processedCount++;
                    }
                    else
                    {
                        encryptedTable[row, col] = emptyChar;
                    }
                }
            }

            return true;
        }

        // Prints the encrypted table
        public void PrintEncryptedTable()
        {
            if (builtEncryptedTable == false || encryptedTable == null)
            {
                throw new NullReferenceException("Build the table before printing the table.");
            }
            Console.WriteLine(new string('-',100));
            Console.WriteLine("Encrypted Table");
            for (int row = 0; row < encryptedTable.GetLength(0); row++)
            {
                for (int col = 0; col < encryptedTable.GetLength(1); col++)
                {
                    Console.Write($"{encryptedTable[row, col]}\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine(new string('-', 100));

        }
    }
}
