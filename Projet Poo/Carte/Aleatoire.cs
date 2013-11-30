using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibCLR;
namespace Modele
{
    public class Aleatoire : ICreationCarte
    {

        public void chargerCarte(Carte c)
        {
            WrapperMapAleatoire alea = new WrapperMapAleatoire();
            List<int> data = alea.generer(c.Largeur,5);
            for (int i = 0; i < c.Largeur; i++)
            {
                for (int j = 0; j < c.Hauteur; j++)
                {
                    c.setCase(i, j, c.FabriqueCase.getCase(data[i*c.Largeur + j]));
                }
            }
        }
    }
}
