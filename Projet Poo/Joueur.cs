using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Serialization;


namespace Modele
{
    [XmlInclude(typeof(JoueurConcret))]
    public abstract class Joueur : IJoueur
    {
        public Joueur()
        {

        }

        public Joueur(FabriquePeuple fab, String color, String nom)
        {
            Peuple = fab.creerPeuple();
            Points = 0;
            Couleur = color;
            Nom = nom;
        }

        private Peuple _peuple;

        public Peuple Peuple
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

        private String nom;

        public String Nom
        {
            get { return nom; }
            set { nom = value; }
        }
    }
}
