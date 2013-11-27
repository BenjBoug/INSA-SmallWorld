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

namespace InterfaceGraphique
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Partie partie;
        Rectangle rectOnOver;
        Rectangle rectSelected;
        public MainWindow()
        {
            InitializeComponent();
            MonteurPartie1v1 monteurPartie = new MonteurPartie1v1();
            monteurPartie.creerPartie(new MonteurPetite(), new FabriquePeupleGaulois());
            partie = monteurPartie.Partie;
            rectOnOver = null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            for (int c = 0; c < partie.Carte.Largeur; c++)
            {
                gridMap.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(50, GridUnitType.Pixel) });
            }


            for (int l = 0; l < partie.Carte.Hauteur; l++)
            {
                gridMap.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50, GridUnitType.Pixel) });
                for (int c = 0; c < partie.Carte.Largeur; c++)
                {
                    var tile = partie.Carte.Cases[c][l];
                    var rect = createRectangle(c, l, tile);
                    gridMap.Children.Add(rect);
                }
            }

            gridMap.Width = partie.Carte.Largeur * 50;
            gridMap.Height = partie.Carte.Hauteur * 50;

            updateUnitUI();
        }

        /// <summary>
        /// Récupération de la position de l'unité (logique), mise à jour de l'ellipse (physique) matérialisant l'unité
        /// </summary>
        private void updateUnitUI()
        {

        }
            
        private Rectangle createRectangle(int c, int l, ICase tile)
        {
            var rectangle = new Rectangle();
            if (tile is CaseDesert)
                rectangle.Fill = Brushes.Brown;
            if (tile is CaseEau)
                rectangle.Fill = Brushes.Blue;
            if (tile is CaseForet)
                rectangle.Fill = Brushes.DarkGreen;
            if (tile is CaseMontagne)
                rectangle.Fill = Brushes.Gold;
            if (tile is CasePlaine)
                rectangle.Fill = Brushes.LightGreen;
            // mise à jour des attributs (column et Row) référencant la position dans la grille à rectangle
            Grid.SetColumn(rectangle, c);
            Grid.SetRow(rectangle, l);
            rectangle.Tag = tile; // Tag : ref par defaut sur la tuile logique


            rectangle.Stroke = Brushes.Black;
            rectangle.StrokeThickness = 1;
            // enregistrement d'un écouteur d'evt sur le rectangle : 
            // source = rectangle / evt = MouseLeftButtonDown / délégué = rectangle_MouseLeftButtonDown
            //rectangle.MouseLeftButtonDown += new MouseButtonEventHandler(rectangle_MouseLeftButtonDown);

            rectangle.MouseEnter += new MouseEventHandler(Rectangle_MouseEnter);
            rectangle.MouseLeave += new MouseEventHandler(Rectangle_MouseLeave);
            rectangle.MouseDown += new MouseButtonEventHandler(Rectangle_MouseDown);
            return rectangle;
        }

        private void Rectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            var rect = sender as Rectangle;
            var tile = rect.Tag as ICase;
            int column = Grid.GetColumn(rect);
            int row = Grid.GetRow(rect);

            if (rectOnOver != null) rectOnOver.StrokeThickness = 1;
            rectOnOver = rect;
            rectOnOver.Tag = tile;
            rect.StrokeThickness = 3;
            rect.Stroke = Brushes.Yellow;

            e.Handled = true;

        }

        private void Rectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            if (rectSelected != (sender as Rectangle))
            {
                if (rectOnOver != null)
                {
                    rectOnOver.StrokeThickness = 1;
                    rectOnOver.Stroke = Brushes.Black;
                }
                rectOnOver = null;
            }
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var rect = sender as Rectangle;
            var tile = rect.Tag as ICase;
            int column = Grid.GetColumn(rect);
            int row = Grid.GetRow(rect);

            if (rectSelected != null) rectSelected.StrokeThickness = 1;
            rectSelected = rect;
            rectSelected.Tag = tile;
            rect.StrokeThickness = 3;
            rect.Stroke = Brushes.Blue;
        }

        private void ScrollViewer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (rectSelected != null)
            {
                rectSelected.StrokeThickness = 1;
                rectSelected.Stroke = Brushes.Black;
            }
            rectSelected = null;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            NouvellePartie fen = new NouvellePartie();
            
            fen.ShowDialog();
        }

    }
}
