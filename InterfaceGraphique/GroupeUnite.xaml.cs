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
    /// Logique d'interaction pour GroupeUnite.xaml
    /// </summary>
    public partial class GroupeUnite : UserControl
    {
        private Unite unit;

        public Unite Unit
        {
            get { return unit; }
        }
        private bool selected;

        public bool Selected
        {
            get { return selected; }
        }
        private bool selectable;

        public bool Selectable
        {
            set { selectable = value; }
            get { return selected; }
        }

        public GroupeUnite(Unite unit)
        {
            InitializeComponent();
            this.unit = unit;
            selected = false;
            selectable = false;
            grpUnit.Header = unit.ToString() + " " + unit.Proprietaire.Peuple;
            PV.Text = unit.PointsVie.ToString()+"/"+unit.PointsVieMax.ToString();
            PA.Text = unit.PointsAttaque.ToString();
            PDef.Text = unit.PointsDefense.ToString();
            PDepl.Text = unit.PointsDepl.ToString();
            grpUnit.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString(unit.Proprietaire.Couleur);
        }

        private void grpUnit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            selected = !Selected;
            if (Selected && unit.PointsDepl>0 && selectable)
                grpUnit.Background = Brushes.White;
            else
                grpUnit.Background = null;
        }
    }
}
