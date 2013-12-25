using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Modele
{
    [XmlInclude(typeof(UniteBlindee))]
    [XmlInclude(typeof(UniteElite))]
    public class Unite : IUnite
    {
        public Unite()
        {
            pointsAttaque = 2;
            pointsDefense = 1;
            pointsDepl = 1;
            pointsVie = 2;
            pointsVieMax = pointsVie;
            coord = new Coordonnees();
            strategySuggestion = new Suggestion();
        }

        public virtual int getDeplSuppl()
        {
            return 1;
        }

        protected Coordonnees coord;

        public Coordonnees Coord
        {
            get { return coord; }
            set { coord = value; }
        }

        protected int pointsDefense;

        public int PointsDefense
        {
            get { return pointsDefense; }
            set { pointsDefense = value; }
        }
        protected int pointsAttaque;

        public int PointsAttaque
        {
            get { return pointsAttaque; }
            set { pointsAttaque = value; }
        }

        protected int pointsVieMax;

        public int PointsVieMax
        {
            get { return pointsVieMax; }
            set { pointsVieMax = value; }
        }


        protected int pointsVie;

        public int PointsVie
        {
            get { return pointsVie; }
            set { pointsVie = value; }
        }
        protected int pointsDepl;

        public int PointsDepl
        {
            get { return pointsDepl; }
            set { pointsDepl = value; }
        }
        private Joueur proprietaire;

        [XmlIgnore]
        public Joueur Proprietaire
        {
            get { return proprietaire; }
            set { proprietaire = value; idProp = proprietaire.Id; }
        }

        private int idProp;
        public int IdProprietaire
        {
            get { return idProp; }
            set { idProp = value; }
        }

        /// <summary>
        /// Attaque une unité, les chances de victoire snt calculé en fonction des points de vie, des points d'attaque/défense.
        /// </summary>
        /// <param name="unitDef"></param>
        public void attaquer(IUnite unitDef)
        {
            Random randCombat = new Random();
            Random rand = new Random();
            int nbToursCombat = 3 + randCombat.Next((Math.Max(this.PointsVie, unitDef.PointsVie)) + 2);
            int n = 0;
            while (nbToursCombat - n > 0 && this.estEnVie() && unitDef.estEnVie())
            {
                double ratioVie = (double)this.PointsVie / (double)this.PointsVieMax;
                double ratioVieDef = (double)unitDef.PointsVie / (double)unitDef.PointsVieMax;
                double attaUnit = (double)this.PointsAttaque * (double)ratioVie;
                double defUnitdef = (double)unitDef.PointsDefense * (double)ratioVieDef;
                double ratioAttDef = (double)(attaUnit / defUnitdef);
                double ratioChanceDef = 0;
                if (ratioAttDef > 1) // avantage attaquant
                {
                    ratioChanceDef = (1 / ratioAttDef) / 2;
                    ratioChanceDef = (0.5 - ratioChanceDef) + 0.5;
                }
                else if (ratioAttDef == 1) //égalité, aucun n'a l'avantage
                {
                    ratioChanceDef = 0.5; // 50% de chnce de gagner
                }
                else // avantage défense
                {
                    ratioChanceDef = ratioAttDef/2;
                }
                double ratioCombat = (double)((double)rand.Next(100) / 100);
                if (ratioCombat <= ratioChanceDef)
                {
                    unitDef.perdPV(1);
                }
                else
                {
                    this.perdPV(1);
                }
                n++;
            }
        }
        /// <summary>
        /// Retire des points de vie à l'unité
        /// </summary>
        /// <param name="nb">le nombre de points de vie à enlevés</param>
        public void perdPV(int nb)
        {
            PointsVie -= nb;
            if (PointsVie < 0)
                PointsVie = 0;
        }
        /// <summary>
        /// Test si l'unité est en vie
        /// </summary>
        /// <returns>true si l'unité est en vie, false sinon</returns>
        public bool estEnVie()
        {
            return PointsVie > 0;
        }

        protected StrategySugg strategySuggestion;
        public StrategySugg StrategySuggestion
        {
            get { return strategySuggestion; }
            set { strategySuggestion = value; }
        }


    }
}
