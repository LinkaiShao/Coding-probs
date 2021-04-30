using System;

namespace GoogleInterviewParenthathese
{
    class Program
    {
        static void Main(string[] args)
        {
            string iput;
            
            iput = Console.ReadLine();
            var res = new ParenthMatch();
            if(res.CheckParenthMatch(iput))
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
