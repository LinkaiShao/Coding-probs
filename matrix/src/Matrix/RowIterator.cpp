//
// Created by mfbut on 11/17/2019.
//

#include "RowIterator.h"
#include "Matrix.h"

Matrix::RowIterator::RowIterator(Matrix* matrix, int row)  {

}

Matrix::RowIterator::operator ConstRowIterator() const {

}

Matrix::ConstVectorRef Matrix::RowIterator::operator*() const {
  //make sure that we call the const at method on matrix
  return const_cast<const Matrix*>(matrix)->rowAt(row);
}

Matrix::ConstVectorRef Matrix::RowIterator::operator[](int offset) const {
  //make sure that we call the const at method on matrix
  return const_cast<const Matrix*>(matrix)->rowAt(row + offset);
}

Matrix::RowIterator::value_type Matrix::RowIterator::operator*() {

}

Matrix::RowIterator::value_type Matrix::RowIterator::operator[](int offset) {

}

Matrix::RowIterator& Matrix::RowIterator::operator++() {

}

const Matrix::RowIterator Matrix::RowIterator::operator++(int) {

}

Matrix::RowIterator& Matrix::RowIterator::operator--() {

}

const Matrix::RowIterator Matrix::RowIterator::operator--(int) {

}

Matrix::RowIterator& Matrix::RowIterator::operator+=(int amount) {

}

Matrix::RowIterator Matrix::RowIterator::operator+(int amount) const {

}

Matrix::RowIterator& Matrix::RowIterator::operator-=(int amount) {

}

Matrix::RowIterator Matrix::RowIterator::operator-(int amount) const {

}

Matrix::RowIterator::difference_type Matrix::RowIterator::operator-(const RowIterator& rhs) {

}

bool Matrix::RowIterator::operator==(const RowIterator& rhs) const {

}

bool Matrix::RowIterator::operator!=(const RowIterator& rhs) const {

}

bool Matrix::RowIterator::operator<(const RowIterator& rhs) const {

}

bool Matrix::RowIterator::operator<=(const RowIterator& rhs) const {

}

bool Matrix::RowIterator::operator>(const RowIterator& rhs) const {

}

bool Matrix::RowIterator::operator>=(const RowIterator& rhs) const {

}
