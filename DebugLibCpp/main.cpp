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

	unites[0][0]=10;

	PlaceJoueur j;
	j.placeJoueur(unites,carte,TAILLE,TAILLE,0);

	system("pause");
    return 0;
}