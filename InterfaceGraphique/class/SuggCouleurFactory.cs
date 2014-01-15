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
            return Color.FromRgb(255,255,(byte)(85*i));
            /*
            switch (i)
            {
                case 0:
                    return Brushes.White;
                case 1:
                    return Brushes.Yellow;
                case 2:
                    return Brushes.Orange;
                case 3:
                    return Brushes.Red;
                default:
                    return Brushes.Black;
            }*/
        }
    }
}
