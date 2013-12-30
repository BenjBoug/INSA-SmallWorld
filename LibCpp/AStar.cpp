#include "AStar.h"


AStar::AStar(void)
{
}


AStar::~AStar(void)
{
}

vector<Node*>* AStar::pathFinding(vector<int> carte, int peupleUnite, int largeur, int hauteur, Coordonnees &start, Coordonnees &goal)
{
	factory.clear();
	this->carte.clear();
	this->carte = carte;
	this->peupleUnite = peupleUnite;
	this->largeur=largeur;
	this->hauteur=hauteur;
	try
	{
		return pathFinding(factory.getNode(start),factory.getNode(goal));
	}
	catch(PathNotFoundException &)
	{
		return new vector<Node*>();
	}
}

vector<Node*>* AStar::pathFinding(Node *start, Node *goal)
{
	closedset.clear();
    openset.clear();
    came_from.clear();

    openset.push_back(start);
    start->setG_Score(0);
	start->setF_Score(start->getG_Score() + start->distance(goal));
    Node* current=NULL;
    while (openset.size()>0)
    {
		current = *min_element(openset.begin(),openset.end(),compareFScore);
		 
        if (current == goal)
            return reconstruct_path(came_from, goal);

		openset.erase(remove(openset.begin(), openset.end(), current), openset.end());
        closedset.push_back(current);
		vector<Node*>* neighbors = neighbor_nodes(current);
		vector<Node*>::iterator it;
        for(it=neighbors->begin();it!=neighbors->end();it++)
        {
            double tentative_g_score = current->getG_Score() + current->distance(*it);
            double tentative_f_score = tentative_g_score + (*it)->distance(goal);
            if (find(closedset.begin(), closedset.end(), *it) != closedset.end() && tentative_f_score >= (*it)->getF_Score())
                continue;

            if (find(openset.begin(), openset.end(), *it) == openset.end() || tentative_f_score < (*it)->getF_Score())
            {
                came_from[*it] = current;
                (*it)->setG_Score(tentative_g_score);
                (*it)->setF_Score(tentative_f_score);
                if (find(openset.begin(), openset.end(), *it) == openset.end())
                {
                    openset.push_back(*it);
                }
            }
        }

		delete neighbors;
    }
	throw PathNotFoundException();
}

vector<Node*>* AStar::reconstruct_path(unordered_map<Node*, Node*> came_from, Node *current)
{
	unordered_map<Node*,Node*>::iterator it = came_from.find(current);
	if(it != came_from.end())
	{
        vector<Node*>* tmp = reconstruct_path(came_from,came_from[current]);
        tmp->push_back(new Node(*current));
        return tmp;
    }
    else
    {
        vector<Node*>* path = new vector<Node*>();
        path->push_back(new Node(*current));
        return path;
    }
}

vector<Node*>* AStar::neighbor_nodes(Node *current)
{
	vector<Node*>* res = new vector<Node*>();
	Coordonnees coordCurrent = current->getCoord();

    if (coordCurrent.x() - 1 >= 0 && verif[peupleUnite][carte[(coordCurrent.x()-1) * largeur + coordCurrent.y()]]!=0)
    {
		res->push_back(factory.getNode(Coordonnees(coordCurrent.x() - 1, coordCurrent.y())));
    }
    if (coordCurrent.x() + 1 < largeur && verif[peupleUnite][carte[(coordCurrent.x() + 1) * largeur + coordCurrent.y()]]!=0)
    {
		res->push_back(factory.getNode(Coordonnees(coordCurrent.x() + 1, coordCurrent.y())));
    }
    if (coordCurrent.y() - 1 >= 0 && verif[peupleUnite][carte[coordCurrent.x() * largeur + (coordCurrent.y()-1)]]!=0)
    {
		res->push_back(factory.getNode(Coordonnees(coordCurrent.x(), coordCurrent.y()-1)));
    }
    if (coordCurrent.y() + 1 < hauteur && verif[peupleUnite][carte[coordCurrent.x() * largeur + (coordCurrent.y()+1)]]!=0)
    {
		res->push_back(factory.getNode(Coordonnees(coordCurrent.x(), coordCurrent.y()+1)));
    }

    return res;
}