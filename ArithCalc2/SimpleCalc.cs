using System;
using System.Collections.Generic;
using System.Text;

namespace ArithCalcV2
{
    static class SimpleCalc
    {
        private static void AllFactorsPosition(string expression, int centerSymbolPosition, out int leftSymbolPos, out int rightSymbolPos, out double leftNum, out double rightNum)
        {
            leftSymbolPos = -1;
            rightSymbolPos = expression.Length;
            // find the symbols to the left and right 
            // left
            for (int j = 0; j < centerSymbolPosition; j++)
            {
                if (expression[j] == '+' || expression[j] == '-' || expression[j] == '*' || expression[j] == '/')
                {
                    leftSymbolPos = j;
                    break;
                }
            }
            // right
            for (int j = centerSymbolPosition + 1; j < expression.Length; j++)
            {
                if (expression[j] == '+' || expression[j] == '-' || expression[j] == '*' || expression[j] == '/')
                {
                    rightSymbolPos = j;
                    break;
                }
            }
            // find the numbers which should be between the left right symbol and the center symbol
            // if there is no more left symbol then left symbol was initialized at 0
            // if there is no more right ysmbols then right symbol is automatically the last thing in the expression string
            leftNum = double.Parse(expression.Substring(leftSymbolPos + 1, centerSymbolPosition - leftSymbolPos - 1));
            rightNum = double.Parse(expression.Substring(centerSymbolPosition + 1, rightSymbolPos - centerSymbolPosition - 1));
            return;
        }
        /// <summary>
        /// handle the */ first then +-
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static double Calc (string expression)
        {
            // returns an expression's value without parenth
            char symbol = ' ';
            int leftSymbolPos = 0;
            int rightSymbolPos = expression.Length - 1;
            double leftNum;
            double rightNum;
            string leftstring = "";
            string rightstring = "";
            string centerstring = "";
            //find the first *or/
            for (int i = 0; i < expression.Length; i++)
            {
                //find the first *or/
                if (expression[i] == '*' || expression[i] == '/')
                {
                    symbol = expression[i];

                    AllFactorsPosition(expression, i, out leftSymbolPos, out rightSymbolPos, out leftNum, out rightNum);
                    if (expression[i] == '*')
                    {
                        centerstring = (leftNum * rightNum).ToString();
                    }
                    // else the expression = /
                    else
                    {
                        centerstring = (leftNum / rightNum).ToString();
                    }
                    // put it back into the original expression
                    //left side of the string is from start to left side symbol
                    if (leftSymbolPos > 0)
                    {
                        leftstring = expression.Substring(0, leftSymbolPos + 1);
                    }
                    // right side of string is the right side symbol till the end
                    rightstring = expression.Substring(rightSymbolPos, expression.Length - rightSymbolPos);
                    return (Calc(leftstring + centerstring + rightstring));
                }

            }
            // there are no more */ so we handle +-
            if (symbol == ' ')
            {
                //find the first *or/
                for (int i = 0; i < expression.Length; i++)
                {
                    if (expression[i] == '+' || expression[i] == '-')
                    {
                        symbol = expression[i];

                        AllFactorsPosition(expression, i, out leftSymbolPos, out rightSymbolPos, out leftNum, out rightNum);
                        if (expression[i] == '+')
                        {
                            centerstring = (leftNum + rightNum).ToString();
                        }
                        // else the expression = -
                        else
                        {
                            centerstring = (leftNum - rightNum).ToString();
                        }
                        // put it back into the original expression
                        //left side of the string is from start to left side symbol
                        if (leftSymbolPos > 0)
                        {
                            leftstring = expression.Substring(0, leftSymbolPos + 1);
                        }
                        // right side of string is the right side symbol till the end
                        rightstring = expression.Substring(rightSymbolPos, expression.Length - rightSymbolPos);
                        return (Calc(leftstring + centerstring + rightstring));
                    }
                }
            }
            // no more symbols, meaning there is only 1 number as the expression
            return double.Parse(expression);

        }
        
    }
}
