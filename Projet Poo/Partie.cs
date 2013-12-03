using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Modele
{
    [XmlInclude(typeof(Partie1v1))]
    public abstract class Partie : IPartie
    {
        public Partie()
        {
            indiceJoueurActuel = 0;
            listJoueurs = new List<Joueur>();
        }

        private int nbTours;
        private int indiceJoueurActuel;

        public int NbTours
        {
            get { return nbTours; }
            set { nbTours = value; }
        }
        private List<Joueur> listJoueurs;

        public List<Joueur> ListJoueurs
        {
            get { return listJoueurs; }
            set { listJoueurs = value; }
        }
        private Carte carte;

        [XmlElement("CarteClassique", typeof(CarteClassique))]
        public Carte Carte
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

        public void ajoutJoueur(Joueur j)
        {
            listJoueurs.Add(j);
        }

        public Joueur joueurActuel()
        {
            return listJoueurs[indiceJoueurActuel];
        }

    }
}
