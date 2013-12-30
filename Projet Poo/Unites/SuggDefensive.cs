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
                if (pair.Value.Sugg != 0)
                {
                    double minDist = double.MaxValue;
                    Coordonnees coordCase = pair.Key;
                    Coordonnees coord=null;
                    foreach (Unite u in carte.Unites.Where(z => z.IdProprietaire != unite.IdProprietaire))
                    {
                        if (coordCase.distance(u.Coord) < minDist)
                        {
                            minDist = coordCase.distance(u.Coord);
                            coord = u.Coord;
                        }
                    }
                    meilleursCases.Add(coordCase);
                }
            }

            foreach (Coordonnees c in meilleursCases)
            {
                Console.WriteLine(c);
            }


            return res;
        }
    }
}
