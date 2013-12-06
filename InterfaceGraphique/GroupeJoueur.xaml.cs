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
        Joueur joueur;

        public Joueur Joueur
        {
            get { return joueur; }
            set { joueur = value; }
        }
        public GroupeJoueur(Joueur joueur)
        {
            this.joueur = joueur;
            InitializeComponent();
            grpJoueur.Header = joueur.Nom;
            couleur.Text += joueur.Couleur;
            peuple.Text += joueur.Peuple;
            nbpoints.Text += joueur.Points;
            joueur.PointChange += refreshPointLabel;
            this.IsEnabledChanged += griser;
        }

        private void griser(object o, DependencyPropertyChangedEventArgs e)
        {
            this.Opacity = 0.5;
        }

        private void refreshPointLabel(object o, EventArgs e)
        {
            Joueur j = (Joueur)o;
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                nbpoints.Text = "Points : " + j.Points;
            }));
        }

        public void select(Joueur joueurActif)
        {
            if (joueurActif == Joueur)
            {
                grpJoueur.Background = Brushes.White;
            }
            else
            {
                grpJoueur.Background = null;
            }
        }
    }
}
