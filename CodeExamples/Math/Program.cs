using System;
using System.Linq;

namespace MathExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            //var factors = ModuloMath.FindAllFactors(10000);

            //foreach (var factor in factors)
            //{
            //    Console.WriteLine(factor);
            //}

            //var gcd = ModuloMath.FindGCF(12, 24);

            //Console.WriteLine(gcd);

            //print all combinations of relatively prime numbers from 1 to 100

            for (int i = 1; i < 100; i++)
            {
                for(int j = i; j < 100; j++)
                {
                    if (ModuloMath.AreRelativelyPrime(i, j))
                    {
                        Console.WriteLine($"{i} is relatively prime with {j}");
                    }

                }

            }

            ////foreach (var pair in ModuloMath.GetAllUniqueRelativelyPrimePairs(0,100))
            ////{
            ////    Console.WriteLine($"{pair} are relatively prime");
            ////}


            //for (int i = 0; i < 14; i++)
            //{
            //    Console.WriteLine(ModuloMath.Factorial(i));
            //}

            //Console.WriteLine(ModuloMath.ModularInverse(5, 7));

            //Console.WriteLine(ModuloMath.ModularInverse(3, 11));



            // Print the first 10 Mersenne prime numbers
            //int count = 1;
            //foreach (var (pow, number) in ModuloMath.GetNMersennePrimeNumbers(8))
            //{
            //    Console.WriteLine($"Mersenne Prime Number {count} is (2 ^ {pow}) - 1 = {number}");
            //    count++;
            //}

            //Console.WriteLine(int.MaxValue);

            //for (int i = 0; i < 30000; i++)
            //{
            //    if (ModuloMath.IsPrime(i))
            //    {
            //        Console.WriteLine(i);
            //    }
            //}

            //var combos = StringFunctions.Permute("halo");
            //var unique = combos.Distinct().ToList();

            //var recanagrams = StringFunctions.Permute("karan").Distinct().ToList();

            ;

        }
    }
}
