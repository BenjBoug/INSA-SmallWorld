using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibCLR;

namespace Modele
{
    public class CarteClassique : Carte
    {
        public CarteClassique() : base()
        {
            unites = new List<Unite>[Largeur][];
            for (int i = 0; i < Largeur; i++)
                unites[i] = new List<Unite>[Hauteur];
        }

        public void deplacerUnite(Unite u, int x, int y)
        {

        }

        public override IUnite getAdversaire()
        {
            return new Unite();
        }

        public override void calculerPoints()
        {
            throw new NotImplementedException();
        }


        public override bool caseVide(int x, int y)
        {
            if (Unites[x][y].Count == 0)
                return true;
            else
                return false;
        }


        public override void selectionneCase(int x, int y)
        {
            throw new NotImplementedException();
        }

        public override void selectionneUnite(IUnite unite)
        {

        }

        public unsafe override void placeUnite(List<Unite> list)
        {
            int nbJoueurs = 0;
            Random rand = new Random();

            for (int i = 0; i < Largeur; i++)
            {
                for (int j = 0; j < Hauteur; j++)
                {
                    if (unites[i][j] != null)
                    {
                        nbJoueurs += unites[i][j].Count;
                    }
                }
            }


            int[] coord = new int[2];

            if (nbJoueurs == 0)
            {
                coord[0] = rand.Next(1) * Largeur;
                coord[1] = rand.Next(1) * Hauteur;
            }
            else
            {
                do
                {
                    coord[0] = rand.Next(1) * Largeur;
                    coord[1] = rand.Next(1) * Hauteur;
                } while (unites[coord[0]][coord[1]] != null && unites[coord[0]][coord[1]].Count>=1);
            }


            unites[coord[0]][coord[1]] = list;
        }

    }
}
