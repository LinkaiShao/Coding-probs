using System;

namespace GoogleInterviewParenthathese
{
    class Program
    {
        static void Main(string[] args)
        {
            string iput;
            Console.WriteLine("enter the line");
            iput = Console.ReadLine();
            Console.WriteLine("enter the brackets");
            string checks = Console.ReadLine();
            char[] charChecks = checks.ToCharArray();
            var res = new ParenthMatch();
            if(res.CheckParenthMathc(iput,charChecks))
            {
                Console.WriteLine("works");
            }
            else
            {
                Console.WriteLine("doesn't work");
            }
        }
    }
}
