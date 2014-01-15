#include "PlaceJoueur.h"


PlaceJoueur::PlaceJoueur(void)
{
}


PlaceJoueur::~PlaceJoueur(void)
{
}


int * PlaceJoueur::placeJoueur(int ** carte, int largeur,int hauteur, int* peuple, int nbJoueurs)
{

	vector< vector<Coordonnees> > tabZones;
	for(int i=0;i<nbJoueurs;i++)
	{
		vector<Coordonnees> tmp = findZone(carte,largeur,hauteur,peuple[i]);
		tabZones.push_back(tmp);
	}

	vector<Coordonnees> zoneAccessible = getUnion(tabZones);

	Coordonnees coordJ1, coordJ2;
	int max=0;
	
	AStar * star = new AStar();
	vector<Coordonnees>::iterator it = zoneAccessible.begin();
	for(;it!=zoneAccessible.end();it++)
	{
		vector<Coordonnees>::iterator it2 = zoneAccessible.begin();
		for(;it2!=zoneAccessible.end();it2++)
		{
			if (verif[peuple[0]][carte[(*it).x()][(*it).y()]])
			{
				vector<Node*>* res = star->pathFinding(tabToVecor(carte,largeur,hauteur),peuple[0],largeur,hauteur,(*it),(*it2));
				if (res->size()>max)
				{
					max=res->size();
					coordJ1 = (*it);
					coordJ2 = (*it2);
				}
			}
		}
	}
	delete star;

	int * coord = new int[2*nbJoueurs];
	coord[0]=coordJ1.x();
	coord[1]=coordJ1.y();
	coord[2] = coordJ2.x();
	coord[3] = coordJ2.y();
	
	
	Coordonnees coordJ3;

	if (nbJoueurs>=3)
	{
		double max=0;
		AStar * star = new AStar();
		vector<Coordonnees>::iterator it = zoneAccessible.begin();
		for(;it!=zoneAccessible.end();it++)
		{
			if (!((*it) == coordJ1 || (*it) == coordJ2))
			{
				if (verif[peuple[2]][carte[(*it).x()][(*it).y()]])
				{
					vector<Node*>* res = star->pathFinding(tabToVecor(carte,largeur,hauteur),peuple[2],largeur,hauteur,(*it),(coordJ1));
					vector<Node*>* res2 = star->pathFinding(tabToVecor(carte,largeur,hauteur),peuple[2],largeur,hauteur,(*it),(coordJ2));
					if (std::abs((int)res->size()-(int)res2->size())<=2 && (res->size()+res2->size())/2>max)
					{
						max=(res->size()+res2->size())/2;
						coordJ3 = (*it);
					}
				}
			}
		}
		delete star;
		coord[4] = coordJ3.x();
		coord[5] = coordJ3.y();
	}
	
	
	if (nbJoueurs==4)
	{
		Coordonnees coordJ4;
		double max=0;
		AStar * star = new AStar();
		vector<Coordonnees>::iterator it = zoneAccessible.begin();
		for(;it!=zoneAccessible.end();it++)
		{
			if (!((*it) == coordJ1 || (*it) == coordJ2 || (*it) == coordJ3))
			{
				if (verif[peuple[3]][carte[(*it).x()][(*it).y()]])
				{
					vector<Node*>* res = star->pathFinding(tabToVecor(carte,largeur,hauteur),peuple[3],largeur,hauteur,(*it),(coordJ1));
					vector<Node*>* res2 = star->pathFinding(tabToVecor(carte,largeur,hauteur),peuple[3],largeur,hauteur,(*it),(coordJ2));
					vector<Node*>* res3 = star->pathFinding(tabToVecor(carte,largeur,hauteur),peuple[3],largeur,hauteur,(*it),(coordJ3));
					int sigma = std::abs((int)res->size()-(int)res2->size());
					int sigtmp = std::abs((int)res2->size()-(int)res3->size());
					int sigtmp2 = std::abs((int)res->size()-(int)res3->size());
					if ((sigma+sigtmp+sigtmp2)/3<=2 && (res->size()+res2->size()+res3->size())/3>max)
					{
						max=(res->size()+res2->size()+res3->size())/3;
						coordJ4 = (*it);
					}
				}
			}
		}
		delete star;
		coord[6] = coordJ4.x();
		coord[7] = coordJ4.y();
	}

	
	/*
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
	*/

	return coord;
}


