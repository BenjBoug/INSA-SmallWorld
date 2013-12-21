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
        public GroupeNouveauJoueur()
        {
            InitializeComponent();
        }

        private Joueur joueur;

        public Joueur Joueur
        {
            get 
            {
                joueur = getTypeJoueur(typeJoueur);
                joueur.Nom = nomJoueur.Text;
                joueur.Peuple = getFabriquePeuple(peupleJoueur).creerPeuple();
                joueur.Couleur = (couleurJoueur.SelectedItem as PropertyInfo).Name;
                
                return joueur; 
            }
        }

        private FabriquePeuple getFabriquePeuple(ComboBox combo)
        {
            if (combo.SelectedIndex == 0)
                return new FabriquePeupleGaulois();
            else if (combo.SelectedIndex == 1)
                return new FabriquePeupleNain();
            else if (combo.SelectedIndex == 2)
                return new FabriquePeupleViking();
            else
                return new FabriquePeupleNain(); // throw exception en temps normal ...
        }

        private Joueur getTypeJoueur(ComboBox combo)
        {
            if (combo.SelectedIndex == 0)
                return new JoueurConcret();
            else if (combo.SelectedIndex == 1)
                return new JoueurCOM();
            else
                return new JoueurConcret(); // throw exception en temps normal ...
        }
    }
}
