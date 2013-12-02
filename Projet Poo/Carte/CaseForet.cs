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
        public override int bonusPoints(IPeuple p)
        {
            if (p is PeupleNain)
                return 2;
            else
                return base.bonusPoints(p);
        }
    }
}
