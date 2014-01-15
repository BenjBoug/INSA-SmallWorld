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
    [XmlInclude(typeof(PartieLocale))]
    public abstract class Partie : IPartie
    {
        public Partie()
        {
            indiceJoueurActuel = 0;
            nbTours = 1;
            listJoueurs = new List<Joueur>();
            finpartie = false;
            classement = new Stack<Joueur>();
        }

        private Stack<Joueur> classement;

        [XmlIgnore]
        public Stack<Joueur> Classement
        {
            get { return classement;}
        }

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
        /// Vérifie si le joueur actuel est le dernier à jouer
        /// </summary>
        public bool dernierAJouer()
        {
            for (int i = IndiceJoueurActuel + 1; i < ListJoueurs.Count; i++)
                if(carte.getNombreUniteRestante(ListJoueurs[i]) != 0)
                    return false;
            return true;
        }
        /// <summary>
        /// Fini le tour, et passe au suivant
        /// </summary>
        public void tourSuivant()
        {
            OnFinTours();
            if (getNbJoueursVivants() == 1 || (NbTours == carte.NbToursMax && dernierAJouer()))
            {
                Finpartie = true;
            }
            else
            {
                do
                {
                    joueurSuivant();
                }
                while (carte.getNombreUniteRestante(joueurActuel()) == 0);
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
                    if (!Classement.Contains(j))
                    {
                        Classement.Push(j);
                    }
                }
            }
            else
            {
                foreach (Joueur j in listJoueurs)
                {
                    if (carte.getNombreUniteRestante(j) == 0)
                    {
                        if (!Classement.Contains(j))
                        {
                            Classement.Push(j);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Retourne le nombre de joueur encore vivant
        /// </summary>
        /// <returns></returns>
        private int getNbJoueursVivants()
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
