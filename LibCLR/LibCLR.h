// LibCLR.h

#pragma once
#include "MapAleatoire.h"
using namespace System;
using namespace Collections::Generic;

namespace LibCLR {

	public ref class WrapperMapAleatoire {
		private:
			MapAleatoire* mapAlea;
		public:
			WrapperMapAleatoire() {mapAlea=new MapAleatoire();}
			~WrapperMapAleatoire() {delete mapAlea;}

			int** generer(int taille, int nbTerrain) {
				return mapAlea->generer(taille,nbTerrain);
			}

			int * getEmplacementJoueur(int** emplJoueur,int taille)
			{
				return mapAlea->placeJoueur(emplJoueur,taille);
			}
	protected:
		!WrapperMapAleatoire() {delete mapAlea;}
		};
}
