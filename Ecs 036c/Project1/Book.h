#pragma once
#ifndef Program1_Book_H
#define Program1_Book_H
#include <string>
#include <map>

using namespace std;
class Book {
public:
	int isbnNumber;
	string language;
	string type;
	map<string, int> typePriority = {
		{"new", 0},
		{"used", 1},
		{"digital", 2}
	};
	Book();
	Book(int input1, string input2, string input3); // constructor for Book
	bool operator==(Book& other);
	bool operator<(Book &other);
};

Book::Book() {
	isbnNumber = -1;
}
Book::Book(int input1, string input2, string input3)
{
	this->isbnNumber = input1;
	this->language = input2;
	this->type = input3;
}
bool Book::operator==(Book& other)
{
	if (this->isbnNumber == other.isbnNumber && this->language == other.language && this->type == other.type)
	{
		return true;
	}
	return false;
}
bool Book::operator<(Book& other)
{
	if (this->isbnNumber > other.isbnNumber)
	{
		return true;
	}
	// if they are equals to each other, we need to keep going
	else if (this->isbnNumber < other.isbnNumber)
	{
		return false;
	}
	if (typePriority[this->type] < typePriority[other.type])
	{
		return true;
	}
	else if (typePriority[this->type] > typePriority[other.type])
	{
		return false;
	}
	int languageCompare = this->language.compare(other.language); // >0 if this is lower than the other
	if (languageCompare < 0) // reversed alphabetical order,the other is higher than this, ex: other is z, this is a
	{
		return true;
	}
	else { // also contains the case of them being exactly the same
		return false;
	}
	
}
#endif // !

