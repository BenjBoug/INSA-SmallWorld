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
            indiceJoueurActuel = 0;
            listJoueurs = new List<IJoueur>();
        }

        private int nbTours;
        private int indiceJoueurActuel;

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
        private ICarte carte;

        public ICarte Carte
        {
            get { return carte; }
            set { carte = value; }
        }

        public void tourSuivant()
        {
            joueurSuivant();
        }

        public void joueurSuivant()
        {
            if (++indiceJoueurActuel >= listJoueurs.Count)
            {
                indiceJoueurActuel = 0;
                nbTours++;
                carte.actualiseDeplacement();
                carte.calculerPoints();
            }
        }

        public void ajoutJoueur(IJoueur j)
        {
            listJoueurs.Add(j);
        }

        public IJoueur joueurActuel()
        {
            return listJoueurs[indiceJoueurActuel];
        }

    }
}
