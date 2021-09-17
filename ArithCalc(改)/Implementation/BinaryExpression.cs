//------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="BinaryExpression.cs" company="LiXinXu">
//     Copyright (c) LiXinXu. All rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------------------------------------------

namespace SmartCalculator.Implementation
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Binary expression
    /// </summary>
    public class BinaryExpression : IExpression
    {
        /// <summary>
        /// Function for binary operation
        /// </summary>
        private readonly Func<double, double, double> binaryOperation;

        /// <summary>
        /// left expression
        /// </summary>
        private readonly IExpression leftExpression;

        /// <summary>
        /// right expression
        /// </summary>
        private readonly IExpression rightExpression;

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryExpression" /> class.
        /// </summary>
        /// <param name="binaryOperation">binary operation</param>
        /// <param name="left">left expression</param>
        /// <param name="right">right expression</param>
        public BinaryExpression(Func<double, double, double> binaryOperation, IExpression left, IExpression right)
        {
            if (binaryOperation == null)
            {
                throw new ArgumentNullException(nameof(binaryOperation));
            }

            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            this.binaryOperation = binaryOperation;
            this.leftExpression = left;
            this.rightExpression = right;
        }

        /// <inheritdoc cref="IExpression.Calculate()"/>
        public double Calculate()
        {
            double leftValue = this.leftExpression.Calculate();
            double rightValue = this.rightExpression.Calculate();
            return this.binaryOperation(leftValue, rightValue);
        }
    }
}
