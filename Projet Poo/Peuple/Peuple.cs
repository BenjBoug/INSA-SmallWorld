using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Modele
{
    [XmlInclude(typeof(PeupleGaulois))]
    [XmlInclude(typeof(PeupleNain))]
    [XmlInclude(typeof(PeupleViking))]
    public abstract class Peuple : IPeuple
    {
        public virtual int calculPoints(Carte c, Unite u)
        {
            return c.Cases[u.Coord.X][u.Coord.Y].bonusPoints(this);
        }
    }
}
