#pragma once

#ifdef DLLEXPORT
#define DLL _declspec(dllexport)
#else
#define DLL __declspec(dllimport)
#endif


#include "NodeFactory.h"
#include "PathNotFoundException.h"
#include <vector>
#include <list>
#include <unordered_map>
#include <algorithm>
#include <iostream>

using namespace std;

class AStar
{
public:
	DLL AStar(void);
	DLL ~AStar(void);
	
	DLL vector<Node*>* pathFinding(vector<int> carte, int peupleUnite, int taille, Coordonnees &start, Coordonnees &goal);

	vector<Node*>* pathFinding(Node *start, Node *goal);
	
private:
	vector<Node*>* reconstruct_path(unordered_map<Node*, Node*> came_from, Node *current);
	vector<Node*>* neighbor_nodes(Node *current);

	vector<int> carte;
	int peupleUnite;
	int taille;

    list<Node*> closedset;
    list<Node*> openset;
    unordered_map<Node*, Node*> came_from;
	NodeFactory factory;
};
