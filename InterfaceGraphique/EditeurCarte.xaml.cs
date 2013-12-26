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
        private int largeurCarte;
        private int hauteurCarte;
        private TileFactory tileFactory;
        private SelectedRect selectionRectangle;
        Carte carte;

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
            carte.Largeur = 10;
            carte.Hauteur = 10;
            carte.NbToursMax = 20;
            carte.NbUniteClassique = 5;
            carte.NbUniteElite = 3;
            carte.NbUniteBlindee = 1;
            carte.Cases = new Case[carte.Largeur][];
            for (int i = 0; i < carte.Largeur; i++)
                carte.Cases[i] = new Case[carte.Hauteur];
            for (int i = 0; i < carte.Largeur; i++)
            {
                for (int j = 0; j < carte.Hauteur; j++)
                {
                    carte.setCase(i, j, carte.FabriqueCase.getCase((int)Case.CaseInt.Plaine));
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