vector<Coordonnees> PlaceJoueur::getUnion(vector< vector<Coordonnees> > zones)
{
	vector<Coordonnees> res;


	vector< vector<Coordonnees> >::iterator it;
	for(it=zones.begin();it!=zones.end();it++)
	{
		vector<Coordonnees>::iterator it2;
		for(it2=(*it).begin();it2!=(*it).end();it2++)
		{
			bool ok=true;
			vector< vector<Coordonnees> >::iterator it;
			for(it=zones.begin();it!=zones.end();it++)
			{
				ok &= find((*it).begin(),(*it).end(),(*it2))!=(*it).end();
			}
			if (ok)
			{
				if (find(res.begin(),res.end(),(*it2))==res.end())
					res.push_back((*it2));
			}
		}
	}

	return res;
}


vector<Coordonnees> PlaceJoueur::findZone(int ** carte, int largeur, int hauteur, int peuple)
{
	
	bool ** depl = new bool*[largeur];
	for (int i=0; i<largeur; i++) 
	{
		depl[i]= new bool[hauteur];
	}
	clearDepl(depl,largeur,hauteur);
	vector<Coordonnees> *res= new vector<Coordonnees>();
	for(int i=0;i<largeur;i++)
	{
		for(int j=0;j<hauteur;j++)
		{
			Coordonnees coord(i,j);
			if (verif[peuple][carte[coord.x()][coord.y()]])
			{
				if (find(res->begin(), res->end(), coord)==res->end())
				{
					vector<Coordonnees> *zone = new vector<Coordonnees>();
					zone->push_back(coord);
					clearDepl(depl,largeur,hauteur);
					getZone(coord,carte,largeur,hauteur,depl,zone,peuple);

					if (zone->size()==largeur*hauteur)
						return *zone;
					else if (zone->size()>res->size())
					{
						delete res;
						res=zone;
					}
				}
			}
		}
	}

	return *res;
}

void PlaceJoueur::clearDepl(bool ** depl, int largeur, int hauteur)
{
	for(int i=0;i<largeur;i++)
	{
		for(int j=0;j<hauteur;j++)
		{
			depl[i][j] = false;
		}
	}
}

void PlaceJoueur::getZone(Coordonnees coordActuel, int **carte, int largeur, int hauteur, bool **depl, vector<Coordonnees>* zone, int peuple)
{
	if (!depl[coordActuel.x()][coordActuel.y()])
	{
		depl[coordActuel.x()][coordActuel.y()]=true;
		if (coordActuel.x()-1>=0)
		{
			if (verif[peuple][carte[coordActuel.x()-1][coordActuel.y()]])
			{
				Coordonnees coord(coordActuel.x()-1,coordActuel.y());
				if (find(zone->begin(),zone->end(),coord)==zone->end())
					zone->push_back(coord);
				getZone(coord,carte, largeur, hauteur,depl,zone,peuple); 
			}
		}
		if (coordActuel.x()+1<largeur)
		{
			if (verif[peuple][carte[coordActuel.x()+1][coordActuel.y()]])
			{
				Coordonnees coord(coordActuel.x()+1,coordActuel.y());
				if (find(zone->begin(),zone->end(),coord)==zone->end())
					zone->push_back(coord);
				getZone(coord,carte, largeur, hauteur,depl,zone,peuple); 
			}
		}
		if (coordActuel.y()-1>=0)
		{
			if (verif[peuple][carte[coordActuel.x()][coordActuel.y()-1]])
			{
				Coordonnees coord(coordActuel.x(),coordActuel.y()-1);
				if (find(zone->begin(),zone->end(),coord)==zone->end())
					zone->push_back(coord);
				getZone(coord,carte, largeur, hauteur,depl,zone,peuple); 
			}
		}
		if (coordActuel.y()+1<hauteur)
		{
			if (verif[peuple][carte[coordActuel.x()][coordActuel.y()+1]])
			{
				Coordonnees coord(coordActuel.x(),coordActuel.y()+1);
				if (find(zone->begin(),zone->end(),coord)==zone->end())
					zone->push_back(coord);
				getZone(coord,carte, largeur, hauteur,depl,zone,peuple); 
			}
		}
		if (peuple==Nain && carte[coordActuel.x()][coordActuel.y()]==Montagne)
        {
            for (int i=0;i<largeur;i++)
            {
                for (int j=0;j<hauteur;j++)
                {
                    if(carte[i][j]==Montagne)
                    {
							Coordonnees coord(i,j);
							if (find(zone->begin(),zone->end(),coord)==zone->end())
								zone->push_back(coord);
							getZone(coord,carte, largeur, hauteur,depl,zone,peuple); 
                    }
                }
            }
        }
	}
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