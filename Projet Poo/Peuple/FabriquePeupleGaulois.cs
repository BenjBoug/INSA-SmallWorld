using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public class FabriquePeupleGaulois : FabriquePeuple
    {
    
        public override IPeuple creerPeuple()
        {
            return new PeupleGaulois();
        }
    }
}
