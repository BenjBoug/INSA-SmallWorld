using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public class CaseMontagne : Case
    {
        public override string ToString() 
        {
            return "Désert";
        }
        public override int bonusPoints(Peuple p)
        {
            if (p is PeupleGaulois)
                return 0;
            else
                return base.bonusPoints(p);
        }
    }
}
