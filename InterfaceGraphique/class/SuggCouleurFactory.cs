using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace InterfaceGraphique
{
    class SuggCouleurFactory
    {
        public static Color getCouleur(int i)
        {     
            switch (i)
            {
                case 0:
                    return Colors.White;
                case 1:
                    return Colors.Yellow;
                case 2:
                    return Colors.Orange;
                default:
                    return Colors.Transparent;
            }
        }
    }
}
