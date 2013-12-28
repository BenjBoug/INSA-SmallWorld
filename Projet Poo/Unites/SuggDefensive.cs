using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public class SuggDefensive : Suggestion
    {
        public override SuggMap getSuggestion(Carte carte, Unite unite)
        {
            SuggMap res = base.getSuggestion(carte, unite);

            // on cherche la meilleur case possible la plus éloigné des ennemis

            List<Coordonnees> meilleursCases = new List<Coordonnees>();

            foreach (var pair in res)
            {
                int minDist = int.MaxValue;
                Coordonnees coordCase = pair.Key;
                foreach (Unite u in carte.Unites.Where(z => z.IdProprietaire != unite.IdProprietaire))
                {

                }
            }


            return res;
        }
    }
}
