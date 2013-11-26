using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public interface IUnite
    {

        void attaquer(IUnite adversaire);

        void perdPV(int nb);

        bool estEnVie();


    }
}
