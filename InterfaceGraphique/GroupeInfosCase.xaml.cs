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


namespace InterfaceGraphique
{
    /// <summary>
    /// Logique d'interaction pour GroupeInfosCase.xaml
    /// </summary>
    public partial class GroupeInfosCase : UserControl
    {
        public GroupeInfosCase()
        {
            InitializeComponent();
            typeCase.Text = "Type : ";
            nbUnite.Text = "Nb Unités: ";
            coordX.Text = "Coord. x : ";
            coordY.Text = "Coord. y : ";
            /*
            if (selectionRectangle.Visibility == Visibility.Visible)
            {
                rect.Fill = tileFactory.getViewTile(selectionRectangle.TileSelected.TileType);
                column = (int)Canvas.GetLeft(selectionRectangle) / 50;
                row = (int)Canvas.GetTop(selectionRectangle) / 50;
                typeCase.Text +=(selectionRectangle.TileSelected.TileType).ToString();
                nbUnite.Text += selectionRectangle.TileSelected.NbUnite;
                coordX.Text += column.ToString();
                coordY.Text += row.ToString();
            }
            else
            {
                rect.Fill = Brushes.Black;
            }

            selectionRectangle.SelectChanged += actualiseData;*/
        }

        public void actualiseData(object sender, EventArgs e)
        {
            var selectionRectangle = (SelectedRect)sender;
            typeCase.Text = "Type : ";
            nbUnite.Text = "Nb Unités: ";
            coordX.Text = "Coord. x : ";
            coordY.Text = "Coord. y : ";
            if (selectionRectangle.Visibility == Visibility.Visible)
            {
                rect.Fill = selectionRectangle.TileSelected.Background;
                int column = (int)Canvas.GetLeft(selectionRectangle) / 50;
                int row = (int)Canvas.GetTop(selectionRectangle) / 50;
                typeCase.Text += (selectionRectangle.TileSelected.TileType).ToString();
                nbUnite.Text += selectionRectangle.TileSelected.NbUnite;
                coordX.Text += column.ToString();
                coordY.Text += row.ToString();
            }
            else
            {
                rect.Fill = Brushes.Black;
            }
        }
    }
}
