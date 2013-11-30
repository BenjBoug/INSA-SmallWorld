using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public class CaseForet : Case
    {
        public override string ToString()
        {
            return "Foret";
        }
        public int bonusPoints(PeupleNain p)
        {
            return 2;
        }
    }
}
