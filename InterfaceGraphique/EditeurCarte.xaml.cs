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
        private SelectedRect selectionRectangle;
        private Carte carte;

        public EditeurCarte()
        {
            InitializeComponent();
            tileFactory = new ImageFactory();
            selectionRectangle = new SelectedRect();
            selectionRectangle.Visibility = System.Windows.Visibility.Collapsed;

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            carte = new CarteClassique();

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
            comboLargeur.SelectedIndex = 0;
            comboHauteur.SelectedIndex = 0;
            comboTours.SelectedIndex = 0;
            comboUC.SelectedIndex = 0;
            comboUE.SelectedIndex = 0;
            comboUB.SelectedIndex = 0;
            paramCarte();
            comboLargeur.SelectionChanged += ComboBox_SelectionChanged;
            comboHauteur.SelectionChanged += ComboBox_SelectionChanged;
            //comboTours.SelectionChanged += ComboBox_SelectionChanged_1;
            //comboUC.SelectionChanged += ComboBox_SelectionChanged_1;
            //comboUE.SelectionChanged += ComboBox_SelectionChanged_1;
            //comboUB.SelectionChanged += ComboBox_SelectionChanged_1;
        }

        private void paramCarte()
        {   
            carte.Largeur = (int)comboLargeur.SelectedItem;
            carte.Hauteur = (int)comboHauteur.SelectedItem;
            /*carte.NbToursMax = (int)comboTours.SelectedItem;
            carte.NbUniteClassique = (int)comboUC.SelectedItem;
            carte.NbUniteElite = (int)comboUE.SelectedItem;
            carte.NbUniteBlindee = (int)comboUB.SelectedItem;*/
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
            carte.Unites = new List<Unite>();

            canvasMap.Width = carte.Largeur * 50;
            canvasMap.Height = carte.Hauteur * 50;
            afficheCarte();
        }

        private void afficheCarte()
        {
            canvasMap.Children.Clear();
            canvasMap.Children.Add(selectionRectangle);
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
            var rectangle = new Tile(tile, tileFactory, listUnite);

            Canvas.SetLeft(rectangle, c * 50);
            Canvas.SetTop(rectangle, l * 50);
            Canvas.SetZIndex(rectangle, 5);

            //rectangle.MouseLeftButtonDown += new MouseButtonEventHandler(Rectangle_MouseDown);
            return rectangle;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            paramCarte();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        { }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        { }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        { }

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

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
