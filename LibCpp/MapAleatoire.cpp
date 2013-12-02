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

int * MapAleatoire::placeJoueur(int ** tabJoueurs, int ** carte, int taille, int peuple)
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
	while (tabJoueurs[coord[0]][coord[1]]!=0);// || (carte[coord[0]][coord[1]]==1 && peuple != 0));

	return coord;
}


 int ** MapAleatoire::suggestion(int **carte, int**unites, int taille,int xActuel,int yAcuel, int ptDepl, int peupleJActuel)
{
	int** sugg = new int*[taille];
	for (int i=0; i<taille; i++) 
		sugg[i]= new int[taille];
	

	for (int i=0; i<taille; i++) {
		for (int j=0; j<taille; j++) {
			sugg[i][j] = 0;
		} 
	} 

	if (xActuel-1>=0)
		sugg[xActuel-1][yAcuel]=1;
	if (xActuel+1<taille)
		sugg[xActuel+1][yAcuel]=1;
	if (yAcuel-1>=0)
		sugg[xActuel][yAcuel-1]=1;
	if (yAcuel+1<taille)
		sugg[xActuel][yAcuel+1]=1;



	return sugg;
}

/*
int * MapAleatoire::placeJoueur2(int nbJoueurs,int * peuple,int ** carte, int taille)
{
	int tab[3][5] = { {1,1,1,1,1},
					{1,0,1,1,1},
					{1,1,1,1,1}};

}


int MapAleatoire::calculDistance(int x,int y,int x2,int y2)
{

}*/