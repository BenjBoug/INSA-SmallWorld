#include "NodeFactory.h"


NodeFactory::NodeFactory(void)
{
}


NodeFactory::~NodeFactory(void)
{
	clear();
}



Node * NodeFactory::getNode(Coordonnees& c)
{
	 unordered_map<Coordonnees,Node*>::const_iterator got = mapOfNode.find (c);

	if (got == mapOfNode.end())
	{
		Node * tmp = new Node(c);
		mapOfNode[c]=tmp;
		return tmp;
	}
	else
	{
		return got->second;
	}
}


void NodeFactory::clear()
{
	for ( auto it = mapOfNode.begin(); it != mapOfNode.end(); ++it )
	{
		delete it->second;
	}
	mapOfNode.clear();
}