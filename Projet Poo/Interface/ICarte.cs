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

        void calculerPoints();
        bool caseVide(int x, int y);
        ICase getCase(int x, int y);
        void selectionneUnite(IUnite unite);
        void selectionneCase(int x, int y);
        void placeUnite(List<IUnite> l);
        bool estAdjacente(int x, int y, int x2, int y2);
        IUnite getAdversaire();
        void chargerCarte(ICreationCarte creationCarte);
        void setCase(int x, int y, ICase _case);
        void actualiseDeplacement();
    }
}
