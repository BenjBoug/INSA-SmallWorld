#include "Node.h"

Node::Node()
{
	f_score=0;
	g_score=0;
}

Node::Node(Coordonnees c) : coord(c)
{
	f_score=0;
	g_score=0;
}


Node::~Node(void)
{
}

Coordonnees Node::getCoord() const
{
	return coord;
}
void Node::setCoord(Coordonnees coord)
{
	this->coord=coord;
}


double Node::distance(Node &n)
{
	return this->coord.distance(n.coord);
}
double Node::distance(Node *n)
{
	return this->coord.distance(n->coord);
}
bool operator==(Node n, Node n2)
{
	return n.coord==n2.coord;
}
bool operator<(Node n, Node n2)
{
	return false;
}

bool compareFScore(Node * n1, Node *n2)
{
	return n1->f_score < n2->f_score;
}