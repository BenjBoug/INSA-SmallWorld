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

			List<int>^ generer(int taille, int nbTerrain) {
				List<int>^ res = gcnew List<int>();
				
				
				int ** map =  mapAlea->generer(taille,nbTerrain);
				for (int i=0;i<taille;i++)
				{
					for (int j=0;j<taille;j++)
					{
						res->Add(map[i][j]);
					}
				}
				return res;
			}

			int * getEmplacementJoueur(int** emplJoueur,int taille)
			{
				return mapAlea->placeJoueur(emplJoueur,taille);
			}
	protected:
		!WrapperMapAleatoire() {delete mapAlea;}
		};
}
