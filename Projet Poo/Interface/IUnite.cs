using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public interface IUnite
    {
        Joueur Proprietaire
        {
            get;
            set;
        }
        int PointsDepl
        {
            get;
            set;
        }
        int PointsVie
        {
            get;
            set;
        }
        int PointsAttaque
        {
            get;
            set;
        }
        int PointsDefense
        {
            get;
            set;
        }

        void attaquer(IUnite adversaire);
        void perdPV(int nb);
        bool estEnVie();
    }
}
