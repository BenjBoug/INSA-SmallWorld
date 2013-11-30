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

        public int bonusPoints(PeupleNain p)
        {
            return 0;
        }

        public int bonusPoints(PeupleGaulois p)
        {
            return 2;
        }
    }
}
