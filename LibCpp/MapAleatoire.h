#pragma once

#ifdef DLLEXPORT
#define DLL _declspec(dllexport)
#else
#define DLL __declspec(dllimport)
#endif
#include <stdlib.h>
#include <iostream>
#include <ctime>
#include <vector>

using namespace std;

class MapAleatoire
{
public:
	DLL MapAleatoire(void);
	DLL ~MapAleatoire(void);

	DLL int** generer(int taille,int nbTerrain);
	DLL int * placeJoueur(int **, int taille);
};

