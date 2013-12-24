using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibCLR;

namespace Modele
{
    [Serializable]
    public class Suggestion : StrategySugg
    {
        /// <summary>
        /// Récupère les suggestions en fonction du peuple de l'unité et de la case
        /// </summary>
        /// <param name="carte"></param>
        /// <param name="unite"></param>
        /// <returns></returns>
        public override SuggMap getSuggestion(Carte carte, Unite unite)
        {

            int x = unite.Coord.X;
            int y = unite.Coord.Y;
            WrapperSuggestion wrap = new WrapperSuggestion();

            List<int> emplUnites = getUnitesToList(carte, unite);

            int peuple = unite.Proprietaire.Peuple.toInt();

            List<int> sugg = wrap.getSuggestion(carte.toList(), emplUnites, carte.Largeur, x, y, unite.PointsDepl, (int)peuple);

            return listIntToSuggMap(carte, sugg);
        }

        private static SuggMap listIntToSuggMap(Carte carte, List<int> sugg)
        {
            SuggMap res = new SuggMap();
            for (int i = 0; i < carte.Largeur; i++)
            {
                for (int j = 0; j < carte.Hauteur; j++)
                {
                    SuggData data = new SuggData();
                    data.Sugg = sugg[i * carte.Largeur + j];
                    res[new Coordonnees(i, j)] = data;
                }
            }

            for (int i = 0; i < carte.Largeur; i++)
            {
                for (int j = 0; j < carte.Hauteur; j++)
                {
                    res[new Coordonnees(i, j)].Depl = sugg[carte.Largeur * carte.Hauteur + (i * carte.Largeur + j)];
                }
            }

            return res;
        }

        protected List<int> getUnitesToList(Carte carte, Unite unite)
        {
            List<int> res = new List<int>();
            List<Unite> tmp;
            for (int i = 0; i < carte.Largeur; i++)
            {
                for (int j = 0; j < carte.Hauteur; j++)
                {
                    tmp = carte.getUniteFromCoord(new Coordonnees(i, j));
                    if (tmp.Count > 0 && tmp[0].IdProprietaire != unite.IdProprietaire)
                        res.Add(tmp.Count);
                    else
                        res.Add(0);
                }
            }

            return res;
        }
    }
}
