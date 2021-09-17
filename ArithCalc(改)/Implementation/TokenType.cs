//------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="TokenType.cs" company="LiXinXu">
//     Copyright (c) LiXinXu. All rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------------------------------------------

namespace SmartCalculator.Implementation
{
    using System;

    /// <summary>
    /// Token type
    /// </summary>
    [Flags]
    public enum TokenType
    {
        /// <summary>
        /// Left Parentheses: '('
        /// </summary>
        LeftParentheses = 1,

        /// <summary>
        /// Right parentheses: ')'
        /// </summary>
        RightParentheses = 1 << 1,

        /// <summary>
        /// "Add" operator: '+'
        /// </summary>
        Add = 1 << 2,

        /// <summary>
        /// "Minus" operator: '-'
        /// </summary>
        Minus = 1 << 4,

        /// <summary>
        /// Multiplication" operator: '*'
        /// </summary>
        Multiplication = 1 << 5,

        /// <summary>
        /// "Division" operator: '/'
        /// </summary>
        Division = 1 << 6,

        /// <summary>
        /// Number string
        /// </summary>
        Number = 1 << 7,

        /// <summary>
        /// Symbol string
        /// </summary>
        Symbol = 1 << 8,

        /// <summary>
        /// Comma character: ','
        /// </summary>
        Comma = 1 << 9,
        

    }
}
