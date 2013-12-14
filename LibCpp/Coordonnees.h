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
	DLL Coordonnees();
	DLL Coordonnees(int x, int y);
	DLL ~Coordonnees(void);

	DLL inline int x() const
	{
		return _x;
	}
	DLL inline int y() const
	{
		return _y;
	}
	inline void setX(int _x)
	{
		this->_x=_x;
	}
	inline void setY(int _y)
	{
		this->_y=_y;
	}

	DLL double distance(Coordonnees other);

	DLL bool operator==(Coordonnees &coord1);

private:
	int _x, _y;
};
