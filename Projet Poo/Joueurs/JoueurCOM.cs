using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public class JoueurCOM : Joueur
    {
        public JoueurCOM() : base()
        {
            unitesSelect = new List<Unite>();
        }

        public JoueurCOM(FabriquePeuple fab, String color, String nom, StrategySugg sugg)
            : base(fab, color, nom,sugg)
        {
            unitesSelect = new List<Unite>();
        }

        private List<Unite> unitesSelect;


        public override void jouerTour(Partie partie)
        {
            Carte carte = partie.Carte;
            Random rand = new Random();


            List<Unite> unites = carte.getUniteJoueur(this);
            if (unites.Count > 0)
            {
                List<Unite> listUnitoMove = new List<Unite>();
                int nbUnit = unites.Count();
                foreach(Unite u in unites)
                {
                    listUnitoMove.Clear();
                    if (u.PointsDepl > 0)
                    {
                        SuggMap allowedMouv = this.StrategySuggestion.getSuggestion(partie.Carte, u);
                        List<Coordonnees> coord = new List<Coordonnees>();
                        int max = 0;
                        Console.WriteLine("{0}",
                        allowedMouv.Count);
                        foreach (var pair in allowedMouv)
                        {
                            if (pair.Value.Sugg > max)
                            {
                                coord.Clear();
                                coord.Add(pair.Key);
                                max = pair.Value.Sugg;
                            }
                            else if (pair.Value.Sugg == max && max != 0)
                            {
                                coord.Add(pair.Key);
                            }
                        }
                        if (coord.Count > 0)
                        {
                            listUnitoMove.Add(u);
                            int choix = rand.Next(coord.Count());
                            carte.deplaceUnites(listUnitoMove, coord[choix], allowedMouv);
                        }
                    }
                }
            }
        }

        public override void finirTour()
        {
            
        }
    }
}
