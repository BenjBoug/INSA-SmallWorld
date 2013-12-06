using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Serialization;
using System.ComponentModel;


namespace Modele
{

    public delegate void PointChangeEventHandler(object sender, EventArgs e);

    [XmlInclude(typeof(JoueurConcret))]
    [XmlInclude(typeof(JoueurCOM))]
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

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
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
            set { points = value; OnPointChange(); }
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


        public abstract void jouerTour(Partie partie);

        public abstract void finirTour();

        public event PointChangeEventHandler PointChange;

        protected void OnPointChange()
        {
            PointChangeEventHandler handler = PointChange;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

    }
}
