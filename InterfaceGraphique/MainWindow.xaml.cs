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
        TileStrategy tileStrateg;


        public MainWindow()
        {
            InitializeComponent();
            rectOnOver = null;
            tileStrateg = new ImageTile();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public void loadPartie(MonteurCarte monteurC, List<Joueur> joueurs)
        {
            /*
            Task.Factory.StartNew(() =>
            {
                MonteurPartie1v1 monteurPartie = new MonteurPartie1v1();
                monteurPartie.creerPartie(monteurC, joueurs);
                partie = monteurPartie.Partie;

                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    gridMap.Children.Clear();
                    gridMap.RowDefinitions.Clear();
                    gridMap.ColumnDefinitions.Clear();

                    for (int c = 0; c < partie.Carte.Largeur; c++)
                    {
                        gridMap.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(50, GridUnitType.Pixel) });
                    }


                    for (int l = 0; l < partie.Carte.Hauteur; l++)
                    {
                        gridMap.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50, GridUnitType.Pixel) });
                    }

                    loadGrid();
                    loadControl();

                    gridMap.Width = partie.Carte.Largeur * 50;
                    gridMap.Height = partie.Carte.Hauteur * 50;
                }));
            });*/
            MonteurPartie1v1 monteurPartie = new MonteurPartie1v1();
            monteurPartie.creerPartie(monteurC, joueurs);
            partie = monteurPartie.Partie;

            gridMap.Children.Clear();
            gridMap.RowDefinitions.Clear();
            gridMap.ColumnDefinitions.Clear();

            for (int c = 0; c < partie.Carte.Largeur; c++)
            {
                gridMap.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(50, GridUnitType.Pixel) });
            }


            for (int l = 0; l < partie.Carte.Hauteur; l++)
            {
                gridMap.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50, GridUnitType.Pixel) });
            }

            loadGrid();
            loadControl();

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

        private void loadControl()
        {

            controlGauche.Children.Clear();

            Label joueurActuel = new Label();
            joueurActuel.Content = "C'est à " + ((JoueurConcret)partie.joueurSuivant()).Nom + " de jouer !";
            controlGauche.Children.Add(joueurActuel);

            Label nbTours = new Label();
            nbTours.Content = "Tour: "+ partie.NbTours +"/"+partie.Carte.NbToursMax;
            controlGauche.Children.Add(nbTours);

            foreach (JoueurConcret j in partie.ListJoueurs)
            {
                GroupBox grpJoueur = new GroupBox();
                grpJoueur.Margin = new Thickness(5);
                grpJoueur.Header = j.Nom;
                StackPanel panelGrp = new StackPanel();
                Label nbPoint = new Label();
                nbPoint.Content = "Points: "+j.Points;
                panelGrp.Children.Add(nbPoint);
                grpJoueur.Content = panelGrp;
                controlGauche.Children.Add(grpJoueur);
            }
        }

        private void loadGrid()
        {
            for (int l = 0; l < partie.Carte.Hauteur; l++)
            {
                for (int c = 0; c < partie.Carte.Largeur; c++)
                {
                    var tile = partie.Carte.Cases[c][l];
                    var rect = createRectangle(c, l, tile, partie.Carte.Unites[c][l]);
                    gridMap.Children.Add(rect);
                }
            }
        }
            
        private Rectangle createRectangle(int c, int l, ICase tile, List<Unite> listUnite)
        {
            var rectangle = new Rectangle();

            VisualBrush myBrush = new VisualBrush();
            StackPanel aPanel = new StackPanel();

            aPanel.Background = tileStrateg.getViewTile(tile);

            // Create some text.
            TextBlock someText = new TextBlock();
            if (listUnite != null)
            {
                someText.Text = listUnite.Count.ToString();
                someText.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(listUnite[0].Proprietaire.Couleur);
            }
            else
                someText.Text = " ";
            FontSizeConverter fSizeConverter = new FontSizeConverter();
            someText.FontSize = (double)fSizeConverter.ConvertFromString("10pt");
            someText.Margin = new Thickness(10);
            aPanel.Children.Add(someText);
            
            myBrush.Visual = aPanel;

            rectangle.Fill = myBrush;

            // mise à jour des attributs (column et Row) référencant la position dans la grille à rectangle
            Grid.SetColumn(rectangle, c);
            Grid.SetRow(rectangle, l);
            rectangle.Tag = tile; // Tag : ref par defaut sur la tuile logique


            rectangle.Stroke = Brushes.Black;
            rectangle.StrokeThickness = 0;
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
                    rectOnOver.StrokeThickness = 0;
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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            NouvellePartie fen = new NouvellePartie();
            fen.Owner = this;
            fen.ShowDialog();

            e.Handled = true;
        }

        private void MenuItem_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void affTileCheckbox_Click(object sender, RoutedEventArgs e)
        {
            if (affTileCheckbox.IsChecked)
            {
                tileStrateg = new ImageTile();
            }
            else
            {
                tileStrateg = new RectangleTile();
            }
            gridMap.Children.Clear();
            if (partie != null && partie.Carte != null)
                loadGrid();
            e.Handled = true;
        }
    }
}
