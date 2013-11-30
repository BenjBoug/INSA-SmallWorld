using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public abstract class MonteurPartie
    {
        protected Partie partie;

        public Partie Partie
        {
            get { return partie; }
            set { partie = value; }
        }

        public abstract void creerPartie(MonteurCarte monteurCarte, List<IJoueur> joueurs);
        public abstract void initJoueurs(List<IJoueur> joueurs);
        public abstract Carte creerCarte(MonteurCarte monteur);
        public abstract void creerUnite(IJoueur j);
    }
}
