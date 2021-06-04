using System;

namespace Fib
{
    class Program
    {
        static int Fib(int input)
        {
            if(input < 0)
            {
                throw new ArgumentOutOfRangeException("input", "Input must be >=0");
            }
            int[] f = new int[input];
            for(int i = 0; i < f.Length; i++)
            {
                f[i] = -1;
            }
            return DumbFib(input, f);
        }
        static int DumbFib(int input, int[] fibArray)
        {
            // this means that it has been assigned
            if (fibArray[input] != -1)
            {
                return fibArray[input];
            }
            if (input == 0)
            {
                fibArray[input] = 0;
            }
            else if (input == 1)
            {
                fibArray[input] = 1;
            }
            else
            {
                fibArray[input] = DumbFib(input - 1, fibArray) + DumbFib(input - 2, fibArray);
            }
            return fibArray[input];
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
