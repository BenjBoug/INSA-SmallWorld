#pragma once
#include "MapAleatoire.h"
#include "tools.h"
using namespace System;
using namespace Collections::Generic;

namespace LibCLR {

	public ref class WrapperMapAleatoire {
		private:
			MapAleatoire* mapAlea;
		public:
			WrapperMapAleatoire();
			~WrapperMapAleatoire();

			List<int>^ generer(int largeur, int hauteur, int nbTerrain);
	protected:
		!WrapperMapAleatoire() {delete mapAlea;}
		};
}
