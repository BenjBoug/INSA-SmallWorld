#pragma once

#ifdef DLLEXPORT
#define DLL _declspec(dllexport)
#else
#define DLL __declspec(dllimport)
#endif


#include "Node.h"
#include "Coordonnees.h"
#include <vector>
#include <map>
#include <algorithm>

using namespace std;

class AStar
{
public:
	DLL AStar(void);
	DLL ~AStar(void);
	
	DLL vector<Node*> pathFinding(vector<int> carte, int peupleUnite, int taille, Coordonnees *start, Coordonnees *goal);

	vector<Node*> pathFinding(Node *start, Node *goal);
	
private:
	vector<Node*> reconstruct_path(map<Node*, Node*> came_from, Node *current);
	vector<Node*> neighbor_nodes(Node *current);
	Node * isInClosedSet(Coordonnees c);

	vector<int> carte;
	int peupleUnite;
	int taille;

    vector<Node*> closedset;
    vector<Node*> openset;
    map<Node*, Node*> came_from;
};

