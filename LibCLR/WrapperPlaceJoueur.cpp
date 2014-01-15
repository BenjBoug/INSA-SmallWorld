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

List<int> ^ WrapperPlaceJoueur::getEmplacementJoueur(List<int>^ map, int largeur, int hauteur,  List<int>^ peupleJoueur, int nbJoueur)
{
	int * coord =  place->placeJoueur(listToTab(map,largeur,hauteur),largeur,hauteur, listToTab2(peupleJoueur),nbJoueur);
	List<int>^res = gcnew List<int>();
	for(int i=0;i<nbJoueur;i++)
	{
		int a = i*2;
		res->Add(coord[a]);
		int b= (i*2)+1;
		res->Add(coord[b]);
	}
	return res;
}