#pragma once
#include <unordered_map>
#include "Node.h"
using namespace std;


namespace std {
    template <>
        class hash<Coordonnees>{
        public :
        size_t operator()(const Coordonnees &c ) const{
            return hash<int>()(c.x()) ^ hash<int>()(c.y());
        }
    };
}

class NodeFactory
{
public:
	NodeFactory(void);
	~NodeFactory(void);

	Node * getNode(Coordonnees& c);

	void clear();

private:
	unordered_map<Coordonnees, Node*> mapOfNode;
};

