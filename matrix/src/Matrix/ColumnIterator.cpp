//
// Created by mfbut on 11/17/2019.
//

#include "ColumnIterator.h"
#include "ConstColumnIterator.h"
#include "Matrix.h"

Matrix::ColumnIterator::ColumnIterator(Matrix* matrix, int col) {

}

Matrix::ColumnIterator::operator ConstColumnIterator() const {
}

Matrix::ConstVectorRef Matrix::ColumnIterator::operator*() const {
  return const_cast<const Matrix*>(matrix)->colAt(col);
}

Matrix::ColumnIterator::value_type Matrix::ColumnIterator::operator*() {
}



Matrix::ConstVectorRef  Matrix::ColumnIterator::operator[](int offset) const {
  return const_cast<const Matrix*>(matrix)->colAt(col + offset);
}

Matrix::ColumnIterator::value_type Matrix::ColumnIterator::operator[](int offset) {
}

Matrix::ColumnIterator& Matrix::ColumnIterator::operator++() {
}

const Matrix::ColumnIterator Matrix::ColumnIterator::operator++(int) {
}

Matrix::ColumnIterator& Matrix::ColumnIterator::operator--() {
}

const Matrix::ColumnIterator Matrix::ColumnIterator::operator--(int) {
}

Matrix::ColumnIterator& Matrix::ColumnIterator::operator+=(int amount) {
}

Matrix::ColumnIterator Matrix::ColumnIterator::operator+(int amount) const {
}

Matrix::ColumnIterator& Matrix::ColumnIterator::operator-=(int amount) {
}

Matrix::ColumnIterator Matrix::ColumnIterator::operator-(int amount) const {
}

Matrix::ColumnIterator::difference_type Matrix::ColumnIterator::operator-(const ColumnIterator& rhs) {
}

bool Matrix::ColumnIterator::operator==(const ColumnIterator& rhs) const {
}

bool Matrix::ColumnIterator::operator!=(const ColumnIterator& rhs) const {
}

bool Matrix::ColumnIterator::operator<(const ColumnIterator& rhs) const {
}

bool Matrix::ColumnIterator::operator<=(const ColumnIterator& rhs) const {
}

bool Matrix::ColumnIterator::operator>(const ColumnIterator& rhs) const {
}

bool Matrix::ColumnIterator::operator>=(const ColumnIterator& rhs) const {
}
