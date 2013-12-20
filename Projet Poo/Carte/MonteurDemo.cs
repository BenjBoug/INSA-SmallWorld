using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public class MonteurDemo : MonteurCarte
    {
        public MonteurDemo(ICreationCarte crea)
            : base(crea)
        {

        }
        /// <summary>
        /// Créer une carte Démo
        /// </summary>
        public override void creerCarte()
        {
            Carte = new CarteClassique();
            Carte.Largeur = 5;
            Carte.Hauteur = 5;
            Carte.NbToursMax = 20;
            Carte.NbUniteParPeuble = 5;
            Carte.NbUniteClassique = 5;
            Carte.NbUniteElite = 3;
            Carte.NbUniteBlindee = 1;
            creerStructureCarte();
        }
    }
}
