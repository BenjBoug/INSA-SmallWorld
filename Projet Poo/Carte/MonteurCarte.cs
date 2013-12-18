using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public abstract class MonteurCarte
    {
        private Carte carte;

        public Carte Carte
        {
            get { return carte; }
            set { carte = value; }
        }

        public abstract void creerCarte();

        /// <summary>
        /// Créer les structures de données d'une carte
        /// </summary>
        protected void creerStructureCarte()
        {
            Carte.Cases = new Case[Carte.Largeur][];
            for (int i = 0; i < Carte.Largeur; i++)
                Carte.Cases[i] = new Case[Carte.Hauteur];

            
            Carte.Unites = new List<Unite>();

            Carte.chargerCarte(new Aleatoire());
        }

    }
}
