using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public class CaseEau : Case
    {
        public override string ToString()
        {
            return "Eau";
        }
        public override int bonusPoints(IPeuple p)
        {
            return 0;
        }
    }
}
