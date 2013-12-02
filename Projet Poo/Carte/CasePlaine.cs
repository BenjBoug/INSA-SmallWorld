using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public class CasePlaine : Case
    {
        public override string ToString()
        {
            return "Plaine";
        }
        public override int bonusPoints(IPeuple p)
        {
            if (p is PeupleNain)
                return 0;
            else if (p is PeupleGaulois)
                return 2;
            else
                return base.bonusPoints(p);
        }
    }
}
