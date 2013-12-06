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

        public JoueurCOM(FabriquePeuple fab, String color, String nom)
            : base(fab, color, nom)
        {
            unitesSelect = new List<Unite>();
        }

        private List<Unite> unitesSelect;


        public override void jouerTour(Partie partie)
        {
            Carte carte = partie.Carte;
            Random rand = new Random();

            //chercher unites

            for (int i = 0; i < carte.Largeur; i++)
            {
                for (int j = 0; j < carte.Hauteur; j++)
                {
                    List<Unite> unites = carte.Unites[i][j];
                    if (unites != null && unites.Count > 0 && unites[0].IdProprietaire == this.Id)
                    {
                        List<Unite> listUnitoMove = new List<Unite>();
                        Unite u;
                        int nbUnit = unites.Count();
                        for(int k=0;k<nbUnit;k++)
                        {
                            listUnitoMove.Clear();
                            u = unites[0];
                            if (u.PointsDepl > 0)
                            {
                                int[][][] allowedMouv = partie.Carte.suggestion(u, i, j);
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
                                    carte.deplaceUnites(listUnitoMove, coord[choix].X, coord[choix].Y, allowedMouv[coord[choix].X][coord[choix].Y][1]);
                                }
                            }
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
