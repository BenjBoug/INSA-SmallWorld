#include "Suggestion.h"


Suggestion::Suggestion(void)
{
}


Suggestion::~Suggestion(void)
{
}


vector<int*> Suggestion::suggestion(int **carte, int**unites, int largeur, int hauteur,int xActuel,int yActuel, int ptDepl, int peupleJActuel)
{
	 
	int *** sugg = new int**[largeur];
	for (int i=0; i<largeur; i++) 
	{
		sugg[i]= new int*[hauteur];
		for (int j=0; j<hauteur; j++) {
			sugg[i][j] = new int[2];
			sugg[i][j][0] = 0;
			sugg[i][j][1] = 0;
		} 
	}
	
	sugg[xActuel][yActuel][0]=verif[peupleJActuel][carte[xActuel][yActuel]];
	calculDeplClassique(carte,unites,xActuel,yActuel,ptDepl,peupleJActuel,largeur,hauteur, sugg);
	vector<int*> res;
	int *data;
	for (int i=0; i<largeur; i++) 
	{
		for (int j=0; j<hauteur; j++) {
			data = new int[2];
			data[0]=sugg[i][j][0];
			data[1]=sugg[i][j][1];
			res.push_back(data);
		} 
	}

	return res;
}

 
void Suggestion::calculDeplClassique(int **carte, int **unites, int xActuel, int yActuel, int depl, int peupleJActuel, int largeur, int hauteur, int*** sugg)
{
	if (depl>0)
	{
		sugg[xActuel][yActuel][1]=depl;
		int deplSuiv;
		if (peupleJActuel==Elfe && carte[xActuel][yActuel]==Foret)
			deplSuiv=depl;
		else
			deplSuiv=depl-1;

		if (xActuel-1>=0)
		{
			if (unites[xActuel][yActuel]==0)
			{
				sugg[xActuel-1][yActuel][0]=verif[peupleJActuel][carte[xActuel-1][yActuel]];
				if (sugg[xActuel-1][yActuel][1]<depl && sugg[xActuel-1][yActuel][0]>0)
					calculDeplClassique(carte,unites,xActuel-1,yActuel,deplSuiv,peupleJActuel,largeur,hauteur,sugg);
			}
		}
		if (xActuel+1<largeur)
		{
			if (unites[xActuel][yActuel]==0)
			{
				sugg[xActuel+1][yActuel][0]=verif[peupleJActuel][carte[xActuel+1][yActuel]];
				if (sugg[xActuel+1][yActuel][1]<depl && sugg[xActuel+1][yActuel][0]>0)
					calculDeplClassique(carte,unites,xActuel+1,yActuel,deplSuiv,peupleJActuel,largeur,hauteur,sugg);
			}
		}
		if (yActuel-1>=0)
		{
			if (unites[xActuel][yActuel]==0)
			{
				sugg[xActuel][yActuel-1][0]=verif[peupleJActuel][carte[xActuel][yActuel-1]];
				if (sugg[xActuel][yActuel-1][1]<depl && sugg[xActuel][yActuel-1][0]>0)
					calculDeplClassique(carte,unites,xActuel,yActuel-1,deplSuiv,peupleJActuel,largeur,hauteur,sugg);
			}
		}
		if (yActuel+1<hauteur)
		{
			if (unites[xActuel][yActuel]==0)
			{
				sugg[xActuel][yActuel+1][0]=verif[peupleJActuel][carte[xActuel][yActuel+1]];
				if (sugg[xActuel][yActuel+1][1]<depl && sugg[xActuel][yActuel+1][0]>0)
					calculDeplClassique(carte,unites,xActuel,yActuel+1,deplSuiv,peupleJActuel,largeur,hauteur,sugg);
			}
		}

		if (peupleJActuel==Nain && carte[xActuel][yActuel]==Montagne)
        {
            for (int i=0;i<largeur;i++)
            {
                for (int j=0;j<hauteur;j++)
                {
                    if(carte[i][j]==Montagne && unites[i][j]==0)
                    {
                        sugg[i][j][0]=2;
                        if (sugg[i][j][1]<depl && sugg[i][j][0]>0)
                            calculDeplClassique(carte,unites,i,j,depl-1,peupleJActuel,largeur,hauteur, sugg);
                    }
                }
            }
        }
		else if(peupleJActuel==Viking)
		{
			if (xActuel-1>=0)
			{
				if (carte[xActuel-1][yActuel]==Eau)
					sugg[xActuel][yActuel][0]=5;
			}
			if (xActuel+1<largeur)
			{
				if (carte[xActuel+1][yActuel]==Eau)
					sugg[xActuel][yActuel][0]=5;
			}
			if (yActuel-1>=0)
			{
				if (carte[xActuel][yActuel-1]==Eau)
					sugg[xActuel][yActuel][0]=5;
			}
			if (yActuel+1<hauteur)
			{
				if (carte[xActuel][yActuel+1]==Eau)
					sugg[xActuel][yActuel][0]=5;
			}
		}
	}
}