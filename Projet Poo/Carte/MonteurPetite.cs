using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public class MonteurPetite : MonteurCarte
    {
        public MonteurPetite()
            : base(new Aleatoire())
        {

        }
        /// <summary>
        /// Créer une carte Petite
        /// </summary>
        public override void creerCarte()
        {
            Carte = new CarteClassique();
            Carte.Largeur = 10;
            Carte.Hauteur = 10;
            Carte.NbToursMax = 20;
            Carte.NbUniteClassique = 6;
            Carte.NbUniteElite = 4;
            Carte.NbUniteBlindee = 2;
            creerStructureCarte();
        }
    }
}
