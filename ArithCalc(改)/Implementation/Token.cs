//------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="Token.cs" company="LiXinXu">
//     Copyright (c) LiXinXu. All rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------------------------------------------

namespace SmartCalculator.Implementation
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Expression Token
    /// </summary>
    public class Token
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Token" /> class.
        /// </summary>
        /// <param name="type">token type</param>
        /// <param name="start">start position in expression</param>
        /// <param name="length">token length</param>
        public Token(TokenType type, int start, int length)
        {
            this.Type = type;
            this.Start = start;
            this.Length = length;
        }

        /// <summary>
        /// Gets token type
        /// </summary>
        public TokenType Type { get; }

        /// <summary>
        /// Gets start position in expression string
        /// </summary>
        public int Start { get; }

        /// <summary>
        /// Gets token length, in character
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// Parse tokens from expression string
        /// </summary>
        /// <param name="expression">expression string</param>
        /// <returns>token list</returns>
        public static IReadOnlyList<Token> Parse(string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                throw new ArgumentException("Expression should not be null or empty");
            }

            LinkedList<Token> list = new LinkedList<Token>();
            int position = 0;
            int length = expression.Length;
            while (position < length)
            {
                while ((position < length) && char.IsWhiteSpace(expression[position]))
                {
                    position++;
                }

                if (position >= length)
                {
                    break;
                }
                // white space eliminated

                Token token;
                char c = expression[position];
                switch (c)
                {
                    case '(':
                        token = new Token(TokenType.LeftParentheses, position, 1);
                        position++;
                        break;
                    case ')':
                        token = new Token(TokenType.RightParentheses, position, 1);
                        position++;
                        break;
                    case '+':
                        token = new Token(TokenType.Add, position, 1);
                        position++;
                        break;
                    case '-':
                        token = new Token(TokenType.Minus, position, 1);
                        position++;
                        break;
                    case '*':
                        token = new Token(TokenType.Multiplication, position, 1);
                        position++;
                        break;
                    case '/':
                        token = new Token(TokenType.Division, position, 1);
                        position++;
                        break;
                    case ',':
                        token = new Token(TokenType.Comma, position, 1);
                        position++;
                        break;
                    default:
                        if (char.IsNumber(c))
                        {
                            // parse number. it is the combination of numbers and '.'
                            int start = position;
                            while (true)
                            {
                                position++;
                                if (position >= length)
                                {
                                    break;
                                }

                                c = expression[position];
                                if (!char.IsNumber(c) && (c != '.'))
                                {
                                    break;
                                }
                            }

                            token = new Token(TokenType.Number, start, position - start);
                        }
                        else if (char.IsLetter(c))
                        {
                            // parse symbol. it is the combination of letters and '.'
                            int start = position;
                            while (true)
                            {
                                position++;
                                if (position >= length)
                                {
                                    break;
                                }

                                c = expression[position];
                                if (!char.IsLetterOrDigit(c) && (c != '_'))
                                {
                                    break;
                                }
                            }

                            token = new Token(TokenType.Symbol, start, position - start);
                        }
                        else
                        {
                            throw new ArgumentException($"Invalid character '{c}' at position {position}");
                        }

                        break;
                }

                list.AddLast(token);
            }

            if (list.Count < 1)
            {
                throw new ArgumentException("Expression should not be blank");
            }
            // linked list to array
            Token[] tokens = new Token[list.Count];
            LinkedListNode<Token> item = list.First;
            for (int i = 0; i < list.Count; i++)
            {
                tokens[i] = item.Value;
                item = item.Next;
            }

            return tokens;
        }
    }
}
