using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public interface IPeuple
    {

        int calculPoints(Carte c, Unite u);
        int toInt();
    }
}
