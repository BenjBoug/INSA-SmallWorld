#pragma once
#include "PlaceJoueur.h"
#include "tools.h"
using namespace System;
using namespace Collections::Generic;


namespace LibCLR {
	public ref class WrapperPlaceJoueur
	{
	public:
		WrapperPlaceJoueur(void);
		~WrapperPlaceJoueur(void);

		List<int> ^ getEmplacementJoueur(List<int>^ emplJoueur, List<int>^ map, int largeur, int hauteur, int peupleJoueur);

	private:
		PlaceJoueur * place;

	protected:
		!WrapperPlaceJoueur() {delete place;}
	};
}

