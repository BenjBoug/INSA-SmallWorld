#include "stdafx.h"
#include "CppUnitTest.h"
#include "Node.h"
#include "AStar.h"
#include "Coordonnees.h"
#include <vector>
#define TAILLE 5

using namespace std;

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace TestLibCpp
{		
	TEST_CLASS(UnitTest1)
	{
	public:
		
		TEST_METHOD(TestCoordonnees)
		{
			AStar * astar = new AStar();
			vector<int> carte;
			for(int i=0;i<TAILLE*TAILLE;i++)
			{
				carte.push_back(0);
			}
			vector<Node*> path = astar->pathFinding(carte,0,TAILLE,new Coordonnees(0,0),new Coordonnees(TAILLE-1,TAILLE-1));
		}

	};
}