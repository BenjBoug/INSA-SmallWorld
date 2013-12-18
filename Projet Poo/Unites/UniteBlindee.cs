using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public class UniteBlindee : Unite
    {
        private int deplSupp;
        public UniteBlindee()
        {
            pointsAttaque = 1;
            pointsDefense = 3;
            pointsDepl = 1;
            pointsVie = 3;
            pointsVieMax = pointsVie;
            strategySuggestion = new SuggPacifiste();
            deplSupp = 0;
        }
        public override int getDeplSuppl()
        {
            return (deplSupp++)%2;
        }
    }
}
