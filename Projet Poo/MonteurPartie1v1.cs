using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public class MonteurPartie1v1 : MonteurPartie
    {

        public override void creerPartie(MonteurCarte monteurCarte, List<Joueur> joueurs)
        {
            partie = new Partie1v1();
            partie.Carte = creerCarte(monteurCarte);
            partie.NbTours = 0;
            initJoueurs(joueurs);
        }

        public override void initJoueurs(List<Joueur> joueurs)
        {
            foreach (Joueur j in joueurs)
            {
                partie.ajoutJoueur(j);
                creerUnite(j);
            }
        }

        public override Carte creerCarte(MonteurCarte monteur)
        {
            monteur.creerCarte();
            return monteur.Carte;
        }

        public override void creerUnite(Joueur j)
        {
            List<Unite> list = new List<Unite>();
            for (int i = 0; i < partie.Carte.NbUniteClassique; i++)
            {
                Unite unit = new Unite();
                unit.Proprietaire = j;
                list.Add(unit);
            }
            for (int i = 0; i < partie.Carte.NbUniteElite; i++)
            {
                Unite unit = new UniteElite();
                unit.Proprietaire = j;
                list.Add(unit);
            }
            for (int i = 0; i < partie.Carte.NbUniteBlindee; i++)
            {
                Unite unit = new UniteBlindee();
                unit.Proprietaire = j;
                list.Add(unit);
            }
            partie.Carte.placeUnite(list);
        }
    }
}
