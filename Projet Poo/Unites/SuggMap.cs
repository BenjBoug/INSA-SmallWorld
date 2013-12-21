using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public class SuggMap : Dictionary<Coordonnees,SuggData>
    {

        public bool coordOk(Coordonnees c)
        {
            return this.Keys.Contains(c);
        }
    }
}
