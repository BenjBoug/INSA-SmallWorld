using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public class FabriquePeupleElfe : FabriquePeuple
    {
        public override Peuple creerPeuple()
        {
            return new PeupleElfe();
        }
    }
}
