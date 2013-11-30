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
            Carte.Cases = new ICase[Carte.Largeur][];
            for (int i = 0; i < Carte.Largeur; i++)
                Carte.Cases[i] = new ICase[Carte.Hauteur];

            
            Carte.Unites = new List<IUnite>[Carte.Largeur][];
            for (int i = 0; i < Carte.Largeur; i++)
                Carte.Unites[i] = new List<IUnite>[Carte.Hauteur];
            

            Carte.chargerCarte(new Aleatoire());
        }

    }
}
