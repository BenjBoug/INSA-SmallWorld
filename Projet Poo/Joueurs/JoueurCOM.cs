using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    [Serializable]
    public class JoueurCOM : Joueur
    {
        public JoueurCOM() : base()
        {
            unitesSelect = new List<Unite>();
        }

        public JoueurCOM(FabriquePeuple fab, String color, String nom)
            : base(fab, color, nom)
        {
            unitesSelect = new List<Unite>();
        }

        [NonSerialized]
        private List<Unite> unitesSelect;

        /// <summary>
        /// Pour chaque unité de l'IA, on récupère les suggestions de déplacement de l'unité, et on la déplace vers
        /// la case avec le coefficient de suggestion le plus élevé.
        /// </summary>
        /// <param name="partie">la partie en cours</param>
        public override void jouerTour(Partie partie)
        {
            Carte carte = partie.Carte;
            Random rand = new Random();


            List<Unite> unites = carte.getUniteJoueur(this);
            if (unites.Count > 0)
            {
                foreach(Unite u in unites)
                {
                    if (u.PointsDepl > 0)
                    {
                        SuggMap allowedMouv = u.StrategySuggestion.getSuggestion(partie.Carte, u);
                        List<Coordonnees> coord = new List<Coordonnees>();
                        int max = 0;
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
                            int choix = rand.Next(coord.Count());
                            carte.deplaceUnite(u, coord[choix], allowedMouv);
                        }
                    }
                }
            }
            finirTour();
        }
        /// <summary>
        /// Aucune action spécial à faire à la fin du tour...
        /// </summary>
        public override void finirTour()
        {
            
        }
    }
}
