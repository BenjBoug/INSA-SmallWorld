using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Modele
{
    [XmlInclude(typeof(CaseDesert))]
    [XmlInclude(typeof(CasePlaine))]
    [XmlInclude(typeof(CaseEau))]
    [XmlInclude(typeof(CaseMontagne))]
    [XmlInclude(typeof(CaseForet))]
    public abstract class Case : ICase
    {
        public virtual int bonusPoints(Peuple p)
        {
            return 1;
        }

        public virtual bool estAccessible(Peuple p)
        {
            return true;
        }

    }

}
