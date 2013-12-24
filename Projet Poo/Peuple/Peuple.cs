using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Modele
{
    [Serializable]
    [XmlInclude(typeof(PeupleGaulois))]
    [XmlInclude(typeof(PeupleNain))]
    [XmlInclude(typeof(PeupleViking))]
    [XmlInclude(typeof(PeupleElfe))]
    public abstract class Peuple : IPeuple
    {
        /// <summary>
        /// Calcul les points
        /// </summary>
        /// <param name="c"></param>
        /// <param name="u"></param>
        /// <returns></returns>
        public virtual int calculPoints(Carte c, Unite u)
        {
            return c.Cases[u.Coord.X][u.Coord.Y].bonusPoints(this);
        }

        public abstract int toInt();
    }
}
