using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Modele;
using System.Xml.Serialization;
using System.IO;
using System.Threading;
using Microsoft.Win32;

namespace InterfaceGraphique
{
    /// <summary>
    /// Logique d'interaction pour EditeurCarte.xaml
    /// </summary>
    public partial class EditeurCarte : Window
    {
        const int TAILLE_MIN = 5;
        const int TAILLE_MAX = 30;
        const int TOURS_MIN = 5;
        const int TOURS_MAX = 50;
        const int UNITES_MIN = 5;
        const int UNITES_MAX = 50;

        private TileFactory tileFactory;
       // private Carte carte;
        private MonteurCarte monteur;
        private int terrain;

        public EditeurCarte()
        { 
            InitializeComponent();
            tileFactory = new ImageFactory();
            terrain = 0;
            remplirCombo();
        }

        private void remplirCombo()
        {   
            for (int i = TAILLE_MIN; i <= TAILLE_MAX; i++)
            {
                comboLargeur.Items.Add(i);
                comboHauteur.Items.Add(i);
            }
            for (int i = TOURS_MIN; i <= TOURS_MAX; i++)
                comboTours.Items.Add(i);
            for (int i = 0; i <= UNITES_MAX; i++)
            {
                comboUC.Items.Add(i);
                comboUE.Items.Add(i);
                comboUB.Items.Add(i);
            }
        }

        private void initCombo() 
        {
            comboLargeur.SelectedIndex = monteur.Carte.Largeur - TAILLE_MIN;
            comboHauteur.SelectedIndex = monteur.Carte.Hauteur - TAILLE_MIN;
            comboTours.SelectedIndex = monteur.Carte.NbToursMax - TOURS_MIN;
            comboUC.SelectedIndex = monteur.Carte.NbUniteClassique;
            comboUE.SelectedIndex = monteur.Carte.NbUniteElite;
            comboUB.SelectedIndex = monteur.Carte.NbUniteBlindee;
        }

        private void afficheCarte()
        {
            canvasMap.Children.Clear();
            for (int l = 0; l < monteur.Carte.Hauteur; l++)
            {
                for (int c = 0; c < monteur.Carte.Largeur; c++)
                {
                    var tile = monteur.Carte.Cases[c][l];
                    var unite = monteur.Carte.getUniteFromCoord(new Coordonnees(c, l));
                    var rect = creerTile(c, l, tile, unite);
                    canvasMap.Children.Add(rect);
                }
            }
        }

        private Tile creerTile(int c, int l, ICase tile, List<Unite> listUnite)
        {
            var rectangle = new Tile(tile, tileFactory, listUnite, Brushes.White);

            Canvas.SetLeft(rectangle, c * 50);
            Canvas.SetTop(rectangle, l * 50);
            Canvas.SetZIndex(rectangle, 5);

            rectangle.MouseLeftButtonDown += new MouseButtonEventHandler(Rectangle_MouseDown);
            return rectangle;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var rect = sender as Tile;
            int column = (int)Canvas.GetLeft(rect) / 50;
            int row = (int)Canvas.GetTop(rect) / 50;
            monteur.Carte.setCase(column, row, monteur.Carte.FabriqueCase.getCase(terrain));
            afficheCarte();
            e.Handled = true;
        }

        private void MenuItem_Click_Nouvelle(object sender, RoutedEventArgs e)
        {
            disableComboBox_SelectionChanged();

            monteur = new MonteurVide();
            monteur.creerCarte();

            initCombo();

            enableComboBox_SelectionChanged(); 

            canvasMap.Width = monteur.Carte.Largeur * 50;
            canvasMap.Height = monteur.Carte.Hauteur * 50;
            afficheCarte();
        }

        private void disableComboBox_SelectionChanged()
        {
            comboLargeur.SelectionChanged -= ComboBox_SelectionChanged_Taille;
            comboHauteur.SelectionChanged -= ComboBox_SelectionChanged_Taille;
            comboTours.SelectionChanged -= ComboBox_SelectionChanged_Tours;
            comboUC.SelectionChanged -= ComboBox_SelectionChanged_UC;
            comboUE.SelectionChanged -= ComboBox_SelectionChanged_UE;
            comboUB.SelectionChanged -= ComboBox_SelectionChanged_UB;
        }

        private void enableComboBox_SelectionChanged()
        {
            comboLargeur.SelectionChanged += ComboBox_SelectionChanged_Taille;
            comboHauteur.SelectionChanged += ComboBox_SelectionChanged_Taille;
            comboTours.SelectionChanged += ComboBox_SelectionChanged_Tours;
            comboUC.SelectionChanged += ComboBox_SelectionChanged_UC;
            comboUE.SelectionChanged += ComboBox_SelectionChanged_UE;
            comboUB.SelectionChanged += ComboBox_SelectionChanged_UB;
        }

