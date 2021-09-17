//------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="IExpression.cs" company="LiXinXu">
//     Copyright (c) LiXinXu. All rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------------------------------------------

namespace SmartCalculator.Implementation
{
    using System.Collections.Generic;

    /// <summary>
    /// Expression interface
    /// </summary>
    /// <remarks>
    /// Present how to calculate value
    /// </remarks>
    public interface IExpression
    {
        /// <summary>
        /// Get value from expression
        /// </summary>
        /// <param name="parameters">parameters to this expression</param>
        /// <returns>calculated value</returns>
        double Calculate();
    }
}
