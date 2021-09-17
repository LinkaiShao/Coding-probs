//------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="ValueExpression.cs" company="LiXinXu">
//     Copyright (c) LiXinXu. All rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------------------------------------------

namespace SmartCalculator.Implementation
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Expression for value
    /// </summary>
    public class ValueExpression : IExpression
    {
        /// <summary>
        /// value for the expression
        /// </summary>
        private readonly double value;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueExpression" /> class.
        /// </summary>
        /// <param name="value">value for the expression</param>
        public ValueExpression(double value)
        {
            this.value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueExpression" /> class.
        /// </summary>
        /// <param name="token">token for the value</param>
        /// <param name="exprssion">expression string</param>
        public ValueExpression(Token token, string exprssion) : this(Parse(token, exprssion))
        {
        }

        /// <inheritdoc cref="IExpression.Calculate(IReadOnlyList{double}, IReadOnlyDictionary{string, IExpression})"/>
        public double Calculate() 
        {
            return this.value;
        }

        /// <summary>
        /// Parse value from expression
        /// </summary>
        /// <param name="token">token for the value</param>
        /// <param name="exprssion">expression string</param>
        /// <returns>parsed value</returns>
        private static double Parse(Token token, string exprssion)
        {
            ReadOnlySpan<char> span = ((ReadOnlySpan<char>)exprssion).Slice(token.Start, token.Length);
            double value;
            if (!double.TryParse(span, out value))
            {
                string numberString = exprssion.Substring(token.Start, token.Length);
                throw new ArgumentException($"Invalid number '{numberString}' found in expression '{exprssion}'. Start position {token.Start}, length {token.Length}");
            }

            return value;
        }
    }
}
