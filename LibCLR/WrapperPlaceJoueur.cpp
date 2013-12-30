#include "stdafx.h"
#include "WrapperPlaceJoueur.h"

using namespace LibCLR;

WrapperPlaceJoueur::WrapperPlaceJoueur(void)
{
	place = new PlaceJoueur();
}
WrapperPlaceJoueur::~WrapperPlaceJoueur(void)
{
	delete place;
}

List<int> ^ WrapperPlaceJoueur::getEmplacementJoueur(List<int>^ emplJoueur, List<int>^ map, int largeur, int hauteur, int peupleJoueur)
{
	int * coord =  place->placeJoueur(listToTab(emplJoueur,largeur,hauteur),listToTab(map,largeur,hauteur),largeur,hauteur, peupleJoueur);
	List<int>^res = gcnew List<int>();
	res->Add(coord[0]);
	res->Add(coord[1]);
	return res;
}