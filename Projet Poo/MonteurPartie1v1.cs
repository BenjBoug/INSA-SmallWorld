using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public class MonteurPartie1v1 : MonteurPartie
    {
        private const int NBJoueur = 2;

        public override void creerPartie(MonteurCarte monteurCarte, FabriquePeuple fabPeuple)
        {
            partie = new Partie1v1();
            partie.Carte = creerCarte(monteurCarte);
            partie.NbTours = 0;
            creerJoueurs(fabPeuple);
        }

        public override void creerJoueurs(FabriquePeuple fab)
        {
            for (int i = 0; i < NBJoueur; i++)
            {
                Joueur joueur = new JoueurConcret(fab, "blue", "joueur"+i);
                partie.ajoutJoueur(joueur);
                creerUnite(joueur);
            }
        }

        public override Carte creerCarte(MonteurCarte monteur)
        {
            monteur.creerCarte();
            return monteur.Carte;
        }

        public override void creerUnite(IJoueur j)
        {
            List<Unite> list = new List<Unite>();
            for (int i = 0; i < partie.Carte.NbUniteParPeuble; i++)
            {
                Unite unit = new Unite();
                unit.setProprietaire(j);
                list.Add(unit);
            }
            partie.Carte.placeUnite(list);
        }
    }
}
