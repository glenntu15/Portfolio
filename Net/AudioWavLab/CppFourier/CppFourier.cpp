// CppFourier.cpp : Defines the exported functions for the DLL application.
//
#ifndef _DEF_CPPFOURIER_H_DEFINED
#define _DEF_CPPFOURIER_H_DEFINED
#include "CppFourier.h""

CppFourier::CppFourier()
{
	Npoints = 0;
}

CppFourier::~CppFourier(){

}

void CppFourier::RecieveData(double* data, int n)
{
	Npoints = n;

}
#endif