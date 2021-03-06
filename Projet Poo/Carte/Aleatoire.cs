﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibCLR;
namespace Modele
{
    public class Aleatoire : ICreationCarte
    {
        /// <summary>
        /// Charge la carte de façon aléatoire
        /// </summary>
        /// <param name="c">la carte a chargé</param>
        public void chargerCarte(ref Carte c)
        {
            WrapperMapAleatoire alea = new WrapperMapAleatoire();
            List<int> data = alea.generer(c.Largeur, c.Hauteur,5);
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
