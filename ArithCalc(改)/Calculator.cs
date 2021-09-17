//------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="Calculator.cs" company="LiXinXu">
//     Copyright (c) LiXinXu. All rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------------------------------------------

namespace SmartCalculator
{
    using System;
    using System.Collections.Generic;
    using SmartCalculator.Implementation;

    /// <summary>
    /// <see cref="ICalculator"/> implementation
    /// </summary>
    public class Calculator : ICalculator
    {
        #region Data
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
        #endregion Data

        #region Implementation
        /// <inheritdoc cref="ICalculator.Calculate(string)"/>
        public double Calculate(string expression)
        {
            IExpression parsedExpression = Compile(expression);
            return parsedExpression.Calculate();
        }
        #endregion Implementation

        #region Utility
        /// <summary>
        /// Compile expression to expression
        /// </summary>
        /// <param name="expression">expression string</param>
        /// <returns>compiled function</returns>
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
                ref start);
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

        /// <summary>
        /// Compiler expression
        /// </summary>
        /// <param name="expression">expression string</param>
        /// <param name="tokens">tokens of the expression</param>
        /// <param name="start">start location in the token</param>
        /// <returns>compiled expression</returns>
        private static IExpression Compile(
            string expression,
            IReadOnlyList<Token> tokens,
            ref int start)
        {
            int tokenCount = tokens.Count;
            IExpression currentExpression = null;
            IExpression previousExpressions = null;
            TokenType previousOperation = TokenType.Add;
            IExpression holdExpressions = null;
            TokenType holdOperation = TokenType.Add;

            while (true)
            {
                Token token = tokens[start];
                switch (token.Type)
                {
                    case TokenType.Minus: // deal with some expression like -123 or -(1+2)
                        {
                            start++;
                            if (start >= tokenCount)
                            {
                                throw new ArgumentException($"Expect more data after operator '-' (character position: {token.Start}). However got the end of expression");
                            }

                            IExpression followExpression = Compile(
                                expression,
                                tokens,
                                ref start);
                            ValueExpression valueExpression = followExpression as ValueExpression;
                            if (valueExpression != null)
                            {
                                double value = -1 * valueExpression.Calculate();
                                currentExpression = new ValueExpression(value);
                            }
                            else
                            {
                                currentExpression = new BinaryExpression(MultiplicationFunction, new ValueExpression(-1.0D), followExpression);
                            }

                            break;
                        }

                    case TokenType.LeftParentheses:
                        {
                            start++;
                            if (start >= tokenCount)
                            {
                                throw new ArgumentException($"Expect more data after operator '(' (character position: {token.Start}). However got the end of expression");
                            }

                            currentExpression = Compile(
                                expression,
                                tokens,
                                ref start);
                            // start will become closetoken at the end, right parenth
                            if (start >= tokenCount)
                            {
                                throw new ArgumentException($"Expect '(' to match ')' at {token.Start}. However got the end of expression");
                            }

                            Token closeToken = tokens[start];
                            if (closeToken.Type != TokenType.RightParentheses)
                            {
                                throw new ArgumentException($"Expect close parentheses ')' to match '(' (character position: {token.Start}). However got the end of expression");
                            }

                            start++;
                            break;
                        }

                    case TokenType.Number:
                        {
                            currentExpression = new ValueExpression(token, expression);
                            start++;
                            break;
                        }

                    case TokenType.Symbol:
                        {
                            throw new NotSupportedException("Function, Variable and Parameter will be support later");
                        }
                }

                if (start >= tokenCount)
                {
                    break;
                }

                TokenType nextOperation = tokens[start].Type;
                if (!IsOneOfTypes(nextOperation, OperationTokenTypes))
                {
                    break;
                }

                start++;
                if (previousExpressions == null)
                {
                    previousExpressions = currentExpression;
                    previousOperation = nextOperation;
                }
                else if (holdExpressions == null)
                {
                    if (IsOneOfTypes(previousOperation, HighRankOperations) || IsOneOfTypes(nextOperation, LowRandOperations))
                    {
                        previousExpressions = CreateBinaryExpression(previousExpressions, currentExpression, previousOperation);
                        previousOperation = nextOperation;
                        // if previous is higher or next is lower, do the previous, compute the previous and set it as the next previous, previous operation is the one next to previous
                        // if previous is higher or equals next operation, do the previous
                        // if next is lower or equals previous, do previous
                        // at last move previous to next
                    }
                    else
                    {
                        holdExpressions = currentExpression;
                        holdOperation = nextOperation;
                        // hold next
                    }
                }
                else // ??? when does it go here
                {
                    holdExpressions = CreateBinaryExpression(holdExpressions, currentExpression, holdOperation);
                    if (IsOneOfTypes(nextOperation, HighRankOperations))
                    {
                        holdOperation = nextOperation;
                    }
                    else
                    {
                        previousExpressions = CreateBinaryExpression(previousExpressions, holdExpressions, previousOperation);
                        previousOperation = nextOperation;
                        holdExpressions = null;
                    }
                }
            }
            // nothing left, next is either right parenth or nothing
            if (holdExpressions != null)
            {
                currentExpression = CreateBinaryExpression(holdExpressions, currentExpression, holdOperation);
            }

            if (previousExpressions != null)
            {
                currentExpression = CreateBinaryExpression(previousExpressions, currentExpression, previousOperation);
            }

            return currentExpression;
        }

        /// <summary>
        /// Check token type is one of types
        /// </summary>
        /// <param name="typeToCheck">type to check</param>
        /// <param name="allowedTypes">allowed types</param>
        /// <returns>true if type is one of given types</returns>
        private static bool IsOneOfTypes(TokenType typeToCheck, TokenType allowedTypes)
        {
            return (typeToCheck & allowedTypes) != 0;
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
        #endregion Utility
    }
}
