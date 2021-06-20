using System;
using System.Collections.Generic;
using System.Text;

namespace LeetCode_1638_substring_differ_by_1_character
{
    class FindMostSubstringv2
    {
        public int FindSubstring(string s, string t)
        {
            int sl = s.Length; // take the length for faster performence
            int tl = t.Length;
            int total = 0;
            for(int i = 0; i < sl; i++)
            {
                for(int j = 0; j < tl; j++)
                {
                    if (s[i] != t[j])
                    {
                        int leftCounter = 0; // how many steps left are the same
                        int rightCounter = 0; // how many steps right are the same
                        // go left and find amount of left units that are the same
                        // for example abaa vs acaa and we are looking at the second variable b vs c
                        // left there is one unit, towards the right there are two units that are the same
                        // the amount of combinations are (1+1)*(2+1)
                        // for the left you can take none, which makes b vs c
                        // or you can take one which is a
                        // for right you can take none, take one a or take aa
                        // so the combinations are 2*3 in the case of abaa vs acaa
                        while (i-leftCounter-1!=-1 && j-leftCounter-1!=-1)
                        {
                            if (s[i - leftCounter-1] != t[j - leftCounter-1]) // if find elements that are different
                            {
                                break;
                            }
                            leftCounter++;
                        }
                        while (i + rightCounter+1 != sl && j + rightCounter+1 != tl)
                        {
                            if (s[i + rightCounter+1] != t[j + rightCounter+1]) // if find elements that are different
                            {
                                break;
                            }
                            rightCounter++;
                        }
                        total += (leftCounter + 1) * (rightCounter + 1);
                    }
                    
                }
            }
            return total;
        }
    }
}
