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
            List<Joueur> joueurs = new List<Joueur>();
            joueurs.Add(new JoueurConcret(getFabriquePeuple(peupleJoueur1), (couleurJoueur1.SelectedItem as PropertyInfo).Name, nomJoueur1.Text));
            joueurs.Add(new JoueurConcret(getFabriquePeuple(peupleJoueur2), (couleurJoueur2.SelectedItem as PropertyInfo).Name, nomJoueur2.Text));

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
    }
}
