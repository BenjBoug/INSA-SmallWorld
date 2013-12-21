#include <iostream> 
#include <vector>
#include "Suggestion.h"
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

	Suggestion* sugg = new Suggestion();

	vector<int*> res = sugg->suggestion(carte, unites, TAILLE,0,0, 4, 0);

	for(int i =0; i<TAILLE;i++)
	{
		for(int j =0; j<TAILLE;j++)
		{
			cout << res[i * TAILLE + j][0];
		}
		cout << endl;
	}
	system("pause");
    return 0;
}