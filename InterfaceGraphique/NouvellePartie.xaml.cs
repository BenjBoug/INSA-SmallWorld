using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Modele;
using System.Reflection;
using System.IO;
using Microsoft.Win32;
using System;


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


            for (int i = 0; i < 2; i++)
            {
                GroupeNouveauJoueur grp = new GroupeNouveauJoueur(i);
                panelJoueurs.Children.Add(grp);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Joueur> joueurs = new List<Joueur>();
            foreach (GroupeNouveauJoueur grp in panelJoueurs.Children)
            {
                joueurs.Add(grp.Joueur);
            }

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
                ((MainWindow)Owner).chargerPartie(new MonteurDemo(strategyCreationCarte), joueurs);
            else if (comboCarte.SelectedIndex == 1)
                ((MainWindow)Owner).chargerPartie(new MonteurPetite(strategyCreationCarte), joueurs);
            else if (comboCarte.SelectedIndex == 2)
                ((MainWindow)Owner).chargerPartie(new MonteurNormale(strategyCreationCarte), joueurs);
            this.Close();
            e.Handled = true;
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
                {
                    panelChampsCarte.Visibility = Visibility.Visible;
                    panelTypeCarte.Visibility = Visibility.Collapsed;
                }
                else
                {
                    panelChampsCarte.Visibility = Visibility.Collapsed;
                    panelTypeCarte.Visibility = Visibility.Visible;
                }
            }
        }

        private void comboNbJo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (panelJoueurs != null)
            {
                if (comboNbJo.SelectedIndex == 0)
                {
                    if (panelJoueurs.Children.Count > 2)
                    {
                        for (int i = 2; i < panelJoueurs.Children.Count; i++)
                        {
                            panelJoueurs.Children.RemoveAt(i);
                        }
                    }
                }
                else if (comboNbJo.SelectedIndex == 1)
                {
                    if (panelJoueurs.Children.Count > 3)
                    {
                        for (int i = 3; i < panelJoueurs.Children.Count; i++)
                        {
                            panelJoueurs.Children.RemoveAt(i);
                        }
                    }
                    else if (panelJoueurs.Children.Count < 3)
                    {
                        for (int i = panelJoueurs.Children.Count; i < 3; i++)
                        {
                            GroupeNouveauJoueur grp = new GroupeNouveauJoueur(i);
                            panelJoueurs.Children.Add(grp);
                        }
                    }
                }
                else if (comboNbJo.SelectedIndex == 2)
                {
                    if (panelJoueurs.Children.Count < 4)
                    {
                        for (int i = panelJoueurs.Children.Count; i < 4; i++)
                        {
                            GroupeNouveauJoueur grp = new GroupeNouveauJoueur(i);
                            panelJoueurs.Children.Add(grp);
                        }
                    }
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            openFileDialog1.FileName = null;
            openFileDialog1.Filter = "Carte SmallWorld (.card)|*.card";

            Nullable<bool> res = openFileDialog1.ShowDialog();

            if (res == true)
            {
                fileName.Text = openFileDialog1.FileName;
            }
        }
    }
}
