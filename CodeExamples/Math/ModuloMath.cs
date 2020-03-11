using System;
using System.Collections.Generic;
using System.Text;

namespace MathExamples
{
    class ModuloMath
    {
        public static bool IsPrime(int num)
        {
            if (num < 2)
            {
                return false;
            }
            else if (num == 2)
            {
                return true;
            }
            else if (num % 2 == 0)
            {
                return false;
            }

            for (int i = 3; i <= Math.Ceiling(Math.Sqrt(num)); i++)
            {
                if (num % i == 0)
                {
                    return false;
                }
            }

            return true;
        }
        public static int ModularInverse(int num, int mod)
        {
            if (AreRelativelyPrime(num, mod))
            {
                // Find some i where (num * i) % mod == 1

                for (int i = 0; i < mod; i++)
                {
                    int product = num * i;

                    if (product % mod == 1)
                    {
                        return i;
                    }

                }
            }

            throw new Exception($"{num} and {mod} are not relatively prime.");
        }
        public static int Factorial(int num)
        {
            if (num < 0)
            {
                throw new ArgumentOutOfRangeException("Number must be a positive number.");
            }
            int result = 1;

            while (num > 0)
            {
                result = result * num;
                num--;
            }

            return result;
        }

        public static Dictionary<int, int> GetNMersennePrimeNumbers(int n)
        {
            Dictionary<int, int> mersennePrimeNumbers = new Dictionary<int, int>();

            int pow = 1;
            for (int i = 0; i < n; i++)
            {
                int val = (int)Math.Pow(2, pow) - 1;

                while (!IsPrime(val))
                {
                    pow++;
                    val = (int)Math.Pow(2, pow) - 1;
                }

                mersennePrimeNumbers.Add(pow, val);
                pow++;

            }

            return mersennePrimeNumbers;

        }

        public static List<Pair> GetAllUniqueRelativelyPrimePairs(int start = 0, int end = 100)
        {
            List<Pair> pairs = new List<Pair>();

            Pair temp;
            for (int i = start; i <= end; i++)
            {
                for (int j = start; j <= end; j++)
                {
                    if (i != j)
                    {
                        temp = new Pair(i, j);

                        if (!pairs.Contains(temp) && AreRelativelyPrime(i, j))
                        {
                            pairs.Add(temp);
                        }
                    }

                }

            }
            return pairs;
        }
        public static bool AreRelativelyPrime(int x, int y)
        {
            return FindGCF(x, y) == 1;
        }

        public static int FindGCF(int x, int y)
        {
            int gcd = 1;

            for (int i = 1; i <= x && i <= y; i++)
            {
                if (x % i == 0 && y % i == 0)
                {
                    gcd = i;
                }
            }

            return gcd;
        }
        public static List<int> FindAllFactors(int num)
        {
            List<int> factors = new List<int>();

            for (int i = 1; i <= num; i++)
            {
                if (num % i == 0)
                {
                    factors.Add(i);
                }
            }

            return factors;
        }
    }
}
