using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public class FabriquePeupleNain : FabriquePeuple
    {
    
        public override Peuple creerPeuple()
        {
            return new PeupleNain();
        }
    }
}
