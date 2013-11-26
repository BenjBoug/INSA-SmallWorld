using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public class MonteurNormale : MonteurCarte
    {
        public override void creerCarte()
        {
            Carte = new CarteClassique();
            Carte.Largeur = 20;
            Carte.Hauteur = 20;
            Carte.NbToursMax = 25;
            Carte.NbUniteParPeuble = 15;
            creerStructureCarte();
        }
    }
}
