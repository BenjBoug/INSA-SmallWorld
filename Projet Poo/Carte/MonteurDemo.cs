using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public class MonteurDemo : MonteurCarte
    {
        public override void creerCarte()
        {
            Carte = new CarteClassique();
            Carte.Largeur = 5;
            Carte.Hauteur = 5;
            Carte.NbToursMax = 5;
            Carte.NbUniteParPeuble = 4;
            creerStructureCarte();
        }
    }
}
