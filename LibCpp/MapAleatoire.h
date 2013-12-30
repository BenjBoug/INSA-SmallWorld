#pragma once

#ifdef DLLEXPORT
#define DLL _declspec(dllexport)
#else
#define DLL __declspec(dllimport)
#endif
#include <iostream>
#include <ctime>
#include <vector>

using namespace std;

class MapAleatoire
{
public:


	DLL MapAleatoire(void);
	DLL ~MapAleatoire(void);

	DLL vector<int> generer(int largeur, int hauteur,int nbTerrain);
private:
};

