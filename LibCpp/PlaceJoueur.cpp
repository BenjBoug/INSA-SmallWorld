#include "PlaceJoueur.h"


PlaceJoueur::PlaceJoueur(void)
{
}


PlaceJoueur::~PlaceJoueur(void)
{
}


int * PlaceJoueur::placeJoueur(int ** tabJoueurs, int ** carte, int largeur,int hauteur, int peuple)
{

	int nbJoueur = compteJoueurs(tabJoueurs,largeur,hauteur);

	srand (time(NULL));
	int * coord = new int[2];
	if (nbJoueur==0)
	{
		coord[0]=rand()%2*(largeur-1);
		coord[1]=rand()%2*(hauteur-1);
		int * coord2 = new int[2];
		coord2[0] = (((largeur-1)-coord[0])/4)*4;
		coord2[1] = (((hauteur-1)-coord[1])/4)*4;

		AStar * star = new AStar();
		vector<Node*>* res = star->pathFinding(tabToVecor(carte,largeur,hauteur),peuple,largeur,hauteur,Coordonnees(coord[0],coord[1]),Coordonnees(coord2[0],coord2[1]));
		delete star;
		coord[0]=res->back()->getCoord().x();
		coord[1]=res->back()->getCoord().y();
		delete res;

	}
	else if(nbJoueur==1)
	{
		coord = getCoordJoueur1(tabJoueurs,largeur,hauteur);
		int * coord2 = new int[2];
		coord2[0] = (((largeur-1)-coord[0])/4)*4;
		coord2[1] = (((hauteur-1)-coord[1])/4)*4;
		
		AStar * star = new AStar();
		vector<Node*>* res = star->pathFinding(tabToVecor(carte,largeur,hauteur),peuple,largeur,hauteur,Coordonnees(coord[0],coord[1]),Coordonnees(coord2[0],coord2[1]));
		delete star;
		coord[0]=res->back()->getCoord().x();
		coord[1]=res->back()->getCoord().y();
		delete res;

	}
	else
	{
		do
		{
			coord[0]=rand()%2*(largeur-1);
			coord[1]=rand()%2*(hauteur-1);
		} 
		while (tabJoueurs[coord[0]][coord[1]]!=0);
	}


	return coord;
}

vector<int> PlaceJoueur::tabToVecor(int ** tab, int largeur, int hauteur)
{
	vector<int> res;

	for(int i=0;i<largeur;i++)
	{
		for (int j=0;j<hauteur;j++)
		{
			res.push_back(tab[i][j]);
		}
	}

	return res;
}

int * PlaceJoueur::getCoordJoueur1(int ** tabJoueurs, int largeur, int hauteur)
{
	int * coord = new int[2];
	int cmp = 0;

	for(int i=0;i<largeur;i++)
	{
		for (int j=0;j<hauteur;j++)
		{
			if (tabJoueurs[i][j]>0)
			{
				coord[0]=i;
				coord[1]=j;
				return coord;
			}
		}
	}

	return NULL;
}

int PlaceJoueur::compteJoueurs(int ** tabJoueurs, int largeur, int hauteur)
{
	int cmp = 0;

	for(int i=0;i<largeur;i++)
	{
		for (int j=0;j<hauteur;j++)
		{
			if (tabJoueurs[i][j]>0)
				cmp++;
		}
	}

	return cmp;
}