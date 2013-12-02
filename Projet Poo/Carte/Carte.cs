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


        protected List<IUnite>[][] unites;

        public List<IUnite>[][] Unites
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

        public int[] getCoord(IUnite u)
        {
            int[] coord = new int[2];

            for (int i = 0; i < largeur; i++)
            {
                for (int j = 0; j < hauteur; j++)
                {
                    if (unites[i][j] != null && unites[i][j].Contains(u))
                    {
                        coord[0] = i;
                        coord[1] = j;
                        break;
                    }
                }
            }

            return coord;
        }

        public abstract void selectionneUnite(IUnite unite);

        public abstract void selectionneCase(int x, int y);

        public abstract void placeUnite(List<IUnite> list);

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


        public void deplaceUnite(IUnite u, int column, int row)
        {
            for (int i = 0; i < largeur; i++)
            {
                for (int j = 0; j < hauteur; j++)
                {
                    if (unites[i][j] != null && unites[i][j].Count > 0)
                    {
                        if (unites[i][j].Contains(u))
                        {
                            if (unites[column][row] == null)
                                unites[column][row] = new List<IUnite>();
                            unites[column][row].Add(u);
                            unites[i][j].Remove(u);
                        }
                    }
                }
            }

            u.PointsDepl--;

        }

        public int[][] suggestion(IUnite unite, int x, int y)
        {
            WrapperMapAleatoire wrap = new WrapperMapAleatoire();
            List<int> emplUnites = new List<int>();
            for (int i = 0; i < Largeur; i++)
            {
                for (int j = 0; j < Hauteur; j++)
                {
                    if (Unites[i][j] != null)
                        emplUnites.Add(Unites[i][j].Count);
                    else
                        emplUnites.Add(0);
                }
            }

            List<int> carteInt = new List<int>();
            for (int i = 0; i < Largeur; i++)
            {
                for (int j = 0; j < Hauteur; j++)
                {
                    int tile = -1;
                    if (Cases[i][j] is CaseDesert)
                    {
                        tile = 0;
                    }
                    else if (Cases[i][j] is CaseEau)
                    {
                        tile = 1;
                    }
                    else if (Cases[i][j] is CaseForet)
                    {
                        tile = 2;
                    }
                    else if (Cases[i][j] is CaseMontagne)
                    {
                        tile = 3;
                    }
                    else if (Cases[i][j] is CasePlaine)
                    {
                        tile = 4;
                    }
                    else
                        tile = -1;
                    carteInt.Add(tile);
                }
            }

            int peuple = -1;
            IPeuple p = unite.Proprietaire.Peuple;

            if (p is PeupleViking)
                peuple = 0;
            else if (p is PeupleNain)
                peuple = 1;
            else if (p is PeupleGaulois)
                peuple = 2;

            List<int> sugg = wrap.getSuggestion(carteInt, emplUnites, Largeur, x, y, unite.PointsDepl, peuple);

            int[][] res = new int[Largeur][];
            for (int i = 0; i < Largeur; i++)
            {
                res[i] = new int[Hauteur];
            }

            for (int i = 0; i < Largeur; i++)
            {
                for (int j = 0; j < Hauteur; j++)
                {
                    res[i][j] = sugg[i*Largeur + j];
                }
            }

            return res;
        }
    }
}
