// Il s'agit du fichier DLL principal.

#include "stdafx.h"


#include "WrapperMapAleatoire.h"

using namespace LibCLR;

WrapperMapAleatoire::WrapperMapAleatoire()
{
	mapAlea=new MapAleatoire();
}
WrapperMapAleatoire::~WrapperMapAleatoire()
{
	delete mapAlea;
}
List<int>^ WrapperMapAleatoire::generer(int largeur, int hauteur, int nbTerrain) {
	return vectorToList(mapAlea->generer(largeur,hauteur,nbTerrain));
}