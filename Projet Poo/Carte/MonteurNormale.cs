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
            Carte.Largeur = 15;
            Carte.Hauteur = 15;
            Carte.NbToursMax = 30;
            Carte.NbUniteParPeuble = 8;
            creerStructureCarte();
        }
    }
}
