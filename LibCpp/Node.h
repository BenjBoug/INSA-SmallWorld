#pragma once
#ifdef DLLEXPORT
#define DLL _declspec(dllexport)
#else
#define DLL __declspec(dllimport)
#endif
#include "Coordonnees.h"

class Node
{
public:
	DLL Node(Coordonnees coord);
	DLL Node();
	DLL ~Node(void);

	DLL Coordonnees getCoord() const;
	void setCoord(Coordonnees coord);

	inline double getF_Score()
	{
		return f_score;
	}
	inline void setF_Score(double f_score)	
	{
		this->f_score=f_score;
	}

	inline double getG_Score()
	{
		return g_score;
	}
	inline void setG_Score(double g_score)
	{
		this->g_score=g_score;
	}

	double distance(Node &n);
	double distance(Node *n);
	
	friend bool operator==(Node n, Node n2);
	friend bool operator<(Node n, Node n2);
	friend bool compareFScore(Node * n1, Node *n2);

private:
	Coordonnees coord;
	double f_score, g_score;
};
