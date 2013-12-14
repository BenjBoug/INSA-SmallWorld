using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public class SuggPacifiste : Suggestion
    {
        public override int[][][] getSuggestion(Carte carte, Unite unite)
        {
            int[][][] res = base.getSuggestion(carte, unite);

            // on applique des coefficients negatif sur les positions ou il y a des unites

            return res;
        }
    }
}
