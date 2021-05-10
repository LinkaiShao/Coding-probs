using System;

namespace ArithmaticExpression
{
    class Program
    {
        static void Main(string[] args)
        {
            ArithmaticCalc calc = new ArithmaticCalc();
            string expression = Console.ReadLine();
            Console.WriteLine(calc.ArithamticCalc2(expression));
        }
    }
}
