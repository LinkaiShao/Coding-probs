#include "ConstMyStringItr.h"
#include "MyString.h"
#include "MyStringItr.h"
ConstMyStringItr::ConstMyStringItr(const MyString& over, int start) :over(&over), pos(start) {

}
const char& ConstMyStringItr::operator*()const {
	return over->at(pos);
}

ConstMyStringItr& ConstMyStringItr::operator++() {
	pos++;
	return *this;
}
ConstMyStringItr ConstMyStringItr::operator++(int) {
	ConstMyStringItr copy(*this);
	++(*this);
	return copy;
}
ConstMyStringItr& ConstMyStringItr::operator--() {
	pos--;
	return *this;
}
ConstMyStringItr ConstMyStringItr::operator--(int) {
	ConstMyStringItr copy(*this);
	--(*this);
	return copy;
}

ConstMyStringItr ConstMyStringItr::operator+=(int amount) {
	pos += amount;
	return *this;
}
ConstMyStringItr ConstMyStringItr::operator+(int amount)const {
	ConstMyStringItr copy(*this);
	copy += amount;
	return copy;
}
ConstMyStringItr ConstMyStringItr::operator-=(int amount) {
	pos -= amount;
	return *this;
}
ConstMyStringItr ConstMyStringItr::operator-(int amount)const {
	ConstMyStringItr copy(*this);
	copy -= amount;
	return copy;
}
const char& ConstMyStringItr::operator[](int index)const {
	return over->at(pos + index);
}
int ConstMyStringItr::operator-(const ConstMyStringItr& rhs)const {
	return pos - rhs.pos;
}
ConstMyStringItr::operator bool()const {
	return pos >= 0 && pos < over->size();
}
bool ConstMyStringItr::operator==(const ConstMyStringItr& rhs) const {
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
bool ConstMyStringItr::operator!=(const ConstMyStringItr& rhs) const {
	return !(*this == rhs);
}
