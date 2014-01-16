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
using System.Reflection;

namespace InterfaceGraphique
{
    /// <summary>
    /// Logique d'interaction pour GroupeNouveauJoueur.xaml
    /// </summary>
    public partial class GroupeNouveauJoueur : UserControl
    {
        
        List<String> listCouleur = new List<string>();
        int numJoueur;

        public GroupeNouveauJoueur(int i)
        {
            InitializeComponent();
            numJoueur = i+1;

            listCouleur.Add("Blue");
            listCouleur.Add("Red");
            listCouleur.Add("Green");
            listCouleur.Add("Yellow");
            listCouleur.Add("Brown");
            listCouleur.Add("Navy");
            listCouleur.Add("Olive");
            listCouleur.Add("Pink");
            listCouleur.Add("Salmon");
            listCouleur.Add("Violet");

            couleurJoueur.DataContext = listCouleur;

            couleurJoueur.SelectedIndex = i;
            peupleJoueur.SelectedIndex = i;

            nomJoueur.Text = "Joueur " + numJoueur;
        }
        public Joueur Joueur
        {
            get 
            {
                Joueur joueur;
                joueur = getTypeJoueur();
                joueur.Nom = nomJoueur.Text;
                joueur.Peuple = getFabriquePeuple().creerPeuple();
                joueur.Couleur = listCouleur[couleurJoueur.SelectedIndex];
                
                return joueur; 
            }
        }

        private FabriquePeuple getFabriquePeuple()
        {
            if (peupleJoueur.SelectedIndex == 0)
                return new FabriquePeupleGaulois();
            else if (peupleJoueur.SelectedIndex == 1)
                return new FabriquePeupleNain();
            else if (peupleJoueur.SelectedIndex == 2)
                return new FabriquePeupleViking();
            else if (peupleJoueur.SelectedIndex == 3)
                return new FabriquePeupleElfe();
            else
                return null; // throw exception en temps normal ...
        }

        private Joueur getTypeJoueur()
        {
            if (typeJoueur.SelectedIndex == 0)
                return new JoueurConcret();
            else if (typeJoueur.SelectedIndex == 1)
                return new JoueurCOM();
            else
                return null; // throw exception en temps normal ...
        }

        private void typeChanged(object sender, SelectionChangedEventArgs e)
        {
            if (typeJoueur.SelectedIndex == 1)
            {
                nomJoueur.Text = "COM" + numJoueur;
                nomJoueur.IsEnabled = false;
            }
            else
            {
                if (nomJoueur != null)
                {
                    nomJoueur.Text = "Joueur " + numJoueur;
                    nomJoueur.IsEnabled = true;
                }
            }
        }
    }
}
