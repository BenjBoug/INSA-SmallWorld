﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    [Serializable]
    public class UniteBlindee : Unite
    {
        private int deplSupp;
        public UniteBlindee()
        {
            pointsAttaque = 1;
            pointsDefense = 2;
            pointsDepl = 1;
            pointsVie = 3;
            pointsVieMax = pointsVie;
            strategySuggestion = new SuggDefensive();
            deplSupp = 0;
        }
        public override int getDeplSuppl()
        {
            return (deplSupp++)%2;
        }

        public override string ToString()
        {
            return "Unité blindée";
        }
    }
}
