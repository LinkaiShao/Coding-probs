#include "MyStringItr.h"
#include"MyString.h"
#include "ConstMyStringItr.h"
MyStringItr::MyStringItr(MyString& over, int start):over(&over), pos(start) {

}
const char& MyStringItr::operator*()const {
	return over->at(pos);
}
char& MyStringItr::operator*() {
	return over->at(pos);
}

MyStringItr& MyStringItr::operator++() {
	pos++;
	return *this;
}
MyStringItr MyStringItr::operator++(int) {
	MyStringItr copy(*this);
	++(*this);
	return copy;
}
MyStringItr& MyStringItr::operator--() {
	pos--;
	return *this;
}
MyStringItr MyStringItr::operator--(int) {
	MyStringItr copy(*this);
	--(*this);
	return copy;
}

MyStringItr MyStringItr::operator+=(int amount) {
	pos += amount;
	return *this;
}
MyStringItr MyStringItr::operator+(int amount)const {
	MyStringItr copy(*this);
	copy += amount;
	return copy;
}
MyStringItr MyStringItr::operator-=(int amount) {
	pos -= amount;
	return *this;
}
MyStringItr MyStringItr::operator-(int amount)const {
	MyStringItr copy(*this);
	copy -= amount;
	return copy;
}
char& MyStringItr::operator[](int index) {
	return over->at(pos+index);
}
const char& MyStringItr::operator[](int index)const {
	return over->at(pos + index);
}
int MyStringItr::operator-(const MyStringItr& rhs)const {
	return pos - rhs.pos;
}
MyStringItr::operator bool()const {
	return pos >= 0 && pos < over->size();
}
bool MyStringItr::operator==(const MyStringItr& rhs) const{
	if (*this && rhs) { // both are in bounds
		if (&over == &rhs.over) { // same data location
			return pos == rhs.pos; // same position in the same data location
		}
		else {
			return false;
		}
	}
	else if (!*this && !rhs) {// both out of bounds
		return &over == &rhs.over;
	}
	else {
		return false;
	}
}
bool MyStringItr::operator!=(const MyStringItr& rhs) const {
	return !(*this == rhs);
}
MyStringItr::operator ConstMyStringItr() const{
	return ConstMyStringItr(*over, pos);
}