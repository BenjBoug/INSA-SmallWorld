using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibCLR;

namespace Modele
{
    [Serializable]
    public class CaseDesert : Case
    {
        public override string ToString() 
        {
            return "Désert";
        }

        public override int bonusPoints(Peuple p)
        {
            if (p is PeupleViking)
                return 0;
            else
                return base.bonusPoints(p);
        }

        public override int toInt()
        {
            return (int)CaseInt.Desert;
        }
    }
}
