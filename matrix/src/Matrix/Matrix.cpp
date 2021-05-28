//
// Created by mfbut on 11/14/2019.
//

#include <algorithm>
#include <functional>
#include "Matrix.h"

Matrix::Matrix::Matrix(int numRows, int numCols, const Matrix::Matrix::element_type& val) ){

}

Matrix::Matrix::Matrix(int numRows, int numCols) {

}

Matrix::Matrix::Matrix(const std::vector<std::vector<element_type>>& values) {

}

Matrix::Matrix::Matrix(const std::vector<Vector>& values) {

}

Matrix::Matrix::Matrix(const std::vector<ConstVectorRef>& values) {

}

Matrix::Matrix::Matrix(const std::vector<VectorRef>& values) {

}

Matrix::Matrix Matrix::Matrix::ident(int dim) {

}

int Matrix::Matrix::getNumRows() const {

}

int Matrix::Matrix::getNumCols() const {

}

Matrix::Matrix::element_type& Matrix::Matrix::at(int row, int col) {

}

const Matrix::Matrix::element_type& Matrix::Matrix::at(int row, int col) const {

}

Matrix::VectorRef Matrix::Matrix::rowAt(int row) {

}

 Matrix::ConstVectorRef Matrix::Matrix::rowAt(int row) const {

}

Matrix::VectorRef Matrix::Matrix::colAt(int col) {

}

Matrix::ConstVectorRef Matrix::Matrix::colAt(int col) const {

}

Matrix::VectorRef Matrix::Matrix::operator[](int row) {

}

Matrix::ConstVectorRef Matrix::Matrix::operator[](int row) const {

}

Matrix::Matrix::const_iterator Matrix::Matrix::begin() const {

}

Matrix::Matrix::const_iterator Matrix::Matrix::end() const {

}

Matrix::Matrix::iterator Matrix::Matrix::begin() {

}

Matrix::Matrix::iterator Matrix::Matrix::end() {

}

Matrix::Matrix::const_row_iterator Matrix::Matrix::rowBegin() const {

}

Matrix::Matrix::const_row_iterator Matrix::Matrix::rowEnd() const {
  ;
}

Matrix::Matrix::row_iterator Matrix::Matrix::rowBegin() {

}

Matrix::Matrix::row_iterator Matrix::Matrix::rowEnd() {

}

Matrix::Matrix::const_column_iterator Matrix::Matrix::colBegin() const {

}

Matrix::Matrix::const_column_iterator Matrix::Matrix::colEnd() const {

}

Matrix::Matrix::column_iterator Matrix::Matrix::colBegin() {

}

Matrix::Matrix::column_iterator Matrix::Matrix::colEnd() {

}

Matrix::Matrix Matrix::Matrix::operator-() const{

}

Matrix::Matrix& Matrix::Matrix::operator+=(const Matrix& rhs) {

}

Matrix::Matrix Matrix::Matrix::operator+(const Matrix& rhs) {

}

Matrix::Matrix& Matrix::Matrix::operator-=(const Matrix& rhs) {

}

Matrix::Matrix Matrix::Matrix::operator-(const Matrix& rhs) {

}

Matrix::Matrix& Matrix::Matrix::operator*=(const Matrix& rhs) {

}

Matrix::Matrix Matrix::Matrix::operator*(const Matrix& rhs) {

}

Matrix::Matrix& Matrix::Matrix::operator+=(const element_type& rhs) {

}

Matrix::Matrix Matrix::Matrix::operator+(const element_type& rhs) const {

}

Matrix::Matrix& Matrix::Matrix::operator-=(const element_type& rhs) {

}

Matrix::Matrix Matrix::Matrix::operator-(const element_type& rhs) const{

}

Matrix::Matrix& Matrix::Matrix::operator*=(const element_type& rhs) {

}

Matrix::Matrix Matrix::Matrix::operator*(const element_type& rhs) const{

}


Matrix::Matrix Matrix::operator+(const Matrix::element_type& lhs, const Matrix& rhs) {

}

Matrix::Matrix Matrix::operator-(const Matrix::element_type& lhs, const Matrix& rhs) {

}

Matrix::Matrix Matrix::operator*(const Matrix::element_type& lhs, const Matrix& rhs) {

}




