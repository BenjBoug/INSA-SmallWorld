using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    [Serializable]
    public class SuggPacifiste : Suggestion
    {
        public override SuggMap getSuggestion(Carte carte, Unite unite)
        {
            SuggMap res = base.getSuggestion(carte, unite);

            // on applique des coefficients negatif sur les positions ou il y a des unites

            return res;
        }
    }
}
