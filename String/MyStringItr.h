#ifndef  STRING_MYSTRINGITR_H
#define STRING_MYSTRINGITR_H
class MyString;
class ConstMyStringItr; // for conversion from mystring itr to cosnt mystringitr
class MyStringItr {
public:
	// constuctor
	MyStringItr(MyString& over, int start);
	explicit MyStringItr(MyString& over);
	MyStringItr(const MyStringItr& orig) = default;
	virtual ~MyStringItr() = default;
	// the reference pointers
	const char& operator*() const;
	char& operator*();
	MyStringItr& operator++(); // pre
	MyStringItr operator++(int); // post
	MyStringItr& operator--(); // pre
	MyStringItr operator--(int); // post
	MyStringItr operator+=(int amount);
	MyStringItr operator+ (int amount)const;
	MyStringItr operator-=(int amount);
	MyStringItr operator-(int amount) const;
	const char& operator[](int index)const;
	char& operator[](int index);
	// how far apart are the two iterators
	int operator-(const MyStringItr& rhs)const;
	explicit operator bool()const; // true if in bounds, false if out of bounds
	operator ConstMyStringItr() const;
	bool operator==(const MyStringItr& rhs)const;
	bool operator!=(const MyStringItr& rhs)const;
	
private:
	MyString* over;
	int pos;
};

#endif // ! STRING_MYSTRINGITR_H

