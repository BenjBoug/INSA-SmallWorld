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
        List<Unite>[][] Unites
        {
            get;
            set;
        }
        void calculerPoints();
        bool caseVide(int x, int y);
        Case getCase(int x, int y);
        void selectionneUnite(IUnite unite);
        void selectionneCase(int x, int y);
        void placeUnite(List<Unite> l);
        bool estAdjacente(int x, int y, int x2, int y2);
        Unite getAdversaire();
        void chargerCarte(ICreationCarte creationCarte);
        void setCase(int x, int y, Case _case);
        void actualiseDeplacement();
        int[] getCoord(IUnite u);
        void deplaceUnite(Unite u, int column, int row);

        int[][] suggestion(IUnite unite, int x, int y);
    }
}
