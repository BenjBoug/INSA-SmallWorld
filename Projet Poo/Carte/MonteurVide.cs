using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public class MonteurVide : MonteurCarte
    {
        public MonteurVide()
            : base(new Vide())
        {

        }
        public override void creerCarte()
        {
            carte = new CarteClassique();
            carte.Largeur = 5;
            carte.Hauteur = 5;
            creerStructureCarte();
        }
    }
}
