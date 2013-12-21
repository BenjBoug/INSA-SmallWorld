using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibCLR;

namespace Modele
{
    [Serializable]
    public class CaseEau : Case
    {
        public override string ToString()
        {
            return "Eau";
        }
        public override int bonusPoints(Peuple p)
        {
            return 0;
        }

        public override bool estAccessible(Peuple p)
        {
            if (p is PeupleViking)
                return true;
            else
                return false;
        }
        public override int toInt()
        {
            return (int)CaseInt.Eau;
        }
    }
}
