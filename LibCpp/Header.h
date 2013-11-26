#pragma once
#define DLL _declspec(dllexport)

class Test {
public:
	DLL Test() {}
	DLL ~Test() {}

	DLL int test(int a)
	{
		return a*a;
	}

};