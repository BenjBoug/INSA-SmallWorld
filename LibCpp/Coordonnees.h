#pragma once
#ifdef DLLEXPORT
#define DLL _declspec(dllexport)
#else
#define DLL __declspec(dllimport)
#endif
#include <math.h>

using namespace std;

class Coordonnees
{
public:
	DLL Coordonnees(int x, int y);
	DLL ~Coordonnees(void);

	DLL int x();
	DLL int y();
	DLL void setX(int _x);
	DLL void setY(int _y);
	DLL double distance(Coordonnees other);

	DLL bool operator==(Coordonnees &coord1);

private:
	int _x, _y;
};

