using System;
using System.Collections.Generic;
using System.Text;

namespace RSAAlgorithm
{
    public class PrimeFactorization
    {
        public static bool IsPrime(int num)
        {
            if (num == 2)
            {
                return true;
            }
            else if (num < 2)
            {
                return false;
            }
            else if (num % 2 == 0)
            {
                return false;
            }
            else
            {
                for (int i = 3; i < (int)Math.Ceiling(Math.Sqrt(num)); i+=2)
                {
                    if (num % i == 0)
                    {
                        return false;
                    }
                }

                return true;
            }
        }
        public static List<int> GetFactors(int number)
        {
            List<int> factors = new List<int>();

            GetFactors(number, factors);

            return factors;
        }

        private static void GetFactors(int number, List<int> factors)
        {
            if (number <= 1)
            {
                return;
            }

            bool found = false;
            int i = 2;
            for (; i < number; i++)
            {
                if (number % i == 0 && IsPrime(i))
                {
                    factors.Add(i);
                    found = true;
                    break;
                }

            }

            if (!found)
            {
                factors.Add(number);
                return;
            }
            else
            {
                GetFactors(number / i, factors);

            }

        }
    }
}
