using System;
using System.Collections.Generic;
using System.Text;

namespace ArithCalcV2
{
    // user has acceptable input characters but they are ordered incorrectly, such as 1++2 or (+1)
    // 1. user's input
    // 2. base message
    // 3. where the error occured
    public class WrongPresentationException : ArgumentException
    {
        public string UserInput { get; }
        public int ErrorLocation { get; }
        public WrongPresentationException(string message, string userInput, int errorLocation) : base(message)
        {
            this.UserInput = userInput;
            this.ErrorLocation = errorLocation;
        }
    }
}
