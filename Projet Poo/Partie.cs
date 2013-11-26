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
        private List<IJoueur> listJoueurs;
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
            // selection le joueur suivant
            listJoueurs.GetEnumerator().MoveNext();
            return listJoueurs.GetEnumerator().Current;
        }

        public void ajoutJoueur(Joueur j)
        {
            listJoueurs.Add(j);
        }

    }
}
