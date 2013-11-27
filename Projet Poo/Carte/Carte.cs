using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibCLR;

namespace Modele
{
    public abstract class Carte : ICarte
    {

        public Carte()
        {
            fabriqueCase = new FabriqueCase();
        }


        protected List<Unite>[][] unites;

        public List<Unite>[][] Unites
        {
            get { return unites; }
            set { unites = value; }
        }

        protected ICase[][] cases;

        public ICase[][] Cases
        {
            get { return cases; }
            set { cases = value; }
        }
        private FabriqueCase fabriqueCase;


        public FabriqueCase FabriqueCase
        {
            get { return fabriqueCase; }
            set { fabriqueCase = value; }
        }

        private int largeur, hauteur, nbToursMax, nbUniteParPeuble;

        public int NbUniteParPeuble
        {
            get { return nbUniteParPeuble; }
            set { nbUniteParPeuble = value; }
        }

        public int NbToursMax
        {
            get { return nbToursMax; }
            set { nbToursMax = value; }
        }

        public int Hauteur
        {
            get { return hauteur; }
            set { hauteur = value; }
        }

        public int Largeur
        {
            get { return largeur; }
            set { largeur = value; }
        }

        public bool estAdjacente(int x, int y, int x2, int y2)
        {
            return ((Math.Abs(x - x2) <= 1) && (Math.Abs(y - y2) <= 1));
        }

        public abstract IUnite getAdversaire();
        public abstract void calculerPoints();

        public void chargerCarte(ICreationCarte creationCarte)
        {
            creationCarte.chargerCarte(this);
        }

        public abstract bool caseVide(int x, int y);

        public ICase getCase(int x, int y)
        {
            return Cases[x][y];
        }

        public void setCase(int x, int y, ICase _case)
        {
            Cases[x][y] = _case;
        }

        public abstract void selectionneUnite(IUnite unite);

        public abstract void selectionneCase(int x, int y);

        public abstract void placeUnite(List<Unite> list);

        public void actualiseDeplacement()
        {
            for (int i = 0; i < largeur; i++)
            {
                for (int j = 0; j < hauteur; j++)
                {
                    if (unites[i][j] != null && unites[i][j].Count > 0)
                    {
                        foreach(Unite u in unites[i][j])
                        {
                            u.PointsDepl += 1;
                        }
                    }
                }
            }
        }
    }
}
