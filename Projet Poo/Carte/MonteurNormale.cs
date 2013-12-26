using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public class MonteurNormale : MonteurCarte
    {
        public MonteurNormale(ICreationCarte crea)
            : base(crea)
        {

        }
        /// <summary>
        /// Créer une carte Normale
        /// </summary>
        public override void creerCarte()
        {
            Carte = new CarteClassique();
            Carte.Largeur = 15;
            Carte.Hauteur = 15;
            Carte.NbToursMax = 30;
            Carte.NbUniteClassique = 8;
            Carte.NbUniteElite = 5;
            Carte.NbUniteBlindee = 3;
            creerStructureCarte();
        }
    }
}
