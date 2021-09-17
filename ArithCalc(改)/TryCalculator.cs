using System;
using System.Collections.Generic;
using System.Text;
using SmartCalculator.Implementation;

namespace SmartCalculator
{
    /// <summary>
    /// has function name, its contents as string and list of characters as its parameters
    /// </summary>
    public class FunctionDefinition
    {
        string functionName;
        List<char> functionParams;
        public Dictionary<char, int> paramsToSequence = new Dictionary<char, int>();
        List<List<int>> paramLocations;// where each parameters have occured in the function content
        public IReadOnlyList<Token> Tokens;
        public string contents;
        public FunctionDefinition(string name, List<char> functionParams, string contents)
        {
            this.functionName = name;
            this.functionParams = functionParams;
            this.contents = contents;
            Token.Parse(contents);
            // find the param locations
            for(int i = 0; i < functionParams.Count;i++)
            {
                paramsToSequence.Add(functionParams[i], i);
            }
            
        }
    }
    public class TryCalculator : ICalculator
    {
        public static Dictionary<string, FunctionDefinition> allFunctions = new Dictionary<string, FunctionDefinition>();
        public static void AddFunction(string functionName, List<char> parameters, string functionDefinition)
        {
            var thisFunction = new FunctionDefinition(functionName, parameters, functionDefinition);
            allFunctions.Add(functionName,thisFunction);
        }
        /// <summary>
        /// Low rand operations
        /// </summary>
        private const TokenType LowRandOperations = TokenType.Add | TokenType.Minus;

        /// <summary>
        /// High rank operations
        /// </summary>
        private const TokenType HighRankOperations = TokenType.Multiplication | TokenType.Division;

        /// <summary>
        /// Operation Token Type
        /// </summary>
        private const TokenType OperationTokenTypes = LowRandOperations | HighRankOperations;

        /// <summary>
        /// Parentheses Token Type
        /// </summary>
        private const TokenType ParenthesesTokenTypes = TokenType.LeftParentheses | TokenType.RightParentheses;

        /// <summary>
        /// Data token types
        /// </summary>
        private const TokenType DataTokenTypes = TokenType.Number | TokenType.Symbol;
        private static IExpression Compile(string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                throw new ArgumentException($"{nameof(expression)} should not be null or empty");
            }

            IReadOnlyList<Token> tokens = Token.Parse(expression);

            int start = 0;
            IExpression result = Compile(
                expression,
                tokens,
                ref start,
                null,
                null);
            if (start < tokens.Count)
            {
                throw new ArgumentException($"Could not parse the whole expression '{expression}'. {tokens.Count - start} token(s) left");
            }

            if (result == null)
            {
                throw new ArgumentException($"Expression '{expression}' does not have content to calculate");
            }

