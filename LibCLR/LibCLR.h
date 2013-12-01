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

			List<int> ^ getEmplacementJoueur(List<int>^ emplJoueur,int taille)
			{
				int ** unites = new int*[taille];

				for (int i=0;i<taille;i++)
				{
					unites[i] = new int[taille];
					for (int j=0;j<taille;j++)
					{
						unites[i][j] = emplJoueur[i*taille + j];
					}
				}

				int * coord =  mapAlea->placeJoueur(unites,taille);
				List<int>^res = gcnew List<int>();
				res->Add(coord[0]);
				res->Add(coord[1]);
				return res;
			}
	protected:
		!WrapperMapAleatoire() {delete mapAlea;}
		};
}
