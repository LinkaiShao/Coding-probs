using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleInterviewParenthathese
{
    public class ParenthMatch
    {
        public int FindChar(string input, char find, int start)
        {
            for(int i = start; i < input.Length; i++)
            {
                if(input[i] == find)
                {
                    return i;
                }
            }
            return -1;
        }
        public bool CheckParenthMatch(string input)
        {
            int start = 0;
            int start1 = 0;
            while(true)
            {
                int temp = FindChar(input, '(', start);
                int temp1 = FindChar(input, ')', start1);
                // can't find (
                if(temp==-1)
                {
                    if(temp1 != -1)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else if(temp > temp1)
                {
                    return false;
                }
                start = temp + 1;
                start1 = temp1 + 1;
            }
        }
        // enter characters in pairs in the array like '(',')','[',']'
        public bool CheckParenthMatch(string input, char[] characters)
        {
            for(int i = 0; i< characters.Length;i+2)
            {
                int start = 0;
                int start1 = 0;
                while (true)
                {
                    int temp = FindChar(input, characters[i], start);
                    int temp1 = FindChar(input, characters[i + 1], start1);
                    // can't find (
                    if (temp == -1)
                    {
                        if (temp1 != -1)
                        {
                            return false;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else if (temp > temp1)
                    {
                        return false;
                    }
                    start = temp + 1;
                    start1 = temp1 + 1;
                }
                
            }
            return true;

        }
    }
}
