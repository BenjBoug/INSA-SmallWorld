using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public abstract class Partie : IPartie
    {
        public Partie()
        {
            listJoueurs = new List<IJoueur>();
        }

        private int nbTours;

        public int NbTours
        {
            get { return nbTours; }
            set { nbTours = value; }
        }
        private List<IJoueur> listJoueurs;

        public List<IJoueur> ListJoueurs
        {
            get { return listJoueurs; }
            set { listJoueurs = value; }
        }
        private Carte carte;

        public Carte Carte
        {
            get { return carte; }
            set { carte = value; }
        }

        public void tourSuivant()
        {
            carte.actualiseDeplacement();
        }

        public IJoueur joueurSuivant()
        {
            return listJoueurs[0];
        }

        public void ajoutJoueur(Joueur j)
        {
            ListJoueurs.Add(j);
        }

    }
}
