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

			int ** listToTab(List<int>^tab,int taille)
			{
				int ** res = new int*[taille];

				for (int i=0;i<taille;i++)
				{
					res[i] = new int[taille];
					for (int j=0;j<taille;j++)
					{
						res[i][j] = tab[i*taille + j];
					}
				}
				return res;
			}

			List<int>^ tabToList(int ** tab, int taille)
			{
				List<int>^ res = gcnew List<int>();
				for (int i=0;i<taille;i++)
				{
					for (int j=0;j<taille;j++)
					{
						res->Add(tab[i][j]);
					}
				}
				return res;
			}

			List<int>^ tabToList2(int *** tab, int taille)
			{
				List<int>^ res = gcnew List<int>();
				for (int i=0;i<taille;i++)
				{
					for (int j=0;j<taille;j++)
					{
						res->Add(tab[i][j][0]);
					}
				}
				for (int i=0;i<taille;i++)
				{
					for (int j=0;j<taille;j++)
					{
						res->Add(tab[i][j][1]);
					}
				}
				return res;
			}

			List<int> ^ getEmplacementJoueur(List<int>^ emplJoueur, List<int>^ map,int taille, int peupleJoueur)
			{
				int ** unites = listToTab(emplJoueur,taille);
				int ** carte = listToTab(map,taille);

				int * coord =  mapAlea->placeJoueur(unites,carte,taille, peupleJoueur);
				List<int>^res = gcnew List<int>();
				res->Add(coord[0]);
				res->Add(coord[1]);
				return res;
			}

			List<int> ^ getSuggestion(List<int>^ carte,List<int>^ unites,int taille,int x,int y,int ptDepl,int peupleJActuel)
			{
				return tabToList2(mapAlea->suggestion(listToTab(carte,taille), listToTab(unites,taille), taille, x, y, ptDepl, peupleJActuel),taille);
			}
	protected:
		!WrapperMapAleatoire() {delete mapAlea;}
		};
}
