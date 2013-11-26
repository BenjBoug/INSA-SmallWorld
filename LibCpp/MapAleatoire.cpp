#include "MapAleatoire.h"


MapAleatoire::MapAleatoire(void)
{
}


MapAleatoire::~MapAleatoire(void)
{
}

int** MapAleatoire::generer(int taille,int nbTerrain)
{
	int** map = new int*[taille];

	for (int i=0; i<taille; ++i) {
		map[i]= new int[5];
	} 

	int max = taille*taille/nbTerrain;
	int tile;
	int* tilesNb = new int[nbTerrain];
	for (int i=0;i<nbTerrain;i++)
	{
		tilesNb[i]=0;
	}
	
	srand (time(NULL));

	for (int i=0;i<taille;i++)
	{
		for (int j=0;j<taille;j++)
		{
			do
				tile = rand()%5;
			while (tilesNb[tile]>max);
			tilesNb[tile]++;
			map[i][j] = tile;
		}
	}

	return map;
}

int * MapAleatoire::placeJoueur(int ** tabJoueurs, int taille)
{
	int nbJoueurs = 0;

	for (int i=0;i<taille;i++)
	{
		for (int j=0;j<taille;j++)
		{
			if (tabJoueurs[i][j]!=0)
			{
				nbJoueurs++;
			}
		}
	}
	
	srand (time(NULL));

	int * coord = new int[2];

	if (nbJoueurs==0)
	{
		coord[0]=rand()*taille;
		coord[1]=rand()*taille;
	}
	else
	{
		do
		{
			coord[0]=rand()*taille;
			coord[1]=rand()*taille;
		} while (tabJoueurs[coord[0]][coord[1]]!=0);
	}

	return coord;
}