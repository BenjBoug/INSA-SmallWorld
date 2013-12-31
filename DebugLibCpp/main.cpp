#include <iostream> 
#include <vector>
#include "PlaceJoueur.h"
using namespace std;

#define TAILLE 5
 
int main() 
{ 
	int ** carte = new int*[TAILLE];
    for (int i=0;i<TAILLE;i++)
        carte[i] = new int[TAILLE];


    int ** unites = new int*[TAILLE];
    for (int i=0;i<TAILLE;i++)
        unites[i] = new int[TAILLE];


    for (int i=0;i<TAILLE;i++)
    {
        for (int j=0;j<TAILLE;j++)
        {
			unites[i][j] = 0;
            carte[i][j] = 0;//rand()%5;
        }
    }

	unites[2][4]=10;
	carte[4][3]=1;
	carte[3][3]=1;
	carte[3][4]=1;

	PlaceJoueur j;
	int * coord = j.placeJoueur(unites,carte,TAILLE,TAILLE,1);

	cout << coord[0] << " " << coord[1];

	system("pause");
    return 0;
}