#include <iostream> 
#include <vector>
#include "Node.h"
#include "AStar.h"
using namespace std;

#define TAILLE 5
 
int main() 
{ 
	
	AStar * astar = new AStar();
	vector<int> carte;
	for(int i=0;i<TAILLE;i++)
	{
		for(int j=0;j<TAILLE;j++)
		{
			if (j==2)
			{
				if (i==3)
					carte.push_back(0);
				else
					carte.push_back(1);
			}
			else
			{
				carte.push_back(0);
			}
			cout << carte.back() << " ";
		}
		cout << endl;
	}

	for(int i=0;i<0;i++)
		vector<Node*> path = astar->pathFinding(carte,1,TAILLE,new Coordonnees(0,0),new Coordonnees(TAILLE-1,TAILLE-1));
    return 0;
}