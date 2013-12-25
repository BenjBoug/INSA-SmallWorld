using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Modele
{
    [Serializable]
    public class SuggMap : Dictionary<Coordonnees,SuggData>
    {
        public SuggMap() : base()
        {

        }

        protected SuggMap(SerializationInfo info, StreamingContext context) : base(info,context)
        {

        }

        public bool coordOk(Coordonnees c)
        {
            return this.Keys.Contains(c);
        }
    }
}
