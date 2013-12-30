#pragma once
#include "Suggestion.h"
#include "tools.h"
using namespace System;
using namespace Collections::Generic;

namespace LibCLR {
	public ref class WrapperSuggestion
	{
	public:
		WrapperSuggestion(void);
		~WrapperSuggestion(void);
		List<int> ^ getSuggestion(List<int>^ carte,List<int>^ unites, int largeur, int hauteur,int x,int y,int ptDepl,int peupleJActuel);

	private:
		Suggestion* sugg;

	protected:
		!WrapperSuggestion() {delete sugg;}
	};
}

