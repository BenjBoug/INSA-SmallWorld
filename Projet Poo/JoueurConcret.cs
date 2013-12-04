using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public class JoueurConcret : Joueur
    {
        public JoueurConcret(FabriquePeuple fab, String color, String nom) : base(fab,color,nom)
        {

        }

        public JoueurConcret()
            : base()
        {

        }

        public override void jouerTour(Partie partie)
        {

        }
    }
}
