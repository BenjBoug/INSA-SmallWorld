#include "Coordonnees.h"


Coordonnees::Coordonnees(int _x, int _y)
{
	this->_x=_x;
	this->_y=_y;
}

Coordonnees::~Coordonnees(void)
{
}

int Coordonnees::x()
{
	return _x;
}
int Coordonnees::y()
{
	return _y;
}

void Coordonnees::setX(int _x)
{
	this->_x=_x;
}

void Coordonnees::setY(int _y)
{
	this->_y=_y;
}

double Coordonnees::distance(Coordonnees other)
{
	return sqrt(pow((double)other._x,2)+pow((double)other._y,2));
}


bool Coordonnees::operator==(Coordonnees &coord1)
{
	return this->_x==coord1._x && this->_y==coord1._y;
}