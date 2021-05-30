#ifndef STRING_MYSTRING_H
#define STRING_MYSTRING_H
class ConstMyStringItr;
class MyStringItr;
class MyString {
public:
	MyString(const char* c_str);
	MyString();
	virtual ~MyString();
	char& at(int index);// for both reading and writing
	const char& at(int index)const;//for reading
	char& operator[](int index);
	const char& operator[](int index)const;
	int size() const;
	ConstMyStringItr begin() const; // const after means that the class will not be modified
	ConstMyStringItr end() const;
	MyStringItr begin();
	MyStringItr end();
private:
	char* contents;
	int length;
};
#endif
