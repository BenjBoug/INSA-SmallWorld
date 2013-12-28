using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Modele
{
    [XmlInclude(typeof(SuggAgressif))]
    [XmlInclude(typeof(Suggestion))]
    [XmlInclude(typeof(SuggDefensive))]
    public abstract class StrategySugg : IStrategySuggestion
    {
        public abstract SuggMap getSuggestion(Carte carte, Unite unite);
    }
}
