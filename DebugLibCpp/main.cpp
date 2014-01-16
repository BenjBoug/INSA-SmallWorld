#include <iostream> 
#include <vector>
#include "PlaceJoueur.h"
#include "MapAleatoire.h"
using namespace std;

#define TAILLE 10
 
int main() 
{ 
	int ** carte = new int*[TAILLE];
    for (int i=0;i<TAILLE;i++)
        carte[i] = new int[TAILLE];


    int ** unites = new int*[TAILLE];
    for (int i=0;i<TAILLE;i++)
        unites[i] = new int[TAILLE];


	MapAleatoire map;
	vector<int> carteVec=map.generer(TAILLE,TAILLE,5);
	
    for (int i=0;i<TAILLE;i++)
    {
        for (int j=0;j<TAILLE;j++)
        {
			carte[i][j]=carteVec[i*TAILLE + j];
        }
    }
	/*
	unites[2][4]=10;
	carte[4][3]=1;
	carte[3][3]=1;
	carte[3][4]=1;
	*/
	int * peuples = new int[4];
	peuples[0]=0;
	peuples[1]=1;
	peuples[2]=2;
	peuples[3]=3;

	PlaceJoueur j;
	int * coord =  j.placeJoueur(carte,TAILLE,TAILLE, peuples,4);

	cout << coord[0] << " " << coord[1] << endl;
	cout << coord[2] << " " << coord[3] << endl;
	cout << coord[4] << " " << coord[5] << endl;
	cout << coord[6] << " " << coord[7] << endl;

	system("pause");
    return 0;
}