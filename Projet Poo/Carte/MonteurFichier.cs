using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public class MonteurFichier : MonteurCarte
    {
        public MonteurFichier(string file)
            : base(new LectureFichier(file))
        {

        }
        public override void creerCarte()
        {
            Carte = new CarteClassique();
            stragCreation.chargerCarte(ref carte);
        }
    }
}
