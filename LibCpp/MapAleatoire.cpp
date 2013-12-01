#include "MapAleatoire.h"


MapAleatoire::MapAleatoire(void)
{
	srand(time(NULL));
}


MapAleatoire::~MapAleatoire(void)
{
}

int** MapAleatoire::generer(int taille,int nbTerrain)
{
	int** map = new int*[taille];

	for (int i=0; i<taille; i++) {
		map[i]= new int[taille];
	} 

	int max = (taille*taille)/nbTerrain;
	int tile;
	int* tilesNb = new int[nbTerrain];
	for (int i=0;i<nbTerrain;i++)
	{
		tilesNb[i]=0;
	}

	for (int i=0;i<taille;i++)
	{
		for (int j=0;j<taille;j++)
		{
			do
				tile = rand()%nbTerrain;
			while (tilesNb[tile]>=max);
			tilesNb[tile]++;
			map[i][j] = tile;
		}
	}

	delete[] tilesNb;

	return map;
}

int * MapAleatoire::placeJoueur(int ** tabJoueurs, int taille)
{
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