using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Modele;
using System.Reflection;
using System.IO;


namespace InterfaceGraphique
{
    /// <summary>
    /// Logique d'interaction pour NouvellePartie.xaml
    /// </summary>
    public partial class NouvellePartie : Window
    {
        public NouvellePartie()
        {
            InitializeComponent();

            List<string> listCouleur = new List<string>();
            listCouleur.Add("Red");
            listCouleur.Add("Blue");
            listCouleur.Add("Yellow");
            listCouleur.Add("Black");

            for (int i = 0; i < 4; i++)
            {
                GroupeNouveauJoueur grp = new GroupeNouveauJoueur();
                panelJoueurs.Children.Add(grp);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Joueur> joueurs = new List<Joueur>();
            joueurs.Add(new JoueurConcret(new FabriquePeupleElfe(), "Blue", "COM1"));
            joueurs.Add(new JoueurCOM(new FabriquePeupleViking(), "Red", "COM2"));
            joueurs.Add(new JoueurCOM(new FabriquePeupleGaulois(), "Black", "COM3"));
            joueurs.Add(new JoueurCOM(new FabriquePeupleNain(), "Yellow", "COM4"));

            ICreationCarte strategyCreationCarte;
            try
            {
                strategyCreationCarte = getSrategyCreationCarte();
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Erreur: le fichier n'existe pas.");
                return;
            }

            if (comboCarte.SelectedIndex == 0)
                ((MainWindow)Owner).loadPartie(new MonteurDemo(strategyCreationCarte), joueurs);
            else if (comboCarte.SelectedIndex == 1)
                ((MainWindow)Owner).loadPartie(new MonteurPetite(strategyCreationCarte), joueurs);
            else if (comboCarte.SelectedIndex == 2)
                ((MainWindow)Owner).loadPartie(new MonteurNormale(strategyCreationCarte), joueurs);
            this.Close();
            e.Handled = true;
        }

        private FabriquePeuple getFabriquePeuple(ComboBox combo)
        {
            if (combo.SelectedIndex == 0)
               return  new FabriquePeupleGaulois();
            else if (combo.SelectedIndex == 1)
                return new FabriquePeupleNain();
            else if (combo.SelectedIndex == 2)
                return new FabriquePeupleViking();
            else
                return new FabriquePeupleNain(); // throw exception en temps normal ...
        }

        private ICreationCarte getSrategyCreationCarte()
        {
            if (comboChargement.SelectedIndex == 0)
                return new Aleatoire();
            else
                return new LectureFichier(fileName.Text);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
            e.Handled = true;
        }

        private void comboChargement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (panelChampsCarte != null)
            {
                if (comboChargement.SelectedIndex == 1)
                    panelChampsCarte.Visibility = Visibility.Visible;
                else
                    panelChampsCarte.Visibility = Visibility.Collapsed;
            }
        }
    }
}
