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

using namespace std;

class Suggestion
{
public:
	DLL Suggestion(void);
	DLL ~Suggestion(void);

	DLL vector<int*> suggestion(int **carte, int**unites, int largeur, int hauteur,int xActuel,int yAcuel, int ptDepl, int peupleJActuel);
	void calculDeplClassique(int **carte, int**unites, int x, int y, int depl, int peuple, int largeur, int hauteur, int *** sugg);

};

