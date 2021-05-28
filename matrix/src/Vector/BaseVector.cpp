//
// Created by mfbut on 11/14/2019.
//

#include <functional>
#include <algorithm>
#include "BaseVector.h"
#include "Vector.h"
#include "ConstVectorIterator.h"
#include "VectorIterator.h"
#include "Matrix.h"

Matrix::BaseVector::operator Matrix() const {

}

Matrix::VectorIterator Matrix::BaseVector::begin() {

}

Matrix::VectorIterator Matrix::BaseVector::end() {

}

Matrix::ConstVectorIterator Matrix::BaseVector::begin() const {

}

Matrix::ConstVectorIterator Matrix::BaseVector::end() const {

}

Matrix::ConstVectorIterator Matrix::BaseVector::cbegin() const {

}

Matrix::ConstVectorIterator Matrix::BaseVector::cend() const {

}

Matrix::BaseVector& Matrix::BaseVector::operator+=(const ConstBaseVector& rhs) {

}

Matrix::BaseVector& Matrix::BaseVector::operator-=(const ConstBaseVector& rhs) {

}

Matrix::BaseVector& Matrix::BaseVector::operator+=(const BaseVector::value_type& rhs) {

}

Matrix::BaseVector& Matrix::BaseVector::operator-=(const BaseVector::value_type& rhs) {

}

Matrix::BaseVector& Matrix::BaseVector::operator*=(const BaseVector::value_type& rhs) {

}












