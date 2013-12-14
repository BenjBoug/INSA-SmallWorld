using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public interface ICase
    {
        int bonusPoints(Peuple p);
        bool estAccessible(Peuple p);
        //int toInt();
    }
}
