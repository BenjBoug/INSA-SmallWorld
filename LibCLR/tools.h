#pragma once
#include <vector>
using namespace std;
using namespace System;
using namespace Collections::Generic;


List<int>^ tabToList(int ** tab, int largeur, int hauteur);
List<int>^ tabToList(int *** tab, int largeur, int hauteur);
int ** listToTab(List<int>^tab, int largeur, int hauteur);
int * listToTab2(List<int>^tab);
List<int>^ vectorToList(vector<int> tab);
List<int>^ vectorToList(vector<int*> tab);
vector<int> listToVector(List<int>^ tab);