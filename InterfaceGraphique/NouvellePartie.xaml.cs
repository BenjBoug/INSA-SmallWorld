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
using System.Windows.Shapes;
using Modele;

namespace InterfaceGraphique
{
    /// <summary>
    /// Logique d'interaction pour NouvellePartie.xaml
    /// </summary>
    public partial class NouvellePartie : Window
    {
        public int selectionCarte;
        public NouvellePartie()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            selectionCarte = comboCarte.SelectedIndex;
            /*
            if (comboCarte.SelectedIndex == 0)
                ((MainWindow)Owner).loadPartie(new MonteurDemo());
            else if (comboCarte.SelectedIndex == 1)
                ((MainWindow)Owner).loadPartie(new MonteurPetite());
            else if (comboCarte.SelectedIndex == 2)
                ((MainWindow)Owner).loadPartie(new MonteurNormale());
            */
            this.Close();
        }
    }
}
