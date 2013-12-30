using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public class Vide : ICreationCarte
    {
        public void chargerCarte(ref Carte c)
        {
            for (int i = 0; i < c.Largeur; i++)
            {
                for (int j=0;j<c.Hauteur;j++)
                {
                    c.setCase(i, j, c.FabriqueCase.getCase((int)Case.CaseInt.Plaine));
                }
            }
        }
    }
}
