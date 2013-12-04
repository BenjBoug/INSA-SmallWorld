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
	while (tabJoueurs[coord[0]][coord[1]]!=0 || (carte[coord[0]][coord[1]]==Eau && peuple != Viking));

	return coord;
}


 int *** MapAleatoire::suggestion(int **carte, int**unites, int taille,int xActuel,int yAcuel, int ptDepl, int peupleJActuel)
 {

	int *** sugg = new int**[taille];
	for (int i=0; i<taille; i++) 
	{
		sugg[i]= new int*[taille];
		for (int j=0; j<taille; j++) {
			sugg[i][j] = new int[2];
			sugg[i][j][0] = 0;
			sugg[i][j][1] = 0;
		} 
	}

	calculDeplClassique(carte,xActuel,yAcuel,ptDepl,peupleJActuel,taille, sugg);

	


	return sugg;
}

 
void MapAleatoire::calculDeplClassique(int **carte, int xActuel, int yActuel, int depl, int peupleJActuel, int taille, int*** sugg)
{
	 int verif[3][5] = { {2,1,2,1,2},	//VIKING
						 {3,0,1,2,2},	//GAULOIS
						 {1,0,2,2,3}};	//NAIN
	if (depl>0)
	{
		sugg[xActuel][yActuel][1]=depl;
		if (xActuel-1>=0)
		{
			sugg[xActuel-1][yActuel][0]=verif[peupleJActuel][carte[xActuel-1][yActuel]];
			if (sugg[xActuel-1][yActuel][1]<depl && sugg[xActuel-1][yActuel][0]>0)
				calculDeplClassique(carte,xActuel-1,yActuel,depl-1,peupleJActuel,taille,sugg);
		}
		if (xActuel+1<taille)
		{
			sugg[xActuel+1][yActuel][0]=verif[peupleJActuel][carte[xActuel+1][yActuel]];
			if (sugg[xActuel+1][yActuel][1]<depl && sugg[xActuel+1][yActuel][0]>0)
				calculDeplClassique(carte,xActuel+1,yActuel,depl-1,peupleJActuel,taille,sugg);
		}
		if (yActuel-1>=0)
		{
			sugg[xActuel][yActuel-1][0]=verif[peupleJActuel][carte[xActuel][yActuel-1]];
			if (sugg[xActuel][yActuel-1][1]<depl && sugg[xActuel][yActuel-1][0]>0)
				calculDeplClassique(carte,xActuel,yActuel-1,depl-1,peupleJActuel,taille,sugg);
		}
		if (yActuel+1<taille)
		{
			sugg[xActuel][yActuel+1][0]=verif[peupleJActuel][carte[xActuel][yActuel+1]];
			if (sugg[xActuel][yActuel+1][1]<depl && sugg[xActuel][yActuel+1][0]>0)
				calculDeplClassique(carte,xActuel,yActuel+1,depl-1,peupleJActuel,taille,sugg);
		}

		if (peupleJActuel==Nain && carte[xActuel][yActuel]==Montagne)
        {
            for (int i=0;i<taille;i++)
            {
                for (int j=0;j<taille;j++)
                {
                    if(carte[i][j]==Montagne)// && unites[i][j]==0)
                    {
                        sugg[i][j][0]=2;
                        if (sugg[i][j][1]<depl && sugg[i][j][0]>0)
                            calculDeplClassique(carte,i,j,depl-1,peupleJActuel,taille, sugg);
                    }
                }
            }
        }
	}
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