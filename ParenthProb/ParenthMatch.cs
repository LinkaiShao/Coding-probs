using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

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
        public bool CheckParenthMatch(string input, char[] characters)
        {
            for(int i = 0; i< characters.Length;i+=2)
            {
                int start = 0;
                int start1 = 0;
                while (true)
                {
                    int temp = FindChar(input, characters[i], start);
                    int temp1 = FindChar(input, characters[i+1], start1);
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
        public int Findpair(List<char> input, char elem1, char elem2)
        {
            for(int i = 0; i < input.Count-1; i++)
            {
                if(input[i] == elem1 && input[i + 1] == elem2)
                {
                    return i;
                }
            }
            return -1;
        }
        public List<char> CreateNeededElemOnly(string input, char[]brackets)
        {
            List<char> fin = new List<char>();
            foreach(var item in input)
            {
                if(brackets.Contains(item))
                {
                    fin.Add(item);
                }
            }
            return fin;
        }
        public bool CheckParenthMathc(string input, char[]brackets)
        {
            var copy = CreateNeededElemOnly(input, brackets);
            
            while(true)
            {
                bool havePairs = false;
                for (int i = 0; i < brackets.Length; i+=2)
                {
                    int cur = Findpair(copy, brackets[i], brackets[i + 1]);
                    // pair is foud
                    if(cur > -1)
                    {
                        havePairs = true;
                        copy.RemoveAt(cur);
                        copy.RemoveAt(cur);
                    }
                }
                if(!havePairs && copy.Count==0)
                {
                    return true;
                }
                else if(!havePairs && copy.Count!=0)
                {
                    return false;
                }
                
            }
        }
    }
}
