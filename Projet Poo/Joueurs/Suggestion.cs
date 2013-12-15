using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibCLR;

namespace Modele
{
    public class Suggestion : StrategySugg
    {
        protected enum PeupleInt { Gaulois = 1, Viking = 0, Nain = 2 };
        protected enum CaseInt { Plaine = 0, Eau = 1, Montagne = 2, Desert = 3, Foret = 4 };

        public override SuggMap getSuggestion(Carte carte, Unite unite)
        {

            int x = unite.Coord.X;
            int y = unite.Coord.Y;
            WrapperSuggestion wrap = new WrapperSuggestion();

            List<int> emplUnites = getUnitesToList(carte, unite);
            List<int> carteInt = getCasesToList(carte);

            PeupleInt peuple = PeupleInt.Gaulois;
            IPeuple p = unite.Proprietaire.Peuple;

            if (p is PeupleViking)
                peuple = PeupleInt.Viking;
            else if (p is PeupleNain)
                peuple = PeupleInt.Nain;
            else if (p is PeupleGaulois)
                peuple = PeupleInt.Gaulois;

            List<int> sugg = wrap.getSuggestion(carteInt, emplUnites, carte.Largeur, x, y, unite.PointsDepl, (int)peuple);

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

        protected List<int> getCasesToList(Carte carte)
        {
            List<int> res = new List<int>();
            for (int i = 0; i < carte.Largeur; i++)
            {
                for (int j = 0; j < carte.Hauteur; j++)
                {
                    CaseInt tile = CaseInt.Plaine;
                    if (carte.Cases[i][j] is CaseDesert)
                    {
                        tile = CaseInt.Desert;
                    }
                    else if (carte.Cases[i][j] is CaseEau)
                    {
                        tile = CaseInt.Eau;
                    }
                    else if (carte.Cases[i][j] is CaseForet)
                    {
                        tile = CaseInt.Foret;
                    }
                    else if (carte.Cases[i][j] is CaseMontagne)
                    {
                        tile = CaseInt.Montagne;
                    }
                    else if (carte.Cases[i][j] is CasePlaine)
                    {
                        tile = CaseInt.Plaine;
                    }
                    res.Add((int)tile);
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
