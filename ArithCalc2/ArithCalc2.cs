using System;
using System.Collections.Generic;
using System.Text;

namespace ArithCalcV2
{

    internal static class ArithCalcv2
    {
        public static double Run(string input)
        {
            string nonspace;
            ValidateInput(input, out nonspace);
            return Calc(input);
        }
        private static bool ValidateInput(string input, out string nonSpace)
        {
            List<char>nonSpaceInput = new List<char>();
            string cur = "";
            for(int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(')
                {
                    cur = "leftP";
                    nonSpaceInput.Add(input[i]);
                }
                else if (input[i] == ')')
                {
                    cur = "rightP";
                    nonSpaceInput.Add(input[i]);
                }
                else if (char.IsNumber(input[i]))
                {
                    cur = "num";
                    nonSpaceInput.Add(input[i]);
                }
                else if (input[i] == '+' || input[i] == '-' || input[i] == '*' || input[i] == '/')
                {
                    // prior to arith has to be either right ) or num
                    if (cur != "rightP"||cur != "num") {
                        Console.WriteLine("error occurs at position, need either number of ) before arithmatic {0}", i);
                        nonSpace = "";
                        return false;
                    }
                    char temp = input[i + 1];
                    if (temp!= '(' || !char.IsNumber(temp)){
                        nonSpace = "";
                        Console.WriteLine("error occurs at position, need either number of ( after arithmatic {0}", i);
                        return false;
                    }
                    nonSpaceInput.Add(input[i]);
                    cur = "arith";
                }
                else if (input[i]!=' ')
                {
                    nonSpace = "";
                    Console.WriteLine("error occurs at position {0}, strange letter found", i);
                    return false;
                }
            }
            nonSpace = nonSpaceInput.ToString();
            return true;
        }
        // finds the left symbol, right symbol, left number and right number

        private static Dictionary<char, Func<double, double, double>> BasicArithmatic = new Dictionary<char, Func<double, double, double>>()
        {
            { '+', Plus},
            { '-',Minus},
            { '*', Times},
            { '/', Divide}
        };


        private static double Plus(double item1, double item2)
        {
            return item1 + item2;
        }
        private static double Minus(double item1, double item2)
        {
            return item1 - item2;
        }
        private static double Times(double item1, double item2)
        {
            return item1 * item2;
        }
        private static double Divide(double item1, double item2)
        {
            return item1 / item2;
        }
        // this one supports () as first priority then */, similar to a normal calculator
        private static double Calc(string expression)
        {
            // handle the things inside of () first then */ then +-
            // find the deepest level parenth
            bool deepest = false;
            int leftParenthStart = -1;
            int rightParenthStart = -1;
            // find the deepest level parenthethes
            // if no deepest level parenth left and right are -1
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '(')
                {
                    // for this level to be the deepest level parenth, left parenth ( needs to match with a right parenth without any left parenth in the middle
                    for (int j = i + 1; j < expression.Length; j++)
                    {
                        if (expression[j] == '(')
                        {
                            break;
                        }
                        if (expression[j] == ')')
                        {
                            deepest = true;
                            rightParenthStart = j;
                            break;
                        }
                    }
                }
                // found the position of the left parenth so break 
                if (deepest)
                {
                    leftParenthStart = i;
                    break;
                }
            }
            // there is no parenth
            if (leftParenthStart == -1)
            {
                // extra right parenth when there are no left parenth present
                if (expression.Contains(')'))
                {
                    Console.WriteLine("left and right parenth don't match, more right parenth than left parenth, exiting program");
                    System.Environment.Exit(0);
                }
                if (expression.Contains('('))
                {
                    Console.WriteLine("left and right parenth don't match, more left parenth than left parenth, exiting program");
                    System.Environment.Exit(0);
                }
                return SimpleCalc.Calc(expression);
            }
            // have the deepest level parenth, which means that the inside has no parenthethese
            double insideValue = SimpleCalc.Calc(expression.Substring(leftParenthStart + 1, rightParenthStart - leftParenthStart - 1));
            // turn this into a string and pass it into the funciton again
            // get the left part and right part + inside value in the center
            // get rid of the parenth
            string leftPart = expression.Substring(0, leftParenthStart);
            string rightPart = expression.Substring(rightParenthStart + 1, expression.Length - rightParenthStart - 1);
            return Calc(leftPart + insideValue.ToString() + rightPart);
        }
    }
}
