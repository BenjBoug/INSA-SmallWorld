#include "Suggestion.h"


Suggestion::Suggestion(void)
{
}


Suggestion::~Suggestion(void)
{
}


vector<int*> Suggestion::suggestion(int **carte, int**unites, int taille,int xActuel,int yAcuel, int ptDepl, int peupleJActuel)
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

	calculDeplClassique(carte,unites,xActuel,yAcuel,ptDepl,peupleJActuel,taille, sugg);
	vector<int*> res;
	int *data;
	for (int i=0; i<taille; i++) 
	{
		for (int j=0; j<taille; j++) {
			data = new int[2];
			data[0]=sugg[i][j][0];
			data[1]=sugg[i][j][1];
			res.push_back(data);
		} 
	}

	return res;
}

 
void Suggestion::calculDeplClassique(int **carte, int **unites, int xActuel, int yActuel, int depl, int peupleJActuel, int taille, int*** sugg)
{
	 int verif[3][5] = { {2,1,2,1,2},	//VIKING
						 {3,0,1,2,2},	//GAULOIS
						 {1,0,2,2,3}};	//NAIN
	if (depl>0)
	{
		sugg[xActuel][yActuel][1]=depl;
		if (xActuel-1>=0)
		{
			if (unites[xActuel][yActuel]==0)
			{
				sugg[xActuel-1][yActuel][0]=verif[peupleJActuel][carte[xActuel-1][yActuel]];
				if (sugg[xActuel-1][yActuel][1]<depl && sugg[xActuel-1][yActuel][0]>0)
					calculDeplClassique(carte,unites,xActuel-1,yActuel,depl-1,peupleJActuel,taille,sugg);
			}
		}
		if (xActuel+1<taille)
		{
			if (unites[xActuel][yActuel]==0)
			{
				sugg[xActuel+1][yActuel][0]=verif[peupleJActuel][carte[xActuel+1][yActuel]];
				if (sugg[xActuel+1][yActuel][1]<depl && sugg[xActuel+1][yActuel][0]>0)
					calculDeplClassique(carte,unites,xActuel+1,yActuel,depl-1,peupleJActuel,taille,sugg);
			}
		}
		if (yActuel-1>=0)
		{
			if (unites[xActuel][yActuel]==0)
			{
				sugg[xActuel][yActuel-1][0]=verif[peupleJActuel][carte[xActuel][yActuel-1]];
				if (sugg[xActuel][yActuel-1][1]<depl && sugg[xActuel][yActuel-1][0]>0)
					calculDeplClassique(carte,unites,xActuel,yActuel-1,depl-1,peupleJActuel,taille,sugg);
			}
		}
		if (yActuel+1<taille)
		{
			if (unites[xActuel][yActuel]==0)
			{
				sugg[xActuel][yActuel+1][0]=verif[peupleJActuel][carte[xActuel][yActuel+1]];
				if (sugg[xActuel][yActuel+1][1]<depl && sugg[xActuel][yActuel+1][0]>0)
					calculDeplClassique(carte,unites,xActuel,yActuel+1,depl-1,peupleJActuel,taille,sugg);
			}
		}

		if (peupleJActuel==Nain && carte[xActuel][yActuel]==Montagne)
        {
            for (int i=0;i<taille;i++)
            {
                for (int j=0;j<taille;j++)
                {
                    if(carte[i][j]==Montagne && unites[i][j]==0)
                    {
                        sugg[i][j][0]=2;
                        if (sugg[i][j][1]<depl && sugg[i][j][0]>0)
                            calculDeplClassique(carte,unites,i,j,depl-1,peupleJActuel,taille, sugg);
                    }
                }
            }
        }
		else if(peupleJActuel==Viking)
		{
			if (xActuel-1>=0)
			{
				if (carte[xActuel-1][yActuel]==Eau)
					sugg[xActuel-1][yActuel][0]+=1;
			}
			if (xActuel+1<taille)
			{
				if (carte[xActuel+1][yActuel]==Eau)
					sugg[xActuel+1][yActuel][0]+=1;
			}
			if (yActuel-1>=0)
			{
				if (carte[xActuel][yActuel-1]==Eau)
					sugg[xActuel][yActuel-1][0]+=1;
			}
			if (yActuel+1<taille)
			{
				if (carte[xActuel][yActuel+1]==Eau)
					sugg[xActuel][yActuel+1][0]+=1;
			}
		}

	}
}