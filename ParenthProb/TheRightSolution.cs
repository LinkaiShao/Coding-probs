using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace GoogleInterviewParenthathese
{
    public static class TheRightSolution
    {
        public static bool CheckSingleParenth(string expression)
        {
            int depth = 0;
            foreach (char c in expression)
            {
                switch (c)
                {
                    case '(': depth++; break;
                    case ')': if (--depth < 0) return false; break;
                }

               
            }
            return depth == 0;
        }
        /// <summary>
        /// pairs are the (), character to character the matching pairs
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="pairs"></param>
        /// <returns></returns>
        public static bool CheckMultipleParenth(string expression, Dictionary<char,char>pairs)
        {
            Stack leftPairs = new Stack();
            // get every character
            foreach(var c in expression)
            {
                // every pair in dict
                foreach (var pair in pairs)
                {
                    if(c == pair.Key)
                    {
                        leftPairs.Push(c);
                    }
                    if(c == pair.Value && !((char)leftPairs.Pop() == pair.Key))
                    {
                        return false;
                    }
                }
            }
            // there are leftover left side of the pairs
            if(leftPairs.Count > 0)
            {
                return false;
            }
            return true;
        }
        
    }
}
