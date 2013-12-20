using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    [Serializable]
    public class PeupleViking : Peuple
    {
        public override string ToString()
        {
            return "Viking";
        }

        public override int calculPoints(Carte c, Unite u)
        {
            int bonus = 0;
            Coordonnees coord = u.Coord;
            for(int i=0;i<c.Largeur;i++)
            {
                for(int j=0;j<c.Hauteur;j++)
                {
                    if (coord.X - 1 > 0 && c.Cases[coord.X-1][coord.Y] is CaseEau)
                    {
                        bonus = 1;
                    }
                    if (coord.X + 1 < c.Largeur && c.Cases[coord.X + 1][coord.Y] is CaseEau)
                    {
                        bonus = 1;
                    }
                    if (coord.Y - 1 > 0 && c.Cases[coord.X][coord.Y-1] is CaseEau)
                    {
                        bonus = 1;
                    }
                    if (coord.Y + 1 < c.Hauteur && c.Cases[coord.X][coord.Y+1] is CaseEau)
                    {
                        bonus = 1;
                    }
                }
            }
            return base.calculPoints(c,u) + bonus;
        }
    }
}
