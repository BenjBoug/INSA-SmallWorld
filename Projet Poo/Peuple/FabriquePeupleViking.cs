﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public class FabriquePeupleViking : FabriquePeuple
    {
    
        public override IPeuple creerPeuple()
        {
            return new PeupleViking();
        }
    }
}
