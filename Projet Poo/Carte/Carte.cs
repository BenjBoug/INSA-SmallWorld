using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibCLR;
using System.Xml.Serialization;
using System.Xml;

namespace Modele
{
    [Serializable]
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
        [NonSerialized]
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

        private int largeur, hauteur, nbToursMax;

        private int nbUniteClassique, nbUniteElite, nbUniteBlindee;
        /// <summary>
        /// Nombre maximum d'unités blindées par joueur
        /// </summary>
        public int NbUniteBlindee
        {
            get { return nbUniteBlindee; }
            set
            {
                if (value<0)
                    throw new ArgumentException(); 
                nbUniteBlindee = value;
            }
        }
        /// <summary>
        /// Nombre maximum d'unités élites par joueur
        /// </summary>
        public int NbUniteElite
        {
            get { return nbUniteElite; }
            set
            {
                if (value < 0)
                    throw new ArgumentException(); 
                nbUniteElite = value;
            }
        }
        /// <summary>
        /// Nombre maximum d'unités classique par joueur
        /// </summary>
        public int NbUniteClassique
        {
            get { return nbUniteClassique; }
            set
            {
                if (value < 0)
                    throw new ArgumentException(); 
                nbUniteClassique = value;
            }
        }
        /// <summary>
        /// Nombre de tours maximum par partie
        /// </summary>
        public int NbToursMax
        {
            get { return nbToursMax; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException(); 
                nbToursMax = value;
            }
        }
        /// <summary>
        /// La hauteur de la carte en case
        /// </summary>
        public int Hauteur
        {
            get { return hauteur; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException(); 
                hauteur = value;
            }
        }
        /// <summary>
        /// La largeur de la carte en case
        /// </summary>
        public int Largeur
        {
            get { return largeur; }
            set {
                if (value <= 0)
                    throw new ArgumentException(); 
                largeur = value;
            }
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
        /// Place les unités sur la carte à la coordonnees indiqué
        /// </summary>
        /// <param name="list">la liste des unités à placées</param>
        /// <param name="c">la coordonnees où placer les unités</param>
        public void placeUnite(List<Unite> list, Coordonnees c)
        {
            foreach (Unite u in list)
            {
                u.Coord = c;
                unites.Add(u);
            }
        }

        public List<Coordonnees> getCoordonneesDepart(List<Joueur> listJoueur)
        {
            WrapperPlaceJoueur wrap = new WrapperPlaceJoueur();
            List<int> emplUnites = getUnitesToListInt();
            List<int> peuple = new List<int>();
            for(int i=0;i<listJoueur.Count;i++)
                peuple.Add(listJoueur[i].Peuple.toInt());
            List<int> coord = wrap.getEmplacementJoueur(toList(), Largeur,Hauteur, peuple, listJoueur.Count());
            List<Coordonnees> res = new List<Coordonnees>();
            for (int i = 0; i < coord.Count(); i+=2)
            {
                res.Add(new Coordonnees(coord[i], coord[i + 1]));
            }
            return res;
        }

        private List<int> getUnitesToListInt()
        {
            List<int> emplUnites = new List<int>();
            for (int i = 0; i < Largeur; i++)
            {
                for (int j = 0; j < Hauteur; j++)
                {
                    emplUnites.Add(getUniteFromCoord(new Coordonnees(i, j)).Count);
                }
            }
            return emplUnites;
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
                    carteInt.Add(Cases[i][j].toInt());
                }
            }
            return carteInt;
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
        /// <param name="a">la 1er coordonnees d'une case</param>
        /// <param name="b">la 2eme cordonnees d'une case</param>
        /// <returns>return vrai si les cases sont adjacentes, faux sinon</returns>
        private bool estAdjacent(Coordonnees a, Coordonnees b)
        {
            return Math.Abs(Math.Abs(a.X - b.X) - Math.Abs(a.Y - b.Y))==1;
        }
        /// <summary>
        /// Teste si la cases est accessible à une unité. C-a-d que la case est vide ou qu'elle contient des unités du même joueur
        /// et que les suggestions le permettent
        /// </summary>
        /// <param name="c">la coordonnees de destination</param>
        /// <param name="u">l'unité qui doit se déplacer</param>
        /// <returns>vrai si la case est accessible, faux sinon</returns>
        private bool caseAccessible(Coordonnees c, Unite u, SuggMap sugg)
        {
            List<Unite> listUniteCase = getUniteFromCoord(c);
            return coordInBound(c) && sugg[c].Sugg > 0 && listUniteCase.Count == 0 || (listUniteCase.Count > 0 && listUniteCase[0].IdProprietaire == u.IdProprietaire);
        }

