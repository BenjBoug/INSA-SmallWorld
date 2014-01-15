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

        public abstract void creerPartie(MonteurCarte monteurCarte, List<Joueur> joueurs);
        public abstract void initJoueurs(List<Joueur> joueurs);
        public abstract Carte creerCarte(MonteurCarte monteur);
        public abstract List<Unite> creerUnite(Joueur j);
    }
}
