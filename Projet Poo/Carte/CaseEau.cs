﻿using System;
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
    }
}
