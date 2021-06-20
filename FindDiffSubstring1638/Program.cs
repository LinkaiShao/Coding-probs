using System;

namespace LeetCode_1638_substring_differ_by_1_character
{
    class Program
    {
        static void Main(string[] args)
        {
            FindMostSubstring s = new FindMostSubstring();
            int x = s.CountSubstring("abe", "bbc");
            FindMostSubstringv2 a = new FindMostSubstringv2();
            x = a.FindSubstring("abe", "bbc");
        }
    }
}
