//------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="ICalculator.cs" company="LiXinXu">
//     Copyright (c) LiXinXu. All rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------------------------------------------

namespace SmartCalculator
{
    /// <summary>
    /// Calculator interface
    /// </summary>
    public interface ICalculator
    {
        /// <summary>
        /// Calculate expression
        /// </summary>
        /// <param name="expression">expression to calculate</param>
        /// <returns>calculated result</returns>
        double Calculate(string expression);
    }
}
