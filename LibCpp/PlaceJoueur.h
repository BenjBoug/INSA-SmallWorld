#pragma once

#ifdef DLLEXPORT
#define DLL _declspec(dllexport)
#else
#define DLL __declspec(dllimport)
#endif
#include <iostream>
#include <ctime>
#include <vector>
#include "Enum.h"
#include "AStar.h"

using namespace std;

class PlaceJoueur
{
public:
	DLL PlaceJoueur(void);
	DLL ~PlaceJoueur(void);

	DLL int * placeJoueur(int **,int**, int largeur, int hauteur,int peuple);

private:
	int compteJoueurs(int ** tabJoueurs, int largeur, int hauteur);
	int * getCoordJoueur1(int ** tabJoueurs, int largeur, int hauteur);

	void getZone(int **carte, int largeur, int hauteur, int **depl, vector<Coordonnees>* zone);

	vector<int> tabToVecor(int ** tab, int largeur, int hauteur);
};

