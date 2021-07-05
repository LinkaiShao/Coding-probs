using System;
using System.Collections.Generic;
using System.Text;

namespace ArithCalcV2
{
    // only needs one variable which is the user input
    class ParenthNotMatchException : ArgumentException
    {
        public string UserInput { get; }
        public ParenthNotMatchException(string message, string argument): base(message)
        {
            this.UserInput = argument;
        }
    }
}
