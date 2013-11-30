using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Modele;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace InterfaceGraphique
{
    class RectangleFactory : TileFactory
    {
        public override Brush getViewTile(ICase tile)
        {
            if (tile is CaseDesert)
            {
                return Brushes.LightGoldenrodYellow;
            }
            else if (tile is CaseEau)
            {
                return Brushes.LightBlue;
            }
            else if (tile is CaseForet)
            {
                return Brushes.Green;
            }
            else if (tile is CaseMontagne)
            {
                return Brushes.SaddleBrown;
            }
            else if (tile is CasePlaine)
            {
                return Brushes.LightGreen;
            }
            else
                return Brushes.Black;
        }
    }
}
