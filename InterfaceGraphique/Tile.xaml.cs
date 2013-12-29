using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Modele;
using System.Windows.Media.Effects;

namespace InterfaceGraphique
{
    /// <summary>
    /// Logique d'interaction pour Tile.xaml
    /// </summary>
    public partial class Tile : UserControl
    {
        ICase tile;

        public ICase TileType
        {
            get { return tile; }
            set { tile = value; }
        }
        bool selected;

        public bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        int nbUnite;

        public int NbUnite
        {
            get { return nbUnite; }
            set { nbUnite = value; }
        }

        private Grid aPanel;

        public Brush Background
        {
            get { return aPanel.Background; }
        }


        public Tile(ICase tile, TileFactory tileFactory, List<Unite> listUnite, SolidColorBrush playerBrush)
        {
            InitializeComponent();
            this.tile = tile;
            this.nbUnite = listUnite.Count;
            VisualBrush myBrush = new VisualBrush();
            aPanel = new Grid();

            aPanel.Background = tileFactory.getViewTile(tile);

            // Create some text.
            TextBlock backText = new TextBlock();
            Canvas.SetZIndex(backText, 3);
            if (listUnite != null && listUnite.Count > 0)
            {
                backText.Text = listUnite.Count.ToString();
                backText.Foreground =  (SolidColorBrush)new BrushConverter().ConvertFromString(listUnite[0].Proprietaire.Couleur);
            }
            else
                backText.Text = " ";
            if(backText.Text != " ")
                backText.Background = Brushes.White;
            FontSizeConverter fSizeConverter = new FontSizeConverter();
            backText.FontSize = (double)fSizeConverter.ConvertFromString("10pt");
            backText.Margin = new Thickness(10);
            Grid.SetColumn(backText, 0);
            Grid.SetRow(backText, 0);
            DropShadowEffect myDropShadowEffect  = new DropShadowEffect();
            myDropShadowEffect.BlurRadius = 1;
            myDropShadowEffect.Color = Color.FromRgb(0,0,0);
            myDropShadowEffect.ShadowDepth = 2;
            backText.Effect=myDropShadowEffect;
            aPanel.Children.Add(backText);
            myBrush.Visual = aPanel;
            rect.Fill = myBrush;
            rect1.Stroke = playerBrush;
        }

        private void rect_MouseEnter(object sender, MouseEventArgs e)
        {
            rect1.Opacity = 0.7;
            //rect1.StrokeThickness= 3;
            e.Handled = true;
        }

        private void rect_MouseLeave(object sender, MouseEventArgs e)
        {
            rect1.Opacity = 0;
            //rect1.StrokeThickness = 0;
            e.Handled = true;
        }
    }
}
