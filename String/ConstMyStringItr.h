#ifndef  STRING_CONSTMYSTRINGITR_H
#define STRING_CONSTMYSTRINGITR_H
class MyString;
class MyStringItr;
class ConstMyStringItr {
public:
	// constuctor
	ConstMyStringItr(const MyString& over, int start);
	explicit ConstMyStringItr(const MyString& over);
	ConstMyStringItr(const ConstMyStringItr& orig) = default;
	virtual ~ConstMyStringItr() = default;
	// the reference pointers
	const char& operator*() const;
	//char& operator*(); remove because it is nonconst version
	ConstMyStringItr& operator++(); // pre
	ConstMyStringItr operator++(int); // post
	ConstMyStringItr& operator--(); // pre
	ConstMyStringItr operator--(int); // post
	ConstMyStringItr operator+=(int amount);
	ConstMyStringItr operator+ (int amount)const;
	ConstMyStringItr operator-=(int amount);
	ConstMyStringItr operator-(int amount) const;
	const char& operator[](int index)const; 
	//char& operator[](int index); remove because it is nonconst version
	// how far apart are the two iterators
	int operator-(const ConstMyStringItr& rhs)const;
	explicit operator bool()const; // true if in bounds, false if out of bounds
	bool operator==(const ConstMyStringItr& rhs)const;
	bool operator!=(const ConstMyStringItr& rhs)const;

private:
	const MyString* over;
	int pos;
};
#endif // ! STRING_CONSTMYSTRINGITR_H

