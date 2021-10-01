// Program1.cpp : This file contains the 'main' function. Program execution begins and ends there.
//
#pragma once
#include <iostream>
#include <fstream>
#include <vector>
#include <string>
#include <sstream>
#include "Book.h"
#include <algorithm>
#include <chrono>
/// <summary>
/// Reads a file, regardless of whether if its request.dat or new books.dat
/// </summary>
/// <param name="input"></param>
/// <returns></returns>
vector<Book> ReadFile(string input);
/// <summary>
/// Reads a user input, either l or b. If they don't give the right input, keep reading
/// </summary>
/// <returns></returns>
char ReadUserInput();
/// <summary>
/// Sort books, isbn first, then type, then language
/// </summary>
void SortBooks(vector<Book>& input);
/// <summary>
/// Handles the arguments
/// </summary>
/// <param name="argc"></param>
/// <param name="argv"></param>
void ArgsHandling(int argc, char* argv[]);
/// <summary>
/// Search by binary, sort the whole thing first
/// </summary>
/// <param name="allBooks"></param>
/// <param name="requested"></param>
void BinarySearch(vector<Book> allBooks, vector<Book> requested, ofstream& resultFile);
/// <summary>
/// Linear search for the requested books
/// </summary>
/// <param name="allBooks"></param>
/// <param name="requested"></param>
void LinearSearch(vector<Book> allBooks, vector<Book> requested, ofstream& resultFile);
void RunProgram(int argc, char *argv[]);
/// <summary>
/// 3 argvs, one for new, one for request, one for result
/// </summary>
/// <param name="argc"></param>
/// <param name="argv"></param>
/// <returns></returns>
int main(int argc, char *argv[])
{
    vector<Book> ans = ReadFile("C:\\Users\\linka\\OneDrive\\Desktop\\Prog1Test.dat");
    SortBooks(ans);
    std::cout << "Hello World!\n";
}
void RunProgram(int argc, char* argv[])
{
    ArgsHandling(argc, argv);
    string allBooksPath = argv[1];
    string allRequestsPath = argv[2];
    string resultsPath = argv[3];
    vector<Book> allBooks = ReadFile(allBooksPath);
    vector<Book> allRequests = ReadFile(allRequestsPath);
    // try to open the results file
    ofstream resultFile (resultsPath);
    if (!resultFile.is_open())
    {
        cout << "Error: cannot open file " << resultsPath << "\n";
        exit(0);
    }
    
}
void ArgsHandling(int argc, char* argv[])
{
    if (argc != 4) 
    {
        cout << "Usage: .SearchNewBooks <newBooks.dat> <request.dat> <result_file.dat>\n";
    }
}
void BinarySearch(vector<Book> allBooks, vector<Book> requested, ofstream& resultFile)
{
    chrono::high_resolution_clock::time_point start;
    start = chrono::high_resolution_clock::now();
    int totalFound = 0;
    SortBooks(allBooks);
    for (int i = 0; i < (int)requested.size(); i++)
    {
        int start = 0;
        int end = (int)allBooks.size();
        int cur;
        Book thisBook = requested[i];
        while (start < end)
        {
            cur = (start + end) / 2;
            if (thisBook == allBooks[cur]) // found
            {
                totalFound++;
                break;
            }
            if (thisBook < allBooks[cur]) { // move left
                end = cur;
            }
            else // move right
            {
                start = cur + 1;
            }
        }
    }
    auto end = chrono::high_resolution_clock::now();
    double elapsed_us = std::chrono::duration<double, std::micro>(end - start).count();
    std::cout << "CPU time: " << elapsed_us << " microseconds";
    resultFile << totalFound;
    return;
}
void LinearSearch(vector<Book> allBooks, vector<Book> requested, ofstream& resultFile)
{
    chrono::high_resolution_clock::time_point start;
    start = chrono::high_resolution_clock::now();
    int totalFound = 0;
    for (int i = 0; i < (int)requested.size(); i++)
    {
        for (int j = 0; j < (int)allBooks.size(); i++) 
        {
            if (requested[i] == allBooks[j]) // found
            {
                totalFound++;
                break;
            }
        }
    }
    auto end = chrono::high_resolution_clock::now();
    double elapsed_us = std::chrono::duration<double, std::micro>(end - start).count();
    std::cout << "CPU time: " << elapsed_us << " microseconds";
    resultFile << totalFound;
    return;
}
void SortBooks(vector<Book>& input)
{
    sort(input.begin(), input.end());
}
char ReadUserInput()
{
    char final; // unit to return
    std::cout << "Choice of search method([l]inear, [b]inary)?\n";
    while (true)
    {
        string userInput;
        cin >> userInput;
        if ((int)userInput.size() == 1)
        {
            if (userInput == "l") {
                return 'l';
            }
            else if (userInput == "b")
            {
                return 'b';
            }
        }
        cout << "Incorrect choice\n";
    }
    return NULL;
}
vector<Book> ReadFile(string input)
{
    ifstream stream(input);
    if (!stream.is_open())
    {
        cout << "Error: cannot open file " << input << "\n";
        exit(0);
    }
    int isbnNumber;
    string bookType;
    string bookLanguage;
    string line;
    string curString;
    vector<Book> final;
    while (getline(stream,line)) // read an entire line
    {
        stringstream ss(line);
        getline(ss,curString,',');
        isbnNumber = stoi(curString);
        getline(ss, bookLanguage, ',');
        getline(ss, bookType, ',');
        final.push_back(*new Book(isbnNumber, bookLanguage, bookType));
    }
    return final;
}


// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
