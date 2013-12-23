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

    [Serializable]
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
        /// <summary>
        /// Entier représentant un joueur 
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private Peuple _peuple;
        /// <summary>
        /// Le peuple du joueur
        /// </summary>
        public Peuple Peuple
        {
            get { return _peuple; }
            set { _peuple = value; }
        }

        private int points;
        /// <summary>
        /// Les points que le joueur a gagné
        /// </summary>
        public int Points
        {
            get { return points; }
            set { points = value; OnPointChange(); }
        }

        private String couleur;
        /// <summary>
        /// La couleur représentant le joueur
        /// </summary>
        public String Couleur
        {
            get { return couleur; }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentNullException(); 
                couleur = value;
            }
        }

        private String nom;
        /// <summary>
        /// Le nom du joueur
        /// </summary>
        public String Nom
        {
            get { return nom; }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentNullException(); 
                nom = value;
            }
        }

        /// <summary>
        /// Fait jouer un tour le joueur
        /// </summary>
        /// <param name="partie">la partie sur laquel le joueur joue</param>
        public abstract void jouerTour(Partie partie);
        /// <summary>
        /// Fini un tour
        /// </summary>
        public abstract void finirTour();
        /// <summary>
        /// Evenement déclencher quand les points du joueurs change
        /// </summary>
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