        private void ComboBox_SelectionChanged_Taille(object sender, SelectionChangedEventArgs e)
        {
            Carte cp_carte = new CarteClassique();
            if (monteur.Carte.Largeur > 0 && monteur.Carte.Hauteur > 0)
            {
                cp_carte.Largeur = monteur.Carte.Largeur;
                cp_carte.Hauteur = monteur.Carte.Hauteur;
                cp_carte.Cases = monteur.Carte.Cases;
            }
            monteur.Carte.Largeur = (int)comboLargeur.SelectedItem;
            monteur.Carte.Hauteur = (int)comboHauteur.SelectedItem;
            monteur.Carte.Cases = new Case[monteur.Carte.Largeur][];
            for (int i = 0; i < monteur.Carte.Largeur; i++)
                monteur.Carte.Cases[i] = new Case[monteur.Carte.Hauteur];
            for (int i = 0; i < monteur.Carte.Largeur; i++)
            {
                for (int j = 0; j < monteur.Carte.Hauteur; j++)
                {
                    monteur.Carte.setCase(i, j, monteur.Carte.FabriqueCase.getCase(0));
                }
            }
            for (int i = 0; i < Math.Min(monteur.Carte.Largeur, cp_carte.Largeur); i++)
            {
                for (int j = 0; j < Math.Min(monteur.Carte.Hauteur, cp_carte.Hauteur); j++)
                {
                    monteur.Carte.Cases[i][j] = cp_carte.Cases[i][j];
                }
            }
            canvasMap.Width = monteur.Carte.Largeur * 50;
            canvasMap.Height = monteur.Carte.Hauteur * 50;
            afficheCarte();

            e.Handled = true;
        }

        public void ComboBox_SelectionChanged_Tours(object sender, SelectionChangedEventArgs e)
        {
            monteur.Carte.NbToursMax = (int)comboTours.SelectedItem;
            e.Handled = true;
        }

        public void ComboBox_SelectionChanged_UC(object sender, SelectionChangedEventArgs e)
        {
            monteur.Carte.NbUniteClassique = (int)comboUC.SelectedItem;
            e.Handled = true;
        }

        public void ComboBox_SelectionChanged_UE(object sender, SelectionChangedEventArgs e)
        {
            monteur.Carte.NbUniteElite = (int)comboUE.SelectedItem;
            e.Handled = true;
        }

        public void ComboBox_SelectionChanged_UB(object sender, SelectionChangedEventArgs e)
        {
            monteur.Carte.NbUniteBlindee = (int)comboUB.SelectedItem;
            e.Handled = true;
        }

        private void RadioButton_EnabledChanged(object sender, RoutedEventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            terrain = radio.Name[5] - '0';
            e.Handled = true;
        }

        private void MenuItem_Click_Ouvrir(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            openFileDialog.FileName = null;
            openFileDialog.Filter = "Carte SmallWorld (.card)|*.card";

            Nullable<bool> res = openFileDialog.ShowDialog();

            if (res == true)
            {
                try
                {
                    disableComboBox_SelectionChanged();

                    monteur = new MonteurFichier(openFileDialog.FileName);
                    monteur.creerCarte();

                    initCombo();

                    canvasMap.Width = monteur.Carte.Largeur * 50;
                    canvasMap.Height = monteur.Carte.Hauteur * 50;
                    afficheCarte();

                    enableComboBox_SelectionChanged();
                }
                catch (Exception)
                {
                    MessageBox.Show("Un erreur s'est produite pendant l'ouverture de la carte.");
                }
            }
        }

        private void MenuItem_Click_Enregistrer(object sender, RoutedEventArgs e)
        {
            if (monteur.Carte != null)
            {
                if (monteur.Carte.NbUniteBlindee + monteur.Carte.NbUniteElite + monteur.Carte.NbUniteClassique >= UNITES_MIN)
                {
                    SaveFileDialog dlg = new SaveFileDialog();
                    dlg.FileName = "carteSmallWorld.card"; // Default file name
                    dlg.DefaultExt = ".card"; // Default file extension
                    dlg.Filter = "Carte SmallWorld (.card)|*.card"; // Filter files by extension

                    // Show save file dialog box
                    Nullable<bool> result = dlg.ShowDialog();

                    // Process save file dialog box results
                    if (result == true)
                    {
                        // Save document
                        string filename = dlg.FileName;
                        XmlSerializer mySerializer = new XmlSerializer(monteur.Carte.GetType());
                        StreamWriter myWriter = new StreamWriter(filename);
                        mySerializer.Serialize(myWriter, monteur.Carte);
                        myWriter.Close();
                    }
                }
                else
                MessageBox.Show("Le nombre minimal d'unités est de 5 (toutes unités confondues).");
            }
        }

        private void MenuItem_Click_Quitter(object sender, RoutedEventArgs e)
        {
            this.Close();
        }  

        private void default_Click(object sender, RoutedEventArgs e)
        {
            tileFactory = new ImageFactory();

            if (monteur.Carte != null)
                afficheCarte();
            e.Handled = true;
        }

        private void groovy_Click(object sender, RoutedEventArgs e)
        {
            tileFactory = new ImageFactory("groovy");

            if (monteur.Carte != null)
                afficheCarte();
            e.Handled = true;
        }

        private void tropical_Click(object sender, RoutedEventArgs e)
        {
            tileFactory = new ImageFactory("tropical");

            if (monteur.Carte != null)
                afficheCarte();
            e.Handled = true;
        }

        private void noStyle_Click(object sender, RoutedEventArgs e)
        {
            tileFactory = new RectangleFactory();

            if (monteur.Carte != null)
                afficheCarte();
            e.Handled = true;
        }

        private void campaign_Click(object sender, RoutedEventArgs e)
        {
            tileFactory = new ImageFactory("campaign");

            if (monteur.Carte != null)
                afficheCarte();
            e.Handled = true;
        }
    }
}
