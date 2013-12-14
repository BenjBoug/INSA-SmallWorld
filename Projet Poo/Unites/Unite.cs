using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Modele
{
    public class Unite : IUnite
    {
        public Unite()
        {
            pointsAttaque = 5;
            pointsDefense = 3;
            pointsDepl = 1;
            pointsVie = 10;
            pointsVieMax = pointsVie;
            coord = new Coordonnees();
        }

        private Coordonnees coord;

        public Coordonnees Coord
        {
            get { return coord; }
            set { coord = value; }
        }

        private int pointsDefense;

        public int PointsDefense
        {
            get { return pointsDefense; }
            set { pointsDefense = value; }
        }
        private int pointsAttaque;

        public int PointsAttaque
        {
            get { return pointsAttaque; }
            set { pointsAttaque = value; }
        }

        private int pointsVieMax;

        public int PointsVieMax
        {
            get { return pointsVieMax; }
            set { pointsVieMax = value; }
        }


        private int pointsVie;

        public int PointsVie
        {
            get { return pointsVie; }
            set { pointsVie = value; }
        }
        private int pointsDepl;

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


        public void attaquer(IUnite unitDef)
        {
            Random randCombat = new Random();
            Random rand = new Random();
            int nbToursCombat = 3 + randCombat.Next((Math.Max(this.PointsVie, unitDef.PointsVie)) + 2);
            int n = 0;
            //Console.WriteLine("combat nbTours "+nbToursCombat);
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
                //Console.WriteLine(ratioChanceDef+" "+ratioCombat+" "+ratioVie);
                if (ratioCombat <= ratioChanceDef)
                {
                    // Console.WriteLine(unit.Proprietaire.Nom+" gagne tour " + (n+1));
                    unitDef.perdPV(1);
                }
                else
                {
                    //Console.WriteLine(unit.Proprietaire.Nom + " perd tour" + (n + 1));
                    this.perdPV(1);
                }
                n++;
            }
        }

        public void perdPV(int nb)
        {
            PointsVie -= nb;
            if (PointsVie < 0)
                PointsVie = 0;
        }

        public bool estEnVie()
        {
            return PointsVie > 0;
        }


    }
}
