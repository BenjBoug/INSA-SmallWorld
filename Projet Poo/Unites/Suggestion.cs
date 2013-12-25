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
            List<int> sugg = new WrapperSuggestion().getSuggestion(carte.toList(),                      //la carte
                                                                   getUnitesToList(carte, unite),       //l'emplacement des unités ennemis
                                                                   carte.Largeur,                       //la taille de la carte
                                                                   unite.Coord.X,                       //coordonnees x de l'unité
                                                                   unite.Coord.Y,                       //coordonnees y de l'unité
                                                                   unite.PointsDepl,                    //le nombre de points de déplacement de l'unité
                                                                   unite.Proprietaire.Peuple.toInt());  //le peuple de l'unité

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
