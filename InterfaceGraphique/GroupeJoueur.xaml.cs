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
    /// Logique d'interaction pour GroupeJoueur.xaml
    /// </summary>
    public partial class GroupeJoueur : UserControl
    {
        public GroupeJoueur(IJoueur j, bool actif=false)
        {
            InitializeComponent();
            grpJoueur.Header = j.Nom;
            couleur.Text += j.Couleur;
            peuple.Text += j.Peuple;
            nbpoints.Text += j.Points;
            if (actif)
                grpJoueur.Background = Brushes.White;
        }
    }
}
