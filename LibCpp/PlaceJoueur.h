#pragma once

#ifdef DLLEXPORT
#define DLL _declspec(dllexport)
#else
#define DLL __declspec(dllimport)
#endif
#include <iostream>
#include <ctime>
#include "Enum.h"

using namespace std;

class PlaceJoueur
{
public:
	DLL PlaceJoueur(void);
	DLL ~PlaceJoueur(void);

	DLL int * placeJoueur(int **,int**, int largeur, int hauteur,int peuple);
};

