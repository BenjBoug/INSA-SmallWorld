using System;
using System.Windows.Media;
using Modele;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Resources;

namespace InterfaceGraphique
{
    class ImageFactory : TileFactory
    {
        private BitmapImage tileDesert = null;
        private BitmapImage tileEau = null;
        private BitmapImage tileForet = null;
        private BitmapImage tileMontagne = null;
        private BitmapImage tilePlaine = null;

        public ImageFactory()
        {
            styleName = "default";
        }

        public ImageFactory(string style)
        {
            styleName = style;
        }

        private string styleName;

        public override Brush getViewTile(ICase tile)
        {
            ImageBrush brush = new ImageBrush();
            if (tile is CaseDesert)
            {
                if (tileDesert == null)
                    tileDesert = new BitmapImage(new Uri("../../Resources/" + styleName + "/caseDesert.png", UriKind.Relative));
                brush.ImageSource = tileDesert;
            }
            else if (tile is CaseEau)
            {
                if (tileEau == null)
                    tileEau = new BitmapImage(new Uri("../../Resources/" + styleName + "/caseEau.png", UriKind.Relative));
                brush.ImageSource = tileEau;
            }
            else if (tile is CaseForet)
            {
                if (tileForet == null)
                    tileForet = new BitmapImage(new Uri("../../Resources/" + styleName + "/caseForet.png", UriKind.Relative));
                brush.ImageSource = tileForet;
            }
            else if (tile is CaseMontagne)
            {
                if (tileMontagne == null)
                    tileMontagne = new BitmapImage(new Uri("../../Resources/" + styleName + "/caseMontagne.png", UriKind.Relative));
                brush.ImageSource = tileMontagne;
            }
            else if (tile is CasePlaine)
            {
                if (tilePlaine == null)
                    tilePlaine = new BitmapImage(new Uri("../../Resources/" + styleName + "/casePlaine.png", UriKind.Relative));
                brush.ImageSource = tilePlaine;
            }
            return brush;
        }
    }
}
