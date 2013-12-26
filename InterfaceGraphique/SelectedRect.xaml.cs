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
    public delegate void SelectChangedEventHandler(object sender, EventArgs e);
    /// <summary>
    /// Logique d'interaction pour SelectedRect.xaml
    /// </summary>
    public partial class SelectedRect : UserControl
    {
        public SelectedRect()
        {
            InitializeComponent();
        }

        private Tile tileSelected;

        public Tile TileSelected
        {
            get { return tileSelected; }
            set { tileSelected = value; OnSelectChanged(); }
        }

        private Coordonnees coord;

        public Coordonnees Coord
        {
            get { return coord; }
            set { coord = value; }
        }

        public event SelectChangedEventHandler SelectChanged;

        protected void OnSelectChanged()
        {
            SelectChangedEventHandler handler = SelectChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