            return result;
        }
        public double Calculate(string expression)
        {
            IExpression parsedExpression = Compile(expression);
            return parsedExpression.Calculate();
        }
        /// <summary>
        /// might wanna change back to private, current function has to be passed in since if this is doing the parameter values, it need to check which parameter to assign the value to.
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="tokens"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static IExpression Compile(string expression, IReadOnlyList<Token> tokens, ref int start, List<double> parameterValues, FunctionDefinition currentFunction )
        {
            // find the first two operations from start
            // find first 3 numbers from start, value expressions
            // make expressions with value expression function with (token, expression);
            IExpression startingExpression = null;
            Token? firstOperation = null;
            IExpression secondExpression = null;
            Token? secondOperation = null;
            IExpression thirdExpression = null;
            while (true)
            {
                Token currentToken = new Token(TokenType.Add, 0, 0);
                if (start < tokens.Count)
                {
                    currentToken = tokens[start];
                }
                switch (currentToken.Type)
                {
                    case TokenType.Symbol:
                        {
                            ValueExpression result;
                            // need to know whether if this is a function or parameter
                            FunctionDefinition thisFunc;
                            var t = expression.Substring(currentToken.Start, currentToken.Length);
                            if(TryCalculator.allFunctions.TryGetValue(t, out thisFunc)) // this is a function starter with function name
                            {
                                List<double> paramsValues = new List<double>();
                                start++; // move to ( 
                                if (tokens[start].Type != TokenType.LeftParentheses)
                                {
                                    throw new ArgumentException($"invalid way of using function, put ( after function at {tokens[start].Start} position");
                                }
                                // get every params
                                for (int i = 0; i < thisFunc.paramsToSequence.Count; i++)
                                {
                                    start++; // move past ( or , 
                                    var thisParamValue = TryCalculator.Compile(expression, tokens, ref start, parameterValues, thisFunc).Calculate();
                                    paramsValues.Add( thisParamValue);
                                }
                                FunctionExpression thisFuncExpression = new FunctionExpression(thisFunc, paramsValues);
                                result = new ValueExpression(thisFuncExpression.Calculate());
                                start++;
                            }
                            else // this is parameters, as in the x y for function representation such as x*x+y
                            {
                                // assign the value given to the parameter
                                result = new ValueExpression(parameterValues[currentFunction.paramsToSequence[t[0]]]); // take the parameters from current function
                            }
                            FindAndAssignCurrentHandlingExpression(ref startingExpression, ref secondExpression, ref thirdExpression, result);
                            break;
                        }
                    case TokenType.LeftParentheses:
                        {
                            start++; // move one spot forward
                            var result = Compile(expression, tokens,ref start,parameterValues,currentFunction); // do what is inside of the expression
                            FindAndAssignCurrentHandlingExpression(ref startingExpression, ref secondExpression, ref thirdExpression, result);
                            start++;
                            break;
                        }
                    case TokenType.Number:
                        {
                            var currentExp = new ValueExpression(currentToken, expression);
                            FindAndAssignCurrentHandlingExpression(ref startingExpression, ref secondExpression, ref thirdExpression, currentExp);
                            start++; // push forward 1
                            break;
                        }
                }
                // first run through should be at 1st operation
                if (firstOperation == null) // first run through
                {
                    if (EndOfGroup(tokens, start, ref currentToken)) // number by itself, or the operation is not +=*/
                    {
                        break;
                    }
                    // get the first operation and the end of this while run through
                    firstOperation = currentToken;
                    start++;
                }
                else if(!EndOfGroup(tokens, start, ref currentToken) && secondOperation==null) // already have first operation, second operation is empty. ready to do second operation
                {
                    secondOperation = currentToken;
                    start++;
                }
                if(thirdExpression != null) // have three, reduce them to two expressions and one operation
                {
                    ThreeNumberOperation(ref startingExpression, ref secondExpression,ref thirdExpression, ref firstOperation, ref secondOperation);
                }
                else if(firstOperation != null && secondExpression!=null && secondOperation ==null) // have two and two only, make this into one
                {
                    startingExpression = CreateBinaryExpression(startingExpression, secondExpression, firstOperation.Type);
                    firstOperation = null;
                    secondExpression = null;
                }
                
                

                
            }
            return startingExpression;
            
        }
        /// <summary>
        /// find the expression that you are currently handling and assign token to it
        /// </summary>
        /// <param name="expression1"></param>
        /// <param name="expression2"></param>
        /// <param name="expression3"></param>
        /// <param name="currentToken"></param>
        /// <param name="expression"></param>
        private static void FindAndAssignCurrentHandlingExpression(ref IExpression expression1, ref IExpression expression2, ref IExpression expression3,IExpression toBeAssignedExpression)
        {
            // if there is no starting expression
            if (expression1 == null)
            {
                expression1 = toBeAssignedExpression;
            }
            else if (expression2 == null) // you now have an expression and an operator, expect a second expression
            {
                expression2 = toBeAssignedExpression;
            }
            else
            {
                expression3 = toBeAssignedExpression;
            }
        }
        /// <summary>
        /// Take three expressions, do two of them because of operation hierarchy, at the end there will be only expression 1, expression 2 and first operation
        /// </summary> 
        /// <param name="expression1"></param>
        /// <param name="expression2"></param>
        /// <param name="expression3"></param>
        /// <param name="operation1"></param>
        /// <param name="operation2"></param>
        private static void ThreeNumberOperation(ref IExpression expression1, ref IExpression expression2, ref IExpression expression3, ref Token operation1, ref Token operation2)
        {
            // compare the two operations   
            if(IsOneOfTypes(operation1.Type,HighRankOperations) || IsOneOfTypes(operation2.Type,LowRandOperations)) // do the prior
            {
                expression1 = CreateBinaryExpression(expression1, expression2, operation1.Type);
                expression2 = expression3;
                expression3 = null;
                operation1 = operation2;
            }
            else // do the latter
            {
                expression2 = CreateBinaryExpression(expression2, expression3, operation2.Type);
                expression3 = null;
            }
            operation2 = null;
            return;
        }
        private static bool IsOneOfTypes(TokenType typeToCheck, TokenType allowedTypes)
        {
            return (typeToCheck & allowedTypes) != 0;
        }
        /// <summary>
        /// Checks whether if the current place is outside of the string or at the end of a phrase, which means either nothing or right parenth
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="start"></param>
        /// <param name="currentToken"></param>
        /// <returns></returns>
        private static bool EndOfGroup(IReadOnlyList<Token> tokens, int start, ref Token currentToken)
        {
            if (start >= tokens.Count)
            {
                return true;
            }
            currentToken = tokens[start];
            if (currentToken.Type != TokenType.Add && currentToken.Type != TokenType.Minus && currentToken.Type != TokenType.Multiplication && currentToken.Type != TokenType.Division)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Utility to create binary expression
        /// </summary>
        /// <param name="left">left expression</param>
        /// <param name="right">right expression</param>
        /// <param name="operationType">operation type</param>
        /// <returns>binary expression</returns>
        private static IExpression CreateBinaryExpression(
            IExpression left,
            IExpression right,
            TokenType operationType)
        {
            Func<double, double, double> function;
            switch (operationType)
            {
                case TokenType.Add:
                    function = AddFunction;
                    break;
                case TokenType.Minus:
                    function = MinusFunction;
                    break;
                case TokenType.Multiplication:
                    function = MultiplicationFunction;
                    break;
                default:
                    function = DivisionFunction;
                    break;
            }

            IExpression resultExpression;
            ValueExpression leftValueExpresion = left as ValueExpression;
            ValueExpression rightValueExpression = right as ValueExpression;
            if ((leftValueExpresion != null) && (rightValueExpression != null))
            {
                double leftValue = left.Calculate();
                double rightValue = right.Calculate();
                resultExpression = new ValueExpression(function(leftValue, rightValue));
            }
            else
            {
                resultExpression = new BinaryExpression(function, left, right);
            }

            return resultExpression;
        }

        /// <summary>
        /// Add operation
        /// </summary>
        /// <param name="left">left value</param>
        /// <param name="right">right value</param>
        /// <returns>calculated result</returns>
        private static double AddFunction(double left, double right)
        {
            return left + right;
        }

        /// <summary>
        /// Minus operation
        /// </summary>
        /// <param name="left">left value</param>
        /// <param name="right">right value</param>
        /// <returns>calculated result</returns>
        private static double MinusFunction(double left, double right)
        {
            return left - right;
        }

        /// <summary>
        /// Multiplication operation
        /// </summary>
        /// <param name="left">left value</param>
        /// <param name="right">right value</param>
        /// <returns>calculated result</returns>
        private static double MultiplicationFunction(double left, double right)
        {
            return left * right;
        }

        /// <summary>
        /// Division operation
        /// </summary>
        /// <param name="left">left value</param>
        /// <param name="right">right value</param>
        /// <returns>calculated result</returns>
        private static double DivisionFunction(double left, double right)
        {
            return left / right;
        }
    }
}
