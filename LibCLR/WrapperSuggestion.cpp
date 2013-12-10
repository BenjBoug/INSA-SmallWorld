#include "stdafx.h"
#include "WrapperSuggestion.h"

using namespace LibCLR;

WrapperSuggestion::WrapperSuggestion(void)
{
	sugg = new Suggestion();
}
WrapperSuggestion::~WrapperSuggestion(void)
{
	delete sugg;
}

List<int> ^ WrapperSuggestion::getSuggestion(List<int>^ carte,List<int>^ unites,int taille,int x,int y,int ptDepl,int peupleJActuel)
{
	return vectorToList(sugg->suggestion(listToTab(carte,taille), listToTab(unites,taille), taille, x, y, ptDepl, peupleJActuel));
}