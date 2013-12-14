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
                        int[][][] allowedMouv = this.StrategySuggestion.getSuggestion(partie.Carte, u);
                        List<Coordonnees> coord = new List<Coordonnees>();
                        int max = 0;
                        for (int x = 0; x < carte.Largeur; x++)
                        {
                            for (int y = 0; y < carte.Hauteur; y++)
                            {
                                if (allowedMouv[x][y][0] > max)
                                {
                                    coord.Clear();
                                    coord.Add(new Coordonnees(x,y));
                                    max = allowedMouv[x][y][0];
                                }
                                else if (allowedMouv[x][y][0] == max && max != 0)
                                {
                                    coord.Add(new Coordonnees(x, y));
                                }
                            }
                        }
                        if (coord.Count > 0)
                        {
                            listUnitoMove.Add(u);
                            int choix = rand.Next(coord.Count());
                            carte.deplaceUnites(listUnitoMove, coord[choix], allowedMouv[coord[choix].X][coord[choix].Y][1], allowedMouv);
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
