using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Modele
{
    [XmlInclude(typeof(Partie1v1))]
    public abstract class Partie : IPartie, INotifyPropertyChanged
    {
        public Partie()
        {
            indiceJoueurActuel = 0;
            listJoueurs = new List<Joueur>();
            finpartie = false;
        }

        private bool finpartie;

        public bool Finpartie
        {
            get { return finpartie; }
            set { finpartie = value; }
        }
        private int nbTours;
        private int indiceJoueurActuel;

        public int IndiceJoueurActuel
        {
            get { return indiceJoueurActuel; }
            set { indiceJoueurActuel = value; }
        }

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
            OnPropertyChanged("FinTours");
            //Console.WriteLine("NbTours:" + nbTours);
            if (getNbJoueursVivant() > 1)
            {
                do
                    joueurSuivant();
                while (carte.getNombreUniteRestante(joueurActuel()) == 0);
            }
            else
            {
                Finpartie = true;
            }
        }

        private int getNbJoueursVivant()
        {
            int res=0;
            foreach (Joueur j in listJoueurs)
            {
                if (carte.getNombreUniteRestante(j) > 0)
                    res++;
            }

            return res;
        }

        public void joueurSuivant()
        {
            if (++IndiceJoueurActuel >= listJoueurs.Count)
            {
                IndiceJoueurActuel = 0;
                nbTours++;
                carte.actualiseDeplacement();
                carte.calculerPoints();
            }
        }

        public void ajoutJoueur(Joueur j)
        {
            listJoueurs.Add(j);
            j.Id = listJoueurs.IndexOf(j);
        }

        public Joueur joueurActuel()
        {
            return listJoueurs[IndiceJoueurActuel];
        }

        public void associeJoueursUnite()
        {
            for (int i = 0; i < Carte.Largeur; i++)
            {
                for (int j = 0; j < Carte.Hauteur; j++)
                {
                    if (Carte.Unites[i][j] != null && Carte.Unites[i][j].Count > 0)
                    {
                        foreach (Unite u in Carte.Unites[i][j])
                        {
                            u.Proprietaire = listJoueurs[u.IdProprietaire];
                        }
                    }
                }
            }
        }

        private void finirTour(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged("JoueurFini");
            tourSuivant();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        // Create the OnPropertyChanged method to raise the event 
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
