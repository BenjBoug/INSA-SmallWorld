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
        public Tile(ICase tile, TileFactory tileFactory, List<Unite> listUnite)
        {
            InitializeComponent();
            this.tile = tile;
            VisualBrush myBrush = new VisualBrush();
            StackPanel aPanel = new StackPanel();

            aPanel.Background = tileFactory.getViewTile(tile);

            // Create some text.
            TextBlock someText = new TextBlock();
            if (listUnite != null && listUnite.Count > 0)
            {
                someText.Text = listUnite.Count.ToString();
                someText.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(listUnite[0].Proprietaire.Couleur);
            }
            else
                someText.Text = " ";
            FontSizeConverter fSizeConverter = new FontSizeConverter();
            someText.FontSize = (double)fSizeConverter.ConvertFromString("10pt");
            someText.Margin = new Thickness(10);
            aPanel.Children.Add(someText);

            myBrush.Visual = aPanel;

            rect.Fill = myBrush;
            
        }

        private void rect_MouseEnter(object sender, MouseEventArgs e)
        {
            rect.StrokeThickness = 3;
            rect.Stroke = Brushes.Yellow;
        }

        private void rect_MouseLeave(object sender, MouseEventArgs e)
        {
            rect.StrokeThickness = 0;
           // rect.Stroke = color;
        }
    }
}
