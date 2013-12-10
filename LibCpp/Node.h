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
	DLL ~Node(void);

	DLL Coordonnees getCoord();
	void setCoord(Coordonnees coord);
	double getF_Score();
	void setF_Score(double f_score);
	double getG_Score();
	void setG_Score(double g_scre);
	double distance(Node &n);
	double distance(Node *n);

private:
	Coordonnees coord;
	double f_score, g_score;
};

