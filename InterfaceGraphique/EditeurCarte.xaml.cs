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
        private Carte carte;
        private int terrain;

        public EditeurCarte()
        { 
            InitializeComponent();
            tileFactory = new ImageFactory();
            terrain = 0;
            initCombo();
        }

        private void initCombo()
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

        private void modifieCarte()
        {
            Carte cp_carte = new CarteClassique();
            if (carte.Largeur > 0 && carte.Hauteur > 0)
            {
                cp_carte.Largeur = carte.Largeur;
                cp_carte.Hauteur = carte.Hauteur;
                cp_carte.Cases = carte.Cases;
            }
            carte.Largeur = (int)comboLargeur.SelectedItem;
            carte.Hauteur = (int)comboHauteur.SelectedItem;
            carte.Cases = new Case[carte.Largeur][];
            for (int i = 0; i < carte.Largeur; i++)
                carte.Cases[i] = new Case[carte.Hauteur];
            for (int i = 0; i < carte.Largeur; i++)
            {
                for (int j = 0; j < carte.Hauteur; j++)
                {
                    carte.setCase(i, j, carte.FabriqueCase.getCase(0));
                }
            }
            for (int i = 0; i < Math.Min(carte.Largeur,cp_carte.Largeur); i++)
            {
                for (int j = 0; j < Math.Min(carte.Hauteur, cp_carte.Hauteur); j++)
                {
                    carte.Cases[i][j] = cp_carte.Cases[i][j];
                }
            }
            canvasMap.Width = carte.Largeur * 50;
            canvasMap.Height = carte.Hauteur * 50;
            afficheCarte();
        }

        private void afficheCarte()
        {
            canvasMap.Children.Clear();
            for (int l = 0; l < carte.Hauteur; l++)
            {
                for (int c = 0; c < carte.Largeur; c++)
                {
                    var tile = carte.Cases[c][l];
                    var unite = carte.getUniteFromCoord(new Coordonnees(c, l));
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
            carte.setCase(column, row, carte.FabriqueCase.getCase(terrain));
            afficheCarte();
            e.Handled = true;
        }

        private void MenuItem_Click_Nouvelle(object sender, RoutedEventArgs e)
        {
            disableComboBox_SelectionChanged();

            carte = new CarteClassique();
            carte.Unites = new List<Unite>();

            comboLargeur.SelectedIndex = 0;
            comboHauteur.SelectedIndex = 0;
            comboTours.SelectedIndex = 0;
            comboUC.SelectedIndex = 0;
            comboUE.SelectedIndex = 0;
            comboUB.SelectedIndex = 0;

            modifieCarte();            
            carte.NbUniteClassique = (int)comboUC.SelectedItem;
            carte.NbUniteElite = (int)comboUE.SelectedItem;
            carte.NbUniteBlindee = (int)comboUB.SelectedItem;
            
            enableComboBox_SelectionChanged();     
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
            modifieCarte();
            e.Handled = true;
        }

        public void ComboBox_SelectionChanged_Tours(object sender, SelectionChangedEventArgs e)
        {
            carte.NbToursMax = (int)comboTours.SelectedItem;
            e.Handled = true;
        }

        public void ComboBox_SelectionChanged_UC(object sender, SelectionChangedEventArgs e)
        {
            carte.NbUniteClassique = (int)comboUC.SelectedItem;
            e.Handled = true;
        }

        public void ComboBox_SelectionChanged_UE(object sender, SelectionChangedEventArgs e)
        {
            carte.NbUniteElite = (int)comboUE.SelectedItem;
            e.Handled = true;
        }

        public void ComboBox_SelectionChanged_UB(object sender, SelectionChangedEventArgs e)
        {
            carte.NbUniteBlindee = (int)comboUB.SelectedItem;
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
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            openFileDialog1.FileName = null;
            openFileDialog1.Filter = "Carte SmallWorld (.card)|*.card";

            string openFileName;

            Nullable<bool> res = openFileDialog1.ShowDialog();

            if (res == true)
            {
                openFileName = openFileDialog1.FileName;
                try
                {
                    disableComboBox_SelectionChanged();

                    XmlSerializer mySerializer = new XmlSerializer(typeof(CarteClassique));
                    carte = (Carte)mySerializer.Deserialize(openFileDialog1.OpenFile()); 

                    comboLargeur.SelectedIndex = carte.Largeur - TAILLE_MIN;
                    comboHauteur.SelectedIndex = carte.Hauteur - TAILLE_MIN;
                    comboTours.SelectedIndex = carte.NbToursMax - TOURS_MIN;
                    comboUC.SelectedIndex = carte.NbUniteClassique;
                    comboUE.SelectedIndex = carte.NbUniteElite;
                    comboUB.SelectedIndex = carte.NbUniteBlindee;

                    canvasMap.Width = carte.Largeur * 50;
                    canvasMap.Height = carte.Hauteur * 50;
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
            if (carte != null)
            {
                if (carte.NbUniteBlindee + carte.NbUniteElite + carte.NbUniteClassique >= UNITES_MIN)
                {
                    SaveFileDialog dlg = new SaveFileDialog();
                    dlg.FileName = "carteSmallWorld"; // Default file name
                    dlg.DefaultExt = ".card"; // Default file extension
                    dlg.Filter = "Carte SmallWorld (.card)|*.card"; // Filter files by extension

                    // Show save file dialog box
                    Nullable<bool> result = dlg.ShowDialog();

                    // Process save file dialog box results
                    if (result == true)
                    {
                        // Save document
                        string filename = dlg.FileName;
                        XmlSerializer mySerializer = new XmlSerializer(carte.GetType());
                        StreamWriter myWriter = new StreamWriter(filename);
                        mySerializer.Serialize(myWriter, carte);
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

            if (carte != null)
                afficheCarte();
            e.Handled = true;
        }

        private void groovy_Click(object sender, RoutedEventArgs e)
        {
            tileFactory = new ImageFactory("groovy");

            if (carte != null)
                afficheCarte();
            e.Handled = true;
        }

        private void tropical_Click(object sender, RoutedEventArgs e)
        {
            tileFactory = new ImageFactory("tropical");

            if (carte != null)
                afficheCarte();
            e.Handled = true;
        }

        private void noStyle_Click(object sender, RoutedEventArgs e)
        {
            tileFactory = new RectangleFactory();

            if (carte != null)
                afficheCarte();
            e.Handled = true;
        }

        private void campaign_Click(object sender, RoutedEventArgs e)
        {
            tileFactory = new ImageFactory("campaign");

            if (carte != null)
                afficheCarte();
            e.Handled = true;
        }
    }
}
