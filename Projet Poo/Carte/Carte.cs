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
        /// <summary>
        /// la liste des unités présentes sur la carte
        /// </summary>
        public List<Unite> Unites
        {
            get {return unites;}
            set {unites = value;}
        }

        protected Case[][] cases;
        /// <summary>
        /// Matrice de Case qui représente la carte
        /// </summary>
        public Case[][] Cases
        {
            get { return cases; }
            set { cases = value; }
        }
        private FabriqueCase fabriqueCase;
        /// <summary>
        /// La frabrique des cases
        /// </summary>
        [XmlIgnore]
        public FabriqueCase FabriqueCase
        {
            get { return fabriqueCase; }
            set { fabriqueCase = value; }
        }

        private int largeur, hauteur, nbToursMax, nbUniteParPeuble;

        private int nbUniteClassique, nbUniteElite, nbUniteBlindee;
        /// <summary>
        /// Nombre maximum d'unités blindées par joueur
        /// </summary>
        public int NbUniteBlindee
        {
            get { return nbUniteBlindee; }
            set { nbUniteBlindee = value; }
        }
        /// <summary>
        /// Nombre maximum d'unités élites par joueur
        /// </summary>
        public int NbUniteElite
        {
            get { return nbUniteElite; }
            set { nbUniteElite = value; }
        }
        /// <summary>
        /// Nombre maximum d'unités classique par joueur
        /// </summary>
        public int NbUniteClassique
        {
            get { return nbUniteClassique; }
            set { nbUniteClassique = value; }
        }

        public int NbUniteParPeuble
        {
            get { return nbUniteParPeuble; }
            set { nbUniteParPeuble = value; }
        }
        /// <summary>
        /// Nombre de tours maximum par partie
        /// </summary>
        public int NbToursMax
        {
            get { return nbToursMax; }
            set { nbToursMax = value; }
        }
        /// <summary>
        /// La hauteur de la carte en case
        /// </summary>
        public int Hauteur
        {
            get { return hauteur; }
            set { hauteur = value; }
        }
        /// <summary>
        /// La largeur de la carte en case
        /// </summary>
        public int Largeur
        {
            get { return largeur; }
            set { largeur = value; }
        }

        /// <summary>
        /// Calcul les points de chaque joueurs en fonction de la position des unités sur la carte et du peuple du joueur
        /// </summary>
        public void calculerPoints()
        {
            foreach (Unite u in unites)
            {
                IJoueur joueur = u.Proprietaire;
                IPeuple peuple = u.Proprietaire.Peuple;
                joueur.Points += peuple.calculPoints(this, u);
            }
        }
        /// <summary>
        /// Place les unités de façon aléatoire sur la carte
        /// </summary>
        /// <param name="list">la liste des unités à placées</param>
        public void placeUnite(List<Unite> list)
        {
            WrapperPlaceJoueur wrap = new WrapperPlaceJoueur();
            List<int> emplUnites = new List<int>();
            for (int i = 0; i < Largeur; i++)
            {
                for (int j = 0; j < Hauteur; j++)
                {
                    emplUnites.Add(getUniteFromCoord(new Coordonnees(i, j)).Count);
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


            List<int> coord = wrap.getEmplacementJoueur(emplUnites, toList(), Largeur, peuple);
            foreach (Unite u in list)
            {
                u.Coord = new Coordonnees(coord[0], coord[1]);
                unites.Add(u);
            }
        }
        /// <summary>
        /// Convertie la carte en liste d'entier la représnetant
        /// </summary>
        /// <returns>une liste d'entier</returns>
        public List<int> toList()
        {
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
            return carteInt;
        }
        /// <summary>
        /// charge la carte
        /// </summary>
        /// <param name="creationCarte">la méthode a utiliser pour charger la carte</param>
        public void chargerCarte(ICreationCarte creationCarte)
        {
            creationCarte.chargerCarte(this);
        }
        /// <summary>
        /// Modifie la carte
        /// </summary>
        /// <param name="x">la position x</param>
        /// <param name="y">la position y</param>
        /// <param name="_case">la case a ajouté</param>
        public void setCase(int x, int y, Case _case)
        {
            Cases[x][y] = _case;
        }
        /// <summary>
        /// Récupère le nombre d'unités restantes d'un joueur
        /// </summary>
        /// <param name="joueur">le joueur</param>
        /// <returns>le nombre d'unités restantes</returns>
        public int getNombreUniteRestante(Joueur joueur)
        {
            return getUniteJoueur(joueur).Count;
        }

        /// <summary>
        /// Ajoute pour chaque unités le nombre de points de déplacements à la fin de chaque tour
        /// </summary>
        public void actualiseDeplacement()
        {
            foreach(Unite u in unites)
            {
                u.PointsDepl += u.getDeplSuppl();
            }
        }
        /// <summary>
        /// Récupère les unités d'un joueur encore présente sur la carte
        /// </summary>
        /// <param name="j">le joueur</param>
        /// <returns>la liste des unités du joueur</returns>
        public List<Unite> getUniteJoueur(Joueur j)
        {
            return unites.Where(u => u.IdProprietaire==j.Id).ToList();
        }
        /// <summary>
        /// Récupère les unités d'une case
        /// </summary>
        /// <param name="coord">la coordonnées de la case</param>
        /// <returns>la liste des unités de la case</returns>
        public List<Unite> getUniteFromCoord(Coordonnees coord)
        {
            return unites.Where(u => u.Coord == coord).ToList();
        }
        /// <summary>
        /// Récupère les unités d'une case et d'un joueur précis
        /// </summary>
        /// <param name="coord">la coordonnée de la case</param>
        /// <param name="j">le joueur</param>
        /// <returns>la liste des unités d'une case et d'un joueur précis</returns>
        public List<Unite> getUniteFromCoordAndJoueur(Coordonnees coord, Joueur j)
        {
            return unites.Where(u => u.Coord == coord && u.IdProprietaire == j.Id).ToList();
        }
        /// <summary>
        /// Teste si deux cases sont adjacentes
        /// </summary>
        /// <param name="a">la 1er coordonnees de la case</param>
        /// <param name="b">la 2eme cordonnees de la case</param>
        /// <returns>return vrai si les cases sont adjacentes, faux sinon</returns>
        private bool estAdjacent(Coordonnees a, Coordonnees b)
        {
            return Math.Abs(Math.Abs(a.X - b.X) - Math.Abs(a.Y - b.Y))==1;
        }
        /// <summary>
        /// Teste si la cases est accessible à une unité. C-a-d que la case est vide ou qu'elle contient des unités du même joueur
        /// </summary>
        /// <param name="c">la coordonnees de destination</param>
        /// <param name="u">l'unité qui doit se déplacer</param>
        /// <returns>vrai si la case est accessible, faux sinon</returns>
        private bool caseAccessible(Coordonnees c, Unite u)
        {
            List<Unite> listUniteCase = getUniteFromCoord(c);
            return listUniteCase.Count == 0 || (listUniteCase.Count > 0 && listUniteCase[0].IdProprietaire == u.IdProprietaire);
        }
        /// <summary>
        /// Déplace un unité sur la case de destination.
        /// Attaque les unités ennemis présentes sur la case et se déplace après le combat si la case est vide
        /// </summary>
        /// <param name="unit">l'unité à déplacé</param>
        /// <param name="destCoord">coordonnees de la case de destination</param>
        /// <param name="sugg">les suggesions de deplacement</param>
        public void deplaceUnite(Unite unit, Coordonnees destCoord, SuggMap sugg)
        {
            List<Unite> dest = getUniteFromCoord(destCoord);
            if (unites.Contains(unit))
            {
                if (caseAccessible(destCoord, unit)) // si case vide ou case avec alliés
                {
                    unit.Coord = destCoord;
                    unit.PointsDepl = sugg[destCoord].Depl;
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
                            if (!estAdjacent(destCoord, unit.Coord)) // si l'unité n'est pas sur une case adjacente à l'adversaire, on rapproche l'unité
                            {
                                Coordonnees coordApres = null;
                                int deplMax = int.MinValue;
                                Coordonnees tmp = new Coordonnees(destCoord.X - 1, destCoord.Y);
                                if (destCoord.X - 1 >= 0 && sugg[tmp].Sugg != 0 && caseAccessible(tmp, unit))
                                {
                                    if (sugg[tmp].Depl > deplMax)
                                    {
                                        deplMax = sugg[tmp].Depl;
                                        coordApres = tmp;
                                    }
                                }
                                tmp = new Coordonnees(destCoord.X + 1, destCoord.Y);
                                if (destCoord.X + 1 < Largeur && sugg[tmp].Sugg != 0 && caseAccessible(tmp, unit))
                                {
                                    if (sugg[tmp].Depl > deplMax)
                                    {
                                        deplMax = sugg[tmp].Depl;
                                        coordApres = tmp;
                                    }
                                }

                                tmp = new Coordonnees(destCoord.X, destCoord.Y - 1);
                                if (destCoord.Y - 1 >= 0 && sugg[tmp].Sugg != 0 && caseAccessible(tmp, unit))
                                {
                                    if (sugg[tmp].Depl > deplMax)
                                    {
                                        deplMax = sugg[tmp].Depl;
                                        coordApres = tmp;
                                    }
                                }

                                tmp = new Coordonnees(destCoord.X, destCoord.Y + 1);
                                if (destCoord.Y + 1 < Hauteur && sugg[tmp].Sugg != 0 && caseAccessible(tmp, unit))
                                {
                                    if (sugg[tmp].Depl > deplMax)
                                    {
                                        deplMax = sugg[tmp].Depl;
                                        coordApres = tmp;
                                    }
                                }
                                unit.Coord = coordApres;
                            }
                        }
                        unit.PointsDepl = sugg[destCoord].Depl;
                    }
                }
            }
        }
        /// <summary>
        /// Déplace la liste d'unités
        /// </summary>
        /// <param name="u"></param>
        /// <param name="destCoord"></param>
        /// <param name="sugg"></param>
        public void deplaceUnites(List<Unite> u, Coordonnees destCoord, SuggMap sugg)
        {
            foreach(Unite unit in u)
            {
                deplaceUnite(unit, destCoord, sugg);
            }
        }
    }
}
