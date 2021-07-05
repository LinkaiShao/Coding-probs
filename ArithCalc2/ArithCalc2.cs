using System;
using System.Collections.Generic;
using System.Text;

namespace ArithCalcV2
{

    public static class ArithCalcv2
    {
        public static double Run(string input)
        {
            string orig = input;
            string nonspace;
            try
            {
                ValidateInput(input, out nonspace);
                return Calc(input, orig);
            }
            catch (ArgumentException ex)
            {
                Console.Error.WriteLine($"error, {ex.Message}. try again");
            }
            catch(Exception other)
            {
                Console.Error.WriteLine("i dont know what is going on");
            }
            return 0;
            
        }
        // validation of input
        // before every arithmatic needs to be a number or right parenth )
        // needs ( or number after arithmatic
        // weird letters are also sources of error
        private static bool ValidateInput(string input, out string nonSpace)
        {
            List<char>nonSpaceInput = new List<char>();
            string cur = "";
            for(int i = 0; i < input.Length; i++)
            {
                var thisItem = input[i];
                if (thisItem == '(')
                {
                    cur = "leftP";
                    nonSpaceInput.Add(thisItem);
                }
                else if (thisItem == ')')
                {
                    cur = "rightP";
                    nonSpaceInput.Add(thisItem);
                }
                else if (char.IsNumber(thisItem))
                {
                    cur = "num";
                    nonSpaceInput.Add(thisItem);
                }
                else if (thisItem == '+' || thisItem == '-' || thisItem == '*' || thisItem == '/')
                {
                    // prior to arith has to be either right ) or num
                    if (cur != "rightP"||cur != "num") {
                        throw new WrongPresentationException($"wrong presentation by the user, at position {i} of {input}", input, i);
                        return false;
                    }
                    char temp = input[i + 1];
                    if (temp!= '(' || !char.IsNumber(temp)){
                        throw new WrongPresentationException($"wrong presentation by the user, at position {i} of {input}", input, i);
                        return false;
                    }
                    nonSpaceInput.Add(thisItem);
                    cur = "arith";
                }
                else if (thisItem !=' ')
                {
                    throw new StrangeCharacterException($"strange character {input[i]}appears at location {i} of your input {input}", input, thisItem, i);
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
        private static double Calc(string expression, string originalInput)
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
                        var expJ = expression[j];
                        if (expJ == '(')
                        {
                            break;
                        }
                        if (expJ == ')')
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
                    throw new ParenthNotMatchException($"left and right parenth don't match, more right parenth than left parenth of user input {originalInput}", originalInput);
                }
                if (expression.Contains('('))
                {
                    throw new ParenthNotMatchException($"left and right parenth don't match, more left parenth than right parenth of user input {originalInput}", originalInput);
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
            return Calc(leftPart + insideValue.ToString() + rightPart, originalInput);
        }
    }
}
