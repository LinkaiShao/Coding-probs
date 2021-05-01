using System;
using System.Collections;
using System.Collections.Generic;
namespace GoogleInterviewParenthathese
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            Console.WriteLine("type in the expresison");
            input = Console.ReadLine();
            string input1;
            Console.WriteLine("type in the grouping characters");
            input1 = Console.ReadLine();
            Dictionary<char, char> pairs = new Dictionary<char, char>();
            for(int i = 0; i < input1.Length; i+=2)
            {
                pairs.Add(input1[i], input1[i + 1]);
            }
            if(TheRightSolution.CheckMultipleParenth(input,pairs))
            {
                Console.WriteLine("works");
            }
            else
            {
                Console.WriteLine("don't work");
            }
            
        }
    }
}
