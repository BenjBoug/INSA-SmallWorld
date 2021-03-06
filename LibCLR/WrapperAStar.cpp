#include "stdafx.h"
#include "WrapperAStar.h"

using namespace LibCLR;

WrapperAStar::WrapperAStar(void)
{
	aStar = new AStar();
}
WrapperAStar::~WrapperAStar(void)
{
	delete aStar;
}

List<int>^ WrapperAStar::pathFinding(List<int>^ carte, int peupleUnite, int largeur, int hauteur, int startX, int startY, int goalX, int goalY)
{
	vector<Node*>* path = aStar->pathFinding(listToVector(carte),peupleUnite,largeur,hauteur,Coordonnees(startX,startY),Coordonnees(goalX,goalY));

	List<int> ^ res = gcnew List<int>();
	vector<Node*>::iterator it;
	for(it=path->begin();it!=path->end();it++)
	{
		res->Add((*it)->getCoord().x());
		res->Add((*it)->getCoord().y());
	}
	delete path;
	return res;
}