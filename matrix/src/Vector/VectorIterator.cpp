//
// Created by mfbut on 11/14/2019.
//

#include "VectorIterator.h"
#include "ConstColumnIterator.h"

Matrix::VectorIterator::VectorIterator(BaseVector* vector, int pos) {}

Matrix::VectorIterator::operator ConstVectorIterator() const {

}

Matrix::VectorIterator::value_type& Matrix::VectorIterator::operator*() {

}

const Matrix::VectorIterator::value_type& Matrix::VectorIterator::operator*() const {

}

const Matrix::VectorIterator::value_type& Matrix::VectorIterator::operator[](int offset) const {

}

Matrix::VectorIterator::value_type& Matrix::VectorIterator::operator[](int offset) {

}

Matrix::VectorIterator& Matrix::VectorIterator::operator++() {

}

const Matrix::VectorIterator Matrix::VectorIterator::operator++(int) {

}

Matrix::VectorIterator& Matrix::VectorIterator::operator--() {

}

const Matrix::VectorIterator Matrix::VectorIterator::operator--(int) {

}

Matrix::VectorIterator& Matrix::VectorIterator::operator+=(int amount) {

}

Matrix::VectorIterator Matrix::VectorIterator::operator+(int amount) const {

}

Matrix::VectorIterator& Matrix::VectorIterator::operator-=(int amount) {

}

Matrix::VectorIterator Matrix::VectorIterator::operator-(int amount) const {

}

Matrix::VectorIterator::difference_type Matrix::VectorIterator::operator-(const VectorIterator& rhs) {

}

bool Matrix::VectorIterator::operator==(const VectorIterator& rhs) const {

}

bool Matrix::VectorIterator::operator!=(const VectorIterator& rhs) const {

}

bool Matrix::VectorIterator::operator<(const VectorIterator& rhs) const {


}

bool Matrix::VectorIterator::operator<=(const VectorIterator& rhs) const {


}

bool Matrix::VectorIterator::operator>(const VectorIterator& rhs) const {


}

bool Matrix::VectorIterator::operator>=(const VectorIterator& rhs) const {


}






