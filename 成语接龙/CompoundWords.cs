using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace CompoundWords
{
    class Counter 
    {
        public List<string> contents = new List<string>();
        public int counter = 0;
    }

    class CompoundWords
    {

        // class has a list of strings as the list of compound words
        // and two functions one for reading from file and one for doing the compound words
        private List<String> allCompoundWords = new List<string> (); // list of all 成语
        private Dictionary<char, Counter> WordsByHead = new Dictionary<char, Counter>(); // starting character matched with a list of all words of the same character as head
        public void ReadFromFile(string path)
        {
            string[] lines = System.IO.File.ReadAllLines(@path);
            foreach(var item in lines)
            {
                allCompoundWords.Add(item);
            }
        }
        // sets up the wordsbyhead dictionary from all compoundwords list
        public void SetUp()
        {
            foreach(var item in this.allCompoundWords)
            {
                if (!WordsByHead.ContainsKey(item[0]))
                {
                    WordsByHead.Add(item[0], new Counter());
                }
                WordsByHead[item[0]].contents.Add(item);
            }
        }
        // gets a starting compound word and creates a list of size according to request, use recursion
        public bool FindListOfRes(char startingChar, int length, List<string> results)
        {
            if(length == 0) // length = 0 means that you have found original lenght amount of compound words as a list
            {
                return true;
            }
            bool finished = false;
            // go through the list of compound words with the same starting character
            if (WordsByHead.ContainsKey(startingChar))
            {
                for (int i = 0; i < WordsByHead[startingChar].contents.Count; i++)
                {
                    string usedCompound = WordsByHead[startingChar].contents[i];
                    WordsByHead[startingChar].contents.RemoveAt(i);
                    char curLastLetter = usedCompound[3];
                    finished = FindListOfRes(curLastLetter, length - 1, results); // give the last character of the compound word as starting letter
                    WordsByHead[startingChar].contents.Insert(i, usedCompound);
                    if (finished)
                    {
                        results.Insert(0, WordsByHead[startingChar].contents[i]);
                        break;
                    }
                }
            }
            
            return finished;
        }
        public List<string> FindLongestPath(string startingCompoundWord)
        {
            List<string> longestPath = new List<string>();
            List<string> tempPath = new List<string>();
            if (WordsByHead.ContainsKey(startingCompoundWord[3]))
            {
                for (int i = 0; i < WordsByHead[startingCompoundWord[3]].contents.Count; i++)
                {
                    if (WordsByHead.ContainsKey(startingCompoundWord[3]))
                    {
                        string usedCompound = WordsByHead[startingCompoundWord[3]].contents[i];
                        WordsByHead[startingCompoundWord[3]].contents.RemoveAt(i);
                        tempPath = FindLongestPath(usedCompound);
                        WordsByHead[startingCompoundWord[3]].contents.Insert(i, usedCompound);
                    }
                    if (tempPath.Count > longestPath.Count)
                    {
                        longestPath = tempPath;
                    }
                }
            }
            longestPath.Insert(0, startingCompoundWord);
            return longestPath;
        }
        public List<string> Run(string startingCompound, int length)
        {
            Console.OutputEncoding = Encoding.Unicode;
            List<string> results = new List<string>();
            List<string> longestResults = new List<string>();
            bool success = FindListOfRes(startingCompound[3], length, results);
            results.Insert(0, startingCompound);
            if (!success)
            {
                longestResults = FindLongestPath(startingCompound);
                return longestResults;
            }
            return results;
        }

    }
}
