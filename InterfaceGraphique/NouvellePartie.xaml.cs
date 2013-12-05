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

            couleurJoueur1.ItemsSource = typeof(Colors).GetProperties();
            couleurJoueur1.SelectedIndex = 0;

            couleurJoueur2.ItemsSource = typeof(Colors).GetProperties();
            couleurJoueur2.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<IJoueur> joueurs = new List<IJoueur>();
            joueurs.Add(new JoueurCOM(new FabriquePeupleNain(), "Blue"/*(couleurJoueur1.SelectedItem as PropertyInfo).Name*/, nomJoueur1.Text + "COM"));
            joueurs.Add(new JoueurCOM(new FabriquePeupleViking(), "Red"/*(couleurJoueur2.SelectedItem as PropertyInfo).Name*/, nomJoueur2.Text + "COM"));
            joueurs.Add(new JoueurCOM(new FabriquePeupleViking(), "Black"/*(couleurJoueur2.SelectedItem as PropertyInfo).Name*/, nomJoueur2.Text + "COM"));
            joueurs.Add(new JoueurCOM(new FabriquePeupleViking(), "Yellow"/*(couleurJoueur2.SelectedItem as PropertyInfo).Name*/, nomJoueur2.Text + "COM"));

            if (comboCarte.SelectedIndex == 0)
                ((MainWindow)Owner).loadPartie(new MonteurDemo(), joueurs);
            else if (comboCarte.SelectedIndex == 1)
                ((MainWindow)Owner).loadPartie(new MonteurPetite(), joueurs);
            else if (comboCarte.SelectedIndex == 2)
                ((MainWindow)Owner).loadPartie(new MonteurNormale(), joueurs);
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
    }
}
