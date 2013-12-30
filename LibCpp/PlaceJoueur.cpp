#include "PlaceJoueur.h"


PlaceJoueur::PlaceJoueur(void)
{
}


PlaceJoueur::~PlaceJoueur(void)
{
}


int * PlaceJoueur::placeJoueur(int ** tabJoueurs, int ** carte, int largeur,int hauteur, int peuple)
{
	srand (time(NULL));
	int * coord = new int[2];
	do
	{
		coord[0]=rand()%2*(largeur-1);
		coord[1]=rand()%2*(hauteur-1);
	} 
	while (tabJoueurs[coord[0]][coord[1]]!=0);

	return coord;
}
