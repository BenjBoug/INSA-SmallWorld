using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public class MonteurPetite : MonteurCarte
    {
        public override void creerCarte()
        {
            Carte = new CarteClassique();
            Carte.Largeur = 10;
            Carte.Hauteur = 10;
            Carte.NbToursMax = 20;
            Carte.NbUniteParPeuble = 6;
            creerStructureCarte();
        }
    }
}
