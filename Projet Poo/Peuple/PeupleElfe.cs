using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    [Serializable]
    public class PeupleElfe : Peuple
    {
        public override string ToString()
        {
            return "Elfe";
        }
        public override int toInt()
        {
            return 3;
        }
    }
}
