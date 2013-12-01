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

        }

        public void deplacerUnite(IUnite u, int x, int y)
        {

        }

        public override IUnite getAdversaire()
        {
            return new Unite();
        }

        public override void calculerPoints()
        {
            for (int i = 0; i < Largeur; i++)
            {
                for (int j = 0; j < Hauteur; j++)
                {
                    List<IUnite> list = Unites[i][j];
                    if (list!=null)
                    foreach (IUnite u in list)
                    {
                        IJoueur joueur = u.Proprietaire;
                        IPeuple peuple = u.Proprietaire.Peuple;

                        joueur.Points += Cases[i][j].bonusPoints(peuple);

                    }
                }
            }
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

        public override void placeUnite(List<IUnite> list)
        {
            WrapperMapAleatoire wrap = new WrapperMapAleatoire();
            List<int> emplUnites = new List<int>();
            for (int i = 0; i < Largeur; i++)
            {
                for (int j = 0; j < Hauteur; j++)
                {
                    if (Unites[i][j] != null)
                        emplUnites.Add(Unites[i][j].Count);
                    else
                        emplUnites.Add(0);
                }
            }
            List<int> coord = wrap.getEmplacementJoueur(emplUnites,Largeur);
            unites[coord[0]][coord[1]] = list;
        }

    }
}
