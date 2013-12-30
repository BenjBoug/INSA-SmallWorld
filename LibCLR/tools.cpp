#include "Stdafx.h"

List<int>^ tabToList(int ** tab, int largeur, int hauteur)
{
	List<int>^ res = gcnew List<int>();
	for (int i=0;i<largeur;i++)
	{
		for (int j=0;j<hauteur;j++)
		{
			res->Add(tab[i][j]);
		}
	}
	return res;
}

List<int>^ tabToList(int *** tab, int largeur, int hauteur)
{
	List<int>^ res = gcnew List<int>();
	for (int i=0;i<largeur;i++)
	{
		for (int j=0;j<hauteur;j++)
		{
			res->Add(tab[i][j][0]);
		}
	}
	for (int i=0;i<largeur;i++)
	{
		for (int j=0;j<hauteur;j++)
		{
			res->Add(tab[i][j][1]);
		}
	}
	return res;
}

int ** listToTab(List<int>^tab, int largeur, int hauteur)
{
	int ** res = new int*[largeur];

	for (int i=0;i<largeur;i++)
	{
		res[i] = new int[hauteur];
		for (int j=0;j<hauteur;j++)
		{
			res[i][j] = tab[i*hauteur + j];
		}
	}
	return res;
}


List<int>^ vectorToList(vector<int> tab)
{
	List<int>^ res = gcnew List<int>();
	for(unsigned int i=0;i<tab.size();i++)
	{
		res->Add(tab[i]);
	}

	return res;
}

List<int>^ vectorToList(vector<int*> tab)
{
	List<int>^ res = gcnew List<int>();
	for(unsigned int i=0;i<tab.size();i++)
	{
		res->Add(tab[i][0]);
	}

	for(unsigned int i=0;i<tab.size();i++)
	{
		res->Add(tab[i][1]);
	}

	return res;
}


vector<int> listToVector(List<int>^ tab)
{
	vector<int> res;

	for each (int var in tab)
	{
		res.push_back(var);
	}

	return res;
}