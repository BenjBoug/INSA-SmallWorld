﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public class Unite : IUnite
    {
        public Unite()
        {
            pointsAttaque = 2;
            pointsDefense = 1;
            pointsDepl = 1;
            pointsVie = 2;
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
        private IJoueur proprietaire;

        public IJoueur Proprietaire
        {
            get { return proprietaire; }
            set { proprietaire = value; }
        }


        public void attaquer(IUnite adversaire)
        {
            throw new NotImplementedException();
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
