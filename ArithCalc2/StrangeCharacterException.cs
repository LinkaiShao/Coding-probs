using System;
using System.Collections.Generic;
using System.Text;

namespace ArithCalcV2
{
    // there is a character that doesnt belong
    // need the user original input
    // need the strange character
    // need the location of the strange character
    // cannot inherit from wrongpresentation exception since that is logically not sound
    class StrangeCharacterException : ArgumentException
    {
        public string UserInput { get; }
        public char StrangeCharacter { get; }
        public int ErrorLocation { get; }

        public StrangeCharacterException(string message, string userInput, char strangeCharacter, int errorLocation) : base(message)
        {
            this.UserInput = userInput;
            this.ErrorLocation = errorLocation;
            this.StrangeCharacter = strangeCharacter;
        }
    }
}
