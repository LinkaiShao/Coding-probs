#include "MyString.h"
#include <cstring>
#include <stdexcept>
#include "MyStringItr.h"
#include "ConstMyStringItr.h"
MyString::MyString(const char* c_str) : contents(nullptr), length(0) {
	for (; c_str[length] != '\0'; ++length);
	contents = new char[length + 1];
	for (int i = 0; i <= length; i++) {
		contents[i] = c_str[i];
	}
}

MyString::MyString() : MyString (""){

}
char& MyString::at(int index) {
	if (index < 0 || index >= length) {
		throw std::out_of_range("Index given is bad");

	}
	else {
		return contents[index];
	}
}
const char& MyString::at(int index) const {
	if (index < 0 || index >= length) {
		throw std::out_of_range("Index given is bad");

	}
	else {
		return contents[index];
	}
}
int MyString::size()const {
	return length;
}

const char& MyString::operator[](int index)const {
	return at(index);
}
char& MyString::operator[](int index) {
	return at(index);
}
MyStringItr MyString::begin() {
	return MyStringItr(*this, 0);
}
MyStringItr MyString::end() {
	return MyStringItr(*this, size());
}
ConstMyStringItr MyString::begin() const{
	return ConstMyStringItr(*this, 0);
}
ConstMyStringItr MyString::end() const{
	return ConstMyStringItr(*this, size());
}
MyString::~MyString() {
	if (contents != nullptr) {
		delete[] contents;// you made an array so delete[] array
	}
	length = 0;
}