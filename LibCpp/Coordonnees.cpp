#include "Coordonnees.h"

Coordonnees::Coordonnees()
{
	this->_x=0;
	this->_y=0;
}
Coordonnees::Coordonnees(int _x, int _y)
{
	this->_x=_x;
	this->_y=_y;
}

Coordonnees::~Coordonnees(void)
{
}

double Coordonnees::distance(Coordonnees other)
{
	return sqrt(pow((double)other._x,2)+pow((double)other._y,2));
}

bool Coordonnees::operator==(Coordonnees &coord1)
{
	return this->_x==coord1._x && this->_y==coord1._y;
}