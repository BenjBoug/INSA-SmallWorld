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

    }
}
