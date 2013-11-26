using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public class JoueurConcret : Joueur
    {
        public JoueurConcret(FabriquePeuple fab, String color, String nom) : base(fab,color)
        {
            Nom = nom;
        }
        private String nom;

        public String Nom
        {
            get { return nom; }
            set { nom = value; }
        }
    }
}
