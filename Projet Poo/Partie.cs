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

    [Serializable]
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
        /// <summary>
        /// Est vrai si la partie est fini (cad que le nombre de tours max a était atteind ou qu'il ne reste qu'un seul joueur vivant
        /// </summary>
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
        public Carte Carte
        {
            get { return carte; }
            set { carte = value; }
        }
        /// <summary>
        /// Fini le tour, et passe au suivant
        /// </summary>
        public void tourSuivant()
        {
            OnFinTours();
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
        /// <summary>
        /// Fonction de comparaison pour créer le classement.
        /// </summary>
        /// <param name="j1"></param>
        /// <param name="j2"></param>
        /// <returns></returns>
        private int CompareForClassemnt(Joueur j1, Joueur j2)
        {
            return j1.Points - j2.Points;
        }
        /// <summary>
        /// Créer le classement
        /// </summary>
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
        /// <summary>
        /// retourne le gagnant de la partie
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Retourne le nombre de joueur encore vivant
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Passe au joueur suivant, si tout les joueurs ont joués, on incrémente le nombre de tours, et on reprend au début de la liste des joueurs.
        /// </summary>
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
        /// <summary>
        /// Ajoute un joueur à la partie
        /// </summary>
        /// <param name="j"></param>
        public void ajoutJoueur(Joueur j)
        {
            listJoueurs.Add(j);
            j.Id = listJoueurs.IndexOf(j);
        }
        /// <summary>
        /// Retourne le joueur courant
        /// </summary>
        /// <returns></returns>
        public Joueur joueurActuel()
        {
            return listJoueurs[IndiceJoueurActuel];
        }
        /// <summary>
        /// Associe les ids du proprietaire de chaque unite à l'objet joueur
        /// </summary>
        public void associeJoueursUnite()
        {
            foreach (Unite u in Carte.Unites)
            {
                u.Proprietaire = listJoueurs[u.IdProprietaire];
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
