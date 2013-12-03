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


        protected void creerStructureCarte()
        {
            Carte.Cases = new Case[Carte.Largeur][];
            for (int i = 0; i < Carte.Largeur; i++)
                Carte.Cases[i] = new Case[Carte.Hauteur];

            
            Carte.Unites = new List<Unite>[Carte.Largeur][];
            for (int i = 0; i < Carte.Largeur; i++)
            {
                Carte.Unites[i] = new List<Unite>[Carte.Hauteur];
            }

            for (int i = 0; i < Carte.Largeur; i++)
            {
                for (int j = 0; j < Carte.Hauteur; j++)
                {
                    Carte.Unites[i][j] = new List<Unite>();
                }
            }
            

            Carte.chargerCarte(new Aleatoire());
        }

    }
}
