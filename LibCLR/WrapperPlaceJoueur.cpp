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
	
	cout << coord[0] << " " << coord[1] << endl;
	cout << coord[2] << " " << coord[3] << endl;
	cout << coord[4] << " " << coord[5] << endl;
	cout << coord[6] << " " << coord[7] << endl;

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