using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Modele;
using System.Reflection;


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
            joueurs.Add(new JoueurConcret(new FabriquePeupleNain(), "Blue"/*(couleurJoueur1.SelectedItem as PropertyInfo).Name*/, "COM1"));
            joueurs.Add(new JoueurCOM(new FabriquePeupleGaulois(), "Red"/*(couleurJoueur2.SelectedItem as PropertyInfo).Name*/, "COM2"));
            joueurs.Add(new JoueurCOM(new FabriquePeupleViking(), "Black"/*(couleurJoueur2.SelectedItem as PropertyInfo).Name*/, "COM3"));
            joueurs.Add(new JoueurCOM(new FabriquePeupleViking(), "Yellow"/*(couleurJoueur2.SelectedItem as PropertyInfo).Name*/, "COM4"));

            if (comboCarte.SelectedIndex == 0)
                ((MainWindow)Owner).loadPartie(new MonteurDemo(new Aleatoire()), joueurs);
            else if (comboCarte.SelectedIndex == 1)
                ((MainWindow)Owner).loadPartie(new MonteurPetite(new Aleatoire()), joueurs);
            else if (comboCarte.SelectedIndex == 2)
                ((MainWindow)Owner).loadPartie(new MonteurNormale(new Aleatoire()), joueurs);
            this.Close();
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void comboChargement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboChargement.SelectedIndex == 1)
                panelChampsCarte.Visibility = Visibility.Visible;
           /* else
                panelChampsCarte.Visibility = Visibility.Collapsed;*/
        }
    }
}
