using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Modele
{

    public delegate void FinTourEventHandler(object sender, EventArgs e);

    [XmlInclude(typeof(Partie1v1))]
    public abstract class Partie : IPartie
    {
        public Partie()
        {
            indiceJoueurActuel = 0;
            listJoueurs = new List<Joueur>();
            finpartie = false;
            classement = new Stack<Joueur>();
        }

        private Stack<Joueur> classement;


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
            OnFinTours();
            //Console.WriteLine("NbTours:" + nbTours);
            if (nbTours == 20)
            {
                Console.WriteLine();
            }
            if (getNbJoueursVivant() > 1 && nbTours<carte.NbToursMax)
            {
                do
                    joueurSuivant();
                while (carte.getNombreUniteRestante(joueurActuel()) == 0);
            }
            else
            {
                Finpartie = true;
            }
            makeClassement();
        }

        private int CompareForClassemnt(Joueur j1, Joueur j2)
        {
            return j1.Points - j2.Points;
        }

        private void makeClassement()
        {
            if (finpartie)
            {
                listJoueurs.Sort(CompareForClassemnt);
                foreach (Joueur j in listJoueurs)
                {
                    if (!classement.Contains(j))
                    {
                        classement.Push(j);
                    }
                }
            }
            else
            {
                foreach (Joueur j in listJoueurs)
                {
                    if (carte.getNombreUniteRestante(j) == 0)
                    {
                        if (!classement.Contains(j))
                        {
                            classement.Push(j);
                        }
                    }
                }
            }
        }

        public Joueur getGagnant()
        {
            if (Finpartie)
            {
                return classement.Peek();
            }
            else
            {
                return null;
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
        
        public event FinTourEventHandler FinTours;

        protected void OnFinTours()
        {
            FinTourEventHandler handler = FinTours;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
