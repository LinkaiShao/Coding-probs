using System;
using System.Collections.Generic;
using System.Text;

namespace LeetCode1418Restaurant
{
    public class DisplayTable1
    {
        // find the location of the item using binary search, if the item does not exist in curtable, return the position it should be inserted
        private int BinarySearchString(IList<IList<string>> curTable, string find)
        {
            int len = curTable[0].Count;
            int min = 1; // min is one since the start is always going to be "table"
            int max = len;
            while (min <= max)
            {
                int mid = (min + max) / 2;
                if(find == curTable[0][mid])
                {
                    return mid;
                }
                string curUnit = curTable[0][mid];
                if (string.Compare(find, curUnit, StringComparison.Ordinal) < 0){ // find is smaller than the current unit
                    int lastPos = mid - 1;
                    string lastUnit = curTable[0][lastPos];
                    if (mid > 0 && string.Compare(find, lastUnit,StringComparison.Ordinal) > 0) // find is larger than the last unit, which means that it should be in the middle of current and last unit
                    {
                        // return the spot that find belongs, either the spot of this or previous
                        return GetClosest(lastUnit, curUnit, find,lastPos,mid) ;
                    }
                    max = mid;
                }
                else // find is larger than the current unit
                {
                    int nextPos = mid + 1;
                    string nextUnit = curTable[0][nextPos];
                    if(mid < len-1 && string.Compare(find, nextUnit, StringComparison.Ordinal) < 0)
                    {
                        // return the spot that find belongs, either this spot or the next
                        return GetClosest(curUnit, nextUnit, find, mid, nextPos);
                    }
                    min = mid + 1;
                }
                
            }
            return -1; // its not gonna go here
        }
        private int GetClosest(string v1, string v2, string target, int v1Pos, int v2Pos)
        {
            if(string.Compare(target,v1,StringComparison.Ordinal) > string.Compare(v2,target,StringComparison.Ordinal))
            {
                return v2Pos;
            }
            return v1Pos;
        }
        // 1. search table, insert a table at the right pos, keep track of cur col length, if not found, create all 0s row
        // 2. search food, insert food at right pos, alphabetical, keep track of cur row length, if not found, create all 0s col
        // 3. binary search 
        
        public IList<IList<string>> DisplayTable(IList<IList<string>> orders)
        {
            string curTable;
            string curFood;
            IList<IList<string>> finalTable = new List<IList<string>>();
            finalTable.Add(new List<string>());
            finalTable[0].Add("Table");
            // if there is a new table, add a new row, if there is a new food, add a new food
            foreach(var order in orders)
            {
                curTable = order[1];
                curFood = order[2];
                int curTableInt = int.Parse(curTable);
                // handle curTable
                int yLength = finalTable.Count; // how many rows are there
                int xLength = finalTable[0].Count; // how many cols are there
                bool xhas = false; // whether if the food exists in final table
                bool yhas =false; // whether if the table number exists in final table
                int xLocation = xLength, ylocation = yLength; // location that you either add or insertthe obj
                for(int i = 1; i < xLength; i++) // x represents the foods and has table as first element
                {
                    var curItem = finalTable[0][i];
                    if(curItem == curFood)
                    {
                        xhas = true;
                        xLocation = i;
                        break;
                    }
                    if (String.Compare(curItem, curFood,StringComparison.Ordinal) > 0)
                    {
                        xhas = false;
                        xLocation = i;
                        break;
                    }
                }
                for (int i = 1; i < yLength; i++) // y represents the foods and has table as first element
                {
                    var curItem = int.Parse(finalTable[i][0]);
                    if (curItem == curTableInt)
                    {
                        yhas = true;
                        ylocation = i;
                        break;
                    }
                    if (curItem>curTableInt)
                    {
                        yhas = false;
                        ylocation = i;
                        break;
                    }
                }
                // if x doesnt have, create a new column for this food
                // if y doesnt have, create  anew row for this table
                // then insert the item at the spot as +1
                if (!xhas)
                {
                    finalTable[0].Insert(xLocation, curFood);
                    xLength += 1;
                    for(int i = 1; i < yLength; i++)
                    {
                        finalTable[i].Insert(xLocation, "0");
                    }
                }
                if (!yhas)
                {
                    finalTable.Insert(ylocation, new List<string>());
                    finalTable[ylocation].Insert(0, curTable);
                    for (int i = 1; i < xLength; i++)
                    {
                        finalTable[ylocation].Add("0");
                    }
                }
                int curNum = int.Parse(finalTable[ylocation][xLocation]) + 1;
                finalTable[ylocation][xLocation] = curNum.ToString();

            }
            return finalTable;
        }
    }
}
