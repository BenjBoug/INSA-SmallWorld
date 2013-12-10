#include "AStar.h"


AStar::AStar(void)
{
}


AStar::~AStar(void)
{
}

vector<Node*> AStar::pathFinding(vector<int> carte, int peupleUnite, int taille, Coordonnees *start, Coordonnees *goal)
{
	this->carte = carte;
	this->peupleUnite = peupleUnite;
	this->taille=taille;

	return pathFinding(new Node(*start),new Node(*goal));
}

vector<Node*> AStar::pathFinding(Node *start, Node *goal)
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
        //openset.Sort(new ByFScore());
		current = openset.at(0);

        if (current == goal)
            return reconstruct_path(came_from, goal);

		openset.erase(remove(openset.begin(), openset.end(), current), openset.end());
        closedset.push_back(current);
		vector<Node*> neighbors = neighbor_nodes(current);
		vector<Node*>::iterator it;
        for(it=neighbors.begin();it!=neighbors.end();it++)
        {
            double tentative_g_score = current->getG_Score() + current->distance(*it);
            double tentative_f_score = tentative_g_score + (*it)->distance(goal);
            if (find(closedset.begin(), closedset.end(), *it) != closedset.end() && tentative_f_score >= (*it)->getF_Score())
                continue;

            if (find(closedset.begin(), closedset.end(), *it) == closedset.end() || tentative_f_score < (*it)->getF_Score())
            {
                came_from[*it] = current;
                (*it)->setG_Score(tentative_g_score);
                (*it)->setF_Score(tentative_f_score);
                if (find(closedset.begin(), closedset.end(), *it) == closedset.end())
                {
                    openset.push_back(*it);
                }
            }
        }
    }

    return vector<Node*>();
}

vector<Node*> AStar::reconstruct_path(map<Node*, Node*> came_from, Node *current)
{
	if (came_from[current]!=NULL)
    {
        vector<Node*> tmp = reconstruct_path(came_from,came_from[current]);
        tmp.push_back(current);
        return tmp;
    }
    else
    {
        vector<Node*> path ;
        path.push_back(current);
        return path;
    }
}

vector<Node*> AStar::neighbor_nodes(Node *current)
{
	int verif[3][5] =  {{2,1,2,1,2},	//VIKING
						{3,0,1,2,2},	//GAULOIS
						{1,0,2,2,3}};	//NAIN

	vector<Node*> res;
	Coordonnees coordCurrent = current->getCoord();
    Node *tmp;
    if (coordCurrent.x() - 1 >= 0 && verif[peupleUnite][carte[(coordCurrent.x()-1) * taille + coordCurrent.y()]]!=0)
    {
        tmp = new Node(Coordonnees(coordCurrent.x() - 1, coordCurrent.y())); 
		Node * tmpNode = isInClosedSet(tmp->getCoord());
		if (tmpNode != NULL)
		{
			res.push_back(tmpNode);
		}
		else
		{
			res.push_back(tmp);
		}
    }
    if (coordCurrent.x() + 1 < taille && verif[peupleUnite][carte[(coordCurrent.x() + 1) * taille + coordCurrent.y()]]!=0)
    {
        tmp = new Node(Coordonnees(coordCurrent.x() + 1, coordCurrent.y()));
		Node * tmpNode = isInClosedSet(tmp->getCoord());
		if (tmpNode != NULL)
		{
			res.push_back(tmpNode);
		}
		else
		{
			res.push_back(tmp);
		}
    }
    if (coordCurrent.y() - 1 >= 0 && verif[peupleUnite][carte[coordCurrent.x() * taille + (coordCurrent.y()-1)]]!=0)
    {
        tmp = new Node(Coordonnees(coordCurrent.x(), coordCurrent.y()-1));
		Node * tmpNode = isInClosedSet(tmp->getCoord());
		if (tmpNode != NULL)
		{
			res.push_back(tmpNode);
		}
		else
		{
			res.push_back(tmp);
		}
    }
    if (coordCurrent.y() + 1 < taille && verif[peupleUnite][carte[coordCurrent.x() * taille + (coordCurrent.y()+1)]]!=0)
    {
        tmp = new Node(Coordonnees(coordCurrent.x(), coordCurrent.y()+1));
		Node * tmpNode = isInClosedSet(tmp->getCoord());
		if (tmpNode != NULL)
		{
			res.push_back(tmpNode);
		}
		else
		{
			res.push_back(tmp);
		}
    }

    return res;
}

Node * AStar::isInClosedSet(Coordonnees c)
{
	vector<Node*>::iterator it = closedset.begin();
	for(;it!=closedset.end();it++)
	{
		if ((*it)->getCoord()==c)
		{
			return *it;
		}
	}

	return NULL;
}