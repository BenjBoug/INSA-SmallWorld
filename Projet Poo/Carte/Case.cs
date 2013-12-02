using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public abstract class Case : ICase
    {
        public virtual int bonusPoints(IPeuple p)
        {
            return 1;
        }

    }

}
