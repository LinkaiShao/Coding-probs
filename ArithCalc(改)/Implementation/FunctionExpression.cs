using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCalculator.Implementation
{
    class FunctionExpression : IExpression
    {
        private FunctionDefinition thisFunc;
        private List<double> parameters;
        /// <summary>
        /// give which function and what params
        /// </summary>
        public FunctionExpression(FunctionDefinition thisFunc, List<double> parameters)
        {
            this.thisFunc = thisFunc;
            this.parameters = parameters;
        }
        // find the value that the function equals to
        public double Calculate()
        {
            int start = 0;
            return TryCalculator.Compile(thisFunc.contents, thisFunc.Tokens,ref start, parameters,thisFunc).Calculate(); // calculate the function
        }
    }
}
