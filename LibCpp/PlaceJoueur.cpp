#include "PlaceJoueur.h"


PlaceJoueur::PlaceJoueur(void)
{
}


PlaceJoueur::~PlaceJoueur(void)
{
}


int * PlaceJoueur::placeJoueur(int ** tabJoueurs, int ** carte, int taille, int peuple)
{
	/*
	0 : viking
	1 : nain
	2 : gaulois
	*/
	srand (time(NULL));
	int * coord = new int[2];
	do
	{
		coord[0]=rand()%2*(taille-1);
		coord[1]=rand()%2*(taille-1);
	} 
	while (tabJoueurs[coord[0]][coord[1]]!=0);

	return coord;
}
