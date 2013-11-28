using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


namespace Modele
{
    public abstract class Joueur : IJoueur
    {
        public Joueur(FabriquePeuple fab, string color)
        {
            fabriquePeuple = fab;
            Peuple = fabriquePeuple.creerPeuple();
            Points = 0;
            Couleur = color;
        }

        private FabriquePeuple fabriquePeuple;

        public FabriquePeuple FabriquePeuple
        {
            get { return fabriquePeuple; }
            set { fabriquePeuple = value; }
        }

        private IPeuple _peuple;

        public IPeuple Peuple
        {
            get { return _peuple; }
            set { _peuple = value; }
        }

        private int points;

        public int Points
        {
            get { return points; }
            set { points = value; }
        }

        private String couleur;

        public String Couleur
        {
            get { return couleur; }
            set { couleur = value; }
        }
    }
}
