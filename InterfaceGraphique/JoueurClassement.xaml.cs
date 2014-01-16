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
    /// Logique d'interaction pour JoueurClassement.xaml
    /// </summary>
    public partial class JoueurClassement : UserControl
    {
        public JoueurClassement(Joueur j, int rang)
        {
            InitializeComponent();
            nom.Text = j.Nom;
            nbPoint.Text = j.Points.ToString() + "pts";
            string packUri = null;
            switch (rang)
            {
                case 1 :
                    packUri = "../../Resources/Trophy-gold.png";
                    img.Width = 75;
                    img.Height = 75;
                    nbPoint.FontSize = 25;
                    nom.FontSize = 25;
                    break;
                case 2:
                    packUri = "../../Resources/Trophy-silver.png";
                    img.Width = 60;
                    img.Height = 60;
                    nbPoint.FontSize = 20;
                    nom.FontSize = 20;
                    break;
                case 3:
                    packUri = "../../Resources/Trophy-bronze.png";
                    img.Width = 50;
                    img.Height = 50;
                    nbPoint.FontSize = 17;
                    nom.FontSize = 17;
                    break;
                case 4:
                    packUri = "../../Resources/chocolate.png";
                    img.Width = 45;
                    img.Height = 45;
                    nbPoint.FontSize = 14;
                    nom.FontSize = 14;
                    break;

            }
            img.Source = new ImageSourceConverter().ConvertFromString(packUri) as ImageSource;
        }
    }
}
