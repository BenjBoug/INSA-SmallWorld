#include "Node.h"


Node::Node(Coordonnees c) : coord(c)
{
	f_score=0;
	g_score=0;
}


Node::~Node(void)
{
}

Coordonnees Node::getCoord()
{
	return coord;
}
void Node::setCoord(Coordonnees coord)
{
	this->coord=coord;
}
double Node::getF_Score()
{
	return f_score;
}
void Node::setF_Score(double f_score)
{
	this->f_score=f_score;
}
double Node::getG_Score()
{
	return g_score;
}
void Node::setG_Score(double g_score)
{
	this->g_score=g_score;
}
double Node::distance(Node &n)
{
	return this->coord.distance(n.coord);
}
double Node::distance(Node *n)
{
	return this->coord.distance(n->coord);
}