        /// <summary>
        /// Retourne la meilleur unité défensive de la liste
        /// </summary>
        /// <param name="units">la liste des unités</param>
        /// <returns>la meilleur unité defensive</returns>
        private Unite getMeilleurUniteDef(List<Unite> units)
        {
            Unite res=null;
            double maxDef = 0;
            foreach (Unite u in units)
            {
                if (maxDef <= (double)(u.PointsVie / u.PointsVieMax) * (double)u.PointsDefense)
                {
                    maxDef = (double)(u.PointsVie / u.PointsVieMax) * (double)u.PointsDefense;
                    res = u;
                }
            }
            if (res == null)
            {
                maxDef = 0;
            }
            return res;
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
            if (sugg.Keys.Contains(destCoord) && sugg[destCoord].Sugg > 0)
            {
                List<Unite> dest = getUniteFromCoord(destCoord);
                if (unites.Contains(unit))
                {
                    if (caseAccessible(destCoord, unit,sugg)) // si case vide ou case avec alliés
                    {
                        unit.Coord = destCoord;
                        unit.PointsDepl = sugg[destCoord].Depl;
                    }
                    else // sinon combat
                    {
                        Unite unitDef = getMeilleurUniteDef(dest);
                        combat(unit, unitDef, destCoord, sugg);
                    }
                }
            }
        }
        /// <summary>
        /// Fait combattre 2 unités, et fait les déplacements appropriés en cas de victoire de l'attaquant.
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="unitDef"></param>
        /// <param name="destCoord"></param>
        /// <param name="sugg"></param>
        private void combat(Unite unit, Unite unitDef, Coordonnees destCoord, SuggMap sugg)
        {
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
                        rapprocheAuPlusPret(unit, destCoord, sugg);
                    }
                }
                unit.PointsDepl = sugg[destCoord].Depl;
            }
        }
        /// <summary>
        /// Rapproche l'unité au plus pret de la destination
        /// </summary>
        /// <param name="unit">l'unité a déplacé</param>
        /// <param name="destCoord">la case de déstination</param>
        /// <param name="sugg">Les suggestions</param>
        private void rapprocheAuPlusPret(Unite unit, Coordonnees destCoord, SuggMap sugg)
        {
            Coordonnees coordApres = null;
            int deplMax = int.MinValue;
            //OUEST
            Coordonnees tmp = new Coordonnees(destCoord.X - 1, destCoord.Y);
            testDeplacementApresCombat(unit, sugg, ref coordApres, ref deplMax, tmp);
            //EST
            tmp = new Coordonnees(destCoord.X + 1, destCoord.Y);
            testDeplacementApresCombat(unit, sugg, ref coordApres, ref deplMax, tmp);
            //NORD
            tmp = new Coordonnees(destCoord.X, destCoord.Y - 1);
            testDeplacementApresCombat(unit, sugg, ref coordApres, ref deplMax, tmp);
            //SUD
            tmp = new Coordonnees(destCoord.X, destCoord.Y + 1);
            testDeplacementApresCombat(unit, sugg, ref coordApres, ref deplMax, tmp);
            unit.Coord = coordApres;
        }


        private void testDeplacementApresCombat(Unite unit, SuggMap sugg, ref Coordonnees coordApres, ref int deplMax, Coordonnees tmp)
        {
            if (caseAccessible(tmp, unit, sugg) && sugg[tmp].Depl > deplMax)
            {
                deplMax = sugg[tmp].Depl;
                coordApres = tmp;
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

        /// <summary>
        /// Test si la coordonnees est bien contenu dans la map
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private bool coordInBound(Coordonnees c)
        {
            return c.X >= 0 && c.X < Largeur && c.Y >= 0 && c.Y < Hauteur;
        }
    }
}
