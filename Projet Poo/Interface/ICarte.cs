using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public interface ICarte
    {
        int NbUniteParPeuble
        {
            get;
            set;
        }
        int NbToursMax
        {
            get;
            set;
        }
        int Hauteur
        {
            get;
            set;
        }
        int Largeur
        {
            get;
            set;
        }
        Case[][] Cases
        {
            get;
            set;
        }
        List<Unite> Unites
        {
            get;
            set;
        }
        
        void calculerPoints();
        void placeUnite(List<Unite> l);
        void chargerCarte(ICreationCarte creationCarte);
        void setCase(int x, int y, Case _case);
        void actualiseDeplacement();
        void deplaceUnites(List<Unite> u, Coordonnees coord, SuggMap sugg);
        void deplaceUnite(Unite u, Coordonnees coord, SuggMap sugg);

    }
}
