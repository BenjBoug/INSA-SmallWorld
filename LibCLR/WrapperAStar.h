#pragma once
#include "AStar.h"
#include "tools.h"
#include <vector>
using namespace System;
using namespace Collections::Generic;
using namespace std;

namespace LibCLR {
	public ref class WrapperAStar
	{
	public:
		WrapperAStar(void);
		~WrapperAStar(void);

		List<int>^ pathFinding(List<int>^ carte, int peupleUnite, int largeur, int hauteur, int startX, int startY, int goalX, int goalY);

	private:
		AStar * aStar;
	protected:
		!WrapperAStar() {delete aStar;}
	};
}

