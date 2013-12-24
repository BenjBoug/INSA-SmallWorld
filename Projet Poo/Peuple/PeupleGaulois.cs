using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    [Serializable]
    public class PeupleGaulois : Peuple
    {
        public override int toInt()
        {
            return 1;
        }

        public override string ToString()
        {
            return "Gaulois";
        }
    }
}
