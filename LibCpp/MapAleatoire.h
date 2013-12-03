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

	enum Peuple {Gaulois = 1, Viking = 0, Nain = 2};
	enum Case {Plaine = 0,Eau = 1, Montagne = 2,Desert = 3,Foret = 4};

	DLL MapAleatoire(void);
	DLL ~MapAleatoire(void);

	DLL int** generer(int taille,int nbTerrain);
	DLL int * placeJoueur(int **,int**, int taille,int peuple);
	DLL int ** suggestion(int **carte, int**unites, int taille,int xActuel,int yAcuel, int ptDepl, int peupleJActuel);
	/*
	DLL int * placeJoueur2(int nbJoueurs,int * peuple,int ** carte, int taille);
	int calculDistance(int x,int y,int x2,int y2);*/

	void calculDeplClassique(int **carte, int x, int y, int depl, int peuple, int taille, int ** sugg);

private:
};

