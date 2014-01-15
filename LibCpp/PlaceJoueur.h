#pragma once

#ifdef DLLEXPORT
#define DLL _declspec(dllexport)
#else
#define DLL __declspec(dllimport)
#endif
#include <iostream>
#include <ctime>
#include <vector>
#include <cmath>
#include "Enum.h"
#include "AStar.h"

using namespace std;

class PlaceJoueur
{
public:
	DLL PlaceJoueur(void);
	DLL ~PlaceJoueur(void);

	DLL int * placeJoueur(int**, int largeur, int hauteur,int* peuple, int nbJoueurs);

	vector<Coordonnees> findZone(int ** carte, int largeur, int hauteur, int peuple);

private:
	int compteJoueurs(int ** tabJoueurs, int largeur, int hauteur);
	int * getCoordJoueur1(int ** tabJoueurs, int largeur, int hauteur);

	vector<Coordonnees> getUnion(vector< vector<Coordonnees> > zones);
	void clearDepl(bool ** depl, int largeur, int hauteur);
	void getZone(Coordonnees coordActuel, int **carte, int largeur, int hauteur, bool **depl, vector<Coordonnees>* zone, int peuple);

	vector<int> tabToVecor(int ** tab, int largeur, int hauteur);
};

