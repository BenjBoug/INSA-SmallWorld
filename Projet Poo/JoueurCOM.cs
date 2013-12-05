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
                        for(int k=0;k<unites.Count();k++)
                        {
                            u = unites[k];
                            if (u.PointsDepl > 0)
                            {
                                int[][][] allowedMouv = partie.Carte.suggestion(u, i, j);
                                List<int> coordX = new List<int>();
                                List<int> coordY = new List<int>();
                                int max = 0;
                                for (int x = 0; x < carte.Largeur; x++)
                                {
                                    for (int y = 0; y < carte.Hauteur; y++)
                                    {
                                        if (allowedMouv[x][y][0] > max)
                                        {
                                            coordX.Clear();
                                            coordY.Clear();
                                            coordX.Add(x);
                                            coordY.Add(y);
                                            max = allowedMouv[x][y][0];
                                        }
                                        else if (allowedMouv[x][y][0] == max)
                                        {
                                            coordX.Add(x);
                                            coordY.Add(y);
                                        }
                                    }
                                }
                                listUnitoMove.Add(u);
                                Random rand = new Random();
                                int choix = rand.Next(coordX.Count());
                                carte.deplaceUnites(listUnitoMove, coordX[choix], coordY[choix], allowedMouv[coordX[choix]][coordY[choix]][1]);
                            }
                        }
                    }
                }
            }
            //partie.tourSuivant();
        }
    }
}
