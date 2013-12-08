using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibCLR;
using System.Xml.Serialization;
using System.Xml;

namespace Modele
{
    [XmlInclude(typeof(CarteClassique))]
    public abstract class Carte : ICarte
    {
        public Carte()
        {
            fabriqueCase = new FabriqueCase();
        }



        protected List<Unite> unites;
        public List<Unite> Unites
        {
            get {return unites;}
            set {unites = value;}
        }

        protected Case[][] cases;

        public Case[][] Cases
        {
            get { return cases; }
            set { cases = value; }
        }
        private FabriqueCase fabriqueCase;

        [XmlIgnore]
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


        public void calculerPoints()
        {

            foreach (Unite u in unites)
            {
                IJoueur joueur = u.Proprietaire;
                IPeuple peuple = u.Proprietaire.Peuple;

                joueur.Points += Cases[u.Coord.X][u.Coord.Y].bonusPoints(peuple);

            }

        }

        public void placeUnite(List<Unite> list)
        {
            WrapperMapAleatoire wrap = new WrapperMapAleatoire();
            List<int> emplUnites = new List<int>();
            for (int i = 0; i < Largeur; i++)
            {
                for (int j = 0; j < Hauteur; j++)
                {
                    emplUnites.Add(getUniteFromCoord(new Coordonnees(i, j)).Count);
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
            IPeuple p = list[0].Proprietaire.Peuple;

            if (p is PeupleViking)
                peuple = 0;
            else if (p is PeupleNain)
                peuple = 1;
            else if (p is PeupleGaulois)
                peuple = 2;


            List<int> coord = wrap.getEmplacementJoueur(emplUnites, carteInt, Largeur, peuple);
            foreach (Unite u in list)
            {
                u.Coord = new Coordonnees(coord[0], coord[1]);
                unites.Add(u);
            }
        }

        public void chargerCarte(ICreationCarte creationCarte)
        {
            creationCarte.chargerCarte(this);
        }


        public Case getCase(int x, int y)
        {
            return Cases[x][y];
        }

        public void setCase(int x, int y, Case _case)
        {
            Cases[x][y] = _case;
        }

        public int getNombreUniteRestante(Joueur joueur)
        {
            int res = 0;
            foreach (Unite u in unites)
            {
                if (u.IdProprietaire == joueur.Id)
                    res++;
            }

            return res;
        }


        public void actualiseDeplacement()
        {
            foreach(Unite u in unites)
            {
                u.PointsDepl += 1;
            }
        }

        public List<Unite> getUniteJoueur(Joueur j)
        {
            return unites.Where(u => u.IdProprietaire==j.Id).ToList();
        }

        public List<Unite> getUniteFromCoord(Coordonnees coord)
        {
            return unites.Where(u => u.Coord == coord).ToList();
        }

        public List<Unite> getUniteFromCoordAndJoueur(Coordonnees coord, Joueur j)
        {
            return unites.Where(u => u.Coord == coord && u.IdProprietaire == j.Id).ToList();
        }

        private bool estAdjacent(Coordonnees a, Coordonnees b)
        {
            return Math.Abs(Math.Abs(a.X - b.X) - Math.Abs(a.Y - b.Y))==1;
        }

        private bool caseAccessible(Coordonnees c, Unite u)
        {
            List<Unite> listUniteCase = getUniteFromCoord(c);
            return listUniteCase.Count == 0 || (listUniteCase.Count > 0 && listUniteCase[0].IdProprietaire == u.IdProprietaire);
        }


        public void deplaceUnites(List<Unite> u, Coordonnees destCoord, int nbPtDepl, int[][][] sugg)
        {
            foreach(Unite unit in u)
            {
                List<Unite> dest = getUniteFromCoord(destCoord);
                if (unites.Contains(unit))
                {
                    if (caseAccessible(destCoord,unit)) // si case vide ou case avec alliés
                    {
                        unit.Coord = destCoord;
                        unit.PointsDepl = nbPtDepl;
                    }
                    else // sinon combat
                    {
                        Unite unitDef = dest[0]; //getmeilleurunitedef(dest);

                        unit.attaquer(unitDef);

                        if (!unit.estEnVie())
                        {
                            unites.Remove(unit);
                        }
                        if (!unitDef.estEnVie())
                        {
                            unites.Remove(unitDef);
                            if (getUniteFromCoord(destCoord).Count == 0)
                            {
                                unit.Coord = destCoord;
                            }
                            else
                            {
                                if (!estAdjacent(destCoord,unit.Coord)) // si l'unité n'est pas sur une case adjacente à l'adversaire, on rapproche l'unité
                                {
                                    Coordonnees coordApres = null;
                                    int deplMax = int.MinValue;
                                    if (destCoord.X - 1 >= 0 && sugg[destCoord.X - 1][destCoord.Y][0] != 0 && caseAccessible(new Coordonnees(destCoord.X - 1, destCoord.Y),unit))
                                    {
                                        if (sugg[destCoord.X - 1][destCoord.Y][1] > deplMax)
                                        {
                                            deplMax = sugg[destCoord.X - 1][destCoord.Y][1];
                                            coordApres = new Coordonnees(destCoord.X - 1, destCoord.Y);
                                        }
                                    }
                                    if (destCoord.X + 1 < Largeur && sugg[destCoord.X + 1][destCoord.Y][0] != 0 && caseAccessible(new Coordonnees(destCoord.X + 1, destCoord.Y), unit))
                                    {
                                        if (sugg[destCoord.X + 1][destCoord.Y][1] > deplMax)
                                        {
                                            deplMax = sugg[destCoord.X + 1][destCoord.Y][1];
                                            coordApres = new Coordonnees(destCoord.X + 1, destCoord.Y);
                                        }
                                    }

                                    if (destCoord.Y - 1 >= 0 && sugg[destCoord.X][destCoord.Y - 1][0] != 0 && caseAccessible(new Coordonnees(destCoord.X, destCoord.Y - 1), unit))
                                    {
                                        if (sugg[destCoord.X][destCoord.Y - 1][1] > deplMax)
                                        {
                                            deplMax = sugg[destCoord.X][destCoord.Y - 1][1];
                                            coordApres = new Coordonnees(destCoord.X, destCoord.Y - 1);
                                        }
                                    }

                                    if (destCoord.Y + 1 < Hauteur && sugg[destCoord.X][destCoord.Y + 1][0] != 0 && caseAccessible(new Coordonnees(destCoord.X, destCoord.Y + 1), unit))
                                    {
                                        if (sugg[destCoord.X][destCoord.Y + 1][1] > deplMax)
                                        {
                                            deplMax = sugg[destCoord.X][destCoord.Y + 1][1];
                                            coordApres = new Coordonnees(destCoord.X, destCoord.Y + 1);
                                        }
                                    }
                                    unit.Coord = coordApres;
                                }
                            }
                            unit.PointsDepl = nbPtDepl;
                        }


                    }
                }
            }
        }
    }
}
