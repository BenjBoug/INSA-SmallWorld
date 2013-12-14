using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Modele
{
    [XmlInclude(typeof(SuggAgressif))]
    [XmlInclude(typeof(Suggestion))]
    [XmlInclude(typeof(SuggPacifiste))]
    public abstract class StrategySugg : IStrategySuggestion
    {
        public abstract int[][][] getSuggestion(Carte carte, Unite unite);
    }
}
