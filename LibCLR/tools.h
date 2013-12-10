#pragma once
#include <vector>
using namespace std;
using namespace System;
using namespace Collections::Generic;


List<int>^ tabToList(int ** tab, int taille);
List<int>^ tabToList(int *** tab, int taille);
int ** listToTab(List<int>^tab,int taille);
List<int>^ vectorToList(vector<int> tab);
List<int>^ vectorToList(vector<int*> tab);
vector<int> listToVector(List<int>^ tab);


