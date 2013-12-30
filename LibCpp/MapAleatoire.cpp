#include "MapAleatoire.h"


MapAleatoire::MapAleatoire(void)
{
	srand(time(NULL));
}


MapAleatoire::~MapAleatoire(void)
{
}

vector<int> MapAleatoire::generer(int largeur, int hauteur,int nbTerrain)
{
	vector<int> map;

	int max = (largeur*hauteur)/nbTerrain;
	int tile;
	int* tilesNb = new int[nbTerrain];
	for (int i=0;i<nbTerrain;i++)
	{
		tilesNb[i]=0;
	}

	for (int i=0;i<largeur;i++)
	{
		for (int j=0;j<hauteur;j++)
		{
			do
				tile = rand()%nbTerrain;
			while (tilesNb[tile]>=max);
			tilesNb[tile]++;
			map.push_back(tile);
		}
	}

	delete[] tilesNb;

	return map;
}
