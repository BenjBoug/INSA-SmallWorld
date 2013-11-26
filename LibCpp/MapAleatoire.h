#pragma once
#define DLL _declspec(dllexport)
#include <stdlib.h>
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

