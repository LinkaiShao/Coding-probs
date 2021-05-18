using System;
using System.Collections.Generic;
using System.Text;

namespace ArithCalcV2
{
    // has to have parenthethes and calculate the string given
    static class ArithCalc1
    {
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
            return item1 -item2;
        }
        private static double Times(double item1, double item2)
        {
            return item1*item2;
        }
        private static double Divide(double item1, double item2)
        {
            return item1 / item2;
        }
        // each time, find the deepest level parenthethes and then condense the problem
        public static double Calc(string expression)
        {
            bool deepest = false;
            int leftParenthStart = -1;
            int rightParenthStart = -1;
            // find the deepest level parenthethes
            for(int i = 0; i < expression.Length; i++)
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
            // not having any parenthethese means that you have a string of the answer as expression
            if (leftParenthStart == -1)
            {
                return double.Parse(expression);
            }
            // left parenth pos has been found and so has right parenth position
            // calculate the contents inside
            // find the symbol first +-*/
            int symbolPos = -1;
            for (int i = leftParenthStart; i < rightParenthStart; i++)
            {
                if (expression[i] == '+' || expression[i] == '-' || expression[i] == '*' || expression[i] == '/')
                {
                    symbolPos = i;
                    break;
                }
            }
            // first number and second number
            double firstNum, secondNum, result;
            firstNum = double.Parse(expression.Substring(leftParenthStart + 1, symbolPos - leftParenthStart - 1));
            secondNum= double.Parse(expression.Substring(symbolPos + 1, rightParenthStart-symbolPos - 1));
            result = BasicArithmatic[expression[symbolPos]](firstNum, secondNum);
            // convert this into string and pass it in
            //First part is the first half of the expression
            string firstHalf = expression.Substring(0, leftParenthStart);
            // Second half is the part after right parenth
            string secondHalf = expression.Substring(rightParenthStart+1, expression.Length - rightParenthStart - 1);
            return Calc(firstHalf + result.ToString() + secondHalf);

        }
    }
}
