using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public class SuggData
    {
        SuggData(int sugg, int depl)
        {
            this.sugg = sugg;
            this.depl = depl;
        }

        private int sugg;

        public int Sugg
        {
            get { return sugg; }
            set { sugg = value; }
        }
        private int depl;

        public int Depl
        {
            get { return depl; }
            set { depl = value; }
        }
    }
}
