using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public interface IJoueur
    {
        Peuple Peuple
        {
            get;
            set;
        }
        int Points
        {
            get;
            set;
        }
        String Couleur
        {
            get;
            set;
        }
        String Nom
        {
            get;
            set;
        }
    }
}
