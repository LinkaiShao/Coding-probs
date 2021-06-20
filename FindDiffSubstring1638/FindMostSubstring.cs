using System;
using System.Collections.Generic;
using System.Text;

namespace LeetCode_1638_substring_differ_by_1_character
{
    class FindMostSubstring
    {
        // equal length strings check if the difference is exactly 1
        private bool Match(string input1, string input2)
        {
            int diff = 0 ;
            for(int i = 0; i < input1.Length; i++)
            {
                if (input1[i] != input2[i])
                {
                    diff += 1;
                }
                if (diff > 1)
                {
                    return false;
                }
            }
            if(diff == 1)
            {
                return true;
            }
            return false;
        }
        public int CountSubstring(string s, string t)
        {
            int counter = 0;
            string largerString;
            string shorterString;
            if (s.Length > t.Length)
            {
                largerString = s;
                shorterString = t;
            }
            else
            {
                largerString = t;
                shorterString = s;
            }
            //int lengthDiff = largerString.Length - shorterString.Length;
            for (int i = 0; i < shorterString.Length + 1; i++) // length of the current substrings of comparison
            {
                string curSub = "";
                for (int j = 0; j < shorterString.Length - i; j++)
                {
                    curSub = shorterString.Substring(j, i+1); // get the value that you are trying to match with the longer
                    for(int k = 0; k < largerString.Length-i; k++)
                    {
                        if (Match(curSub, largerString.Substring(k, i+1)))
                        {
                            counter += 1;
                        }
                    }
                }
            }
            return counter;
        }
    }
}
