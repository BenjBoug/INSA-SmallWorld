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
        Tile rectSelected;
        TileFactory tileStrateg;
        List<IUnite> listUnitSelected;
        int[][] allowedMouv;


        public MainWindow()
        {
            InitializeComponent();
            //rectOnOver = null;
            allowedMouv = null;
            rectSelected = null;
            listUnitSelected = new List<IUnite>();
            tileStrateg = new ImageFactory();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public void loadPartie(MonteurCarte monteurC, List<IJoueur> joueurs)
        {
            //rectOnOver = null;
            rectSelected = null;
            
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

                    gridMap.Width = partie.Carte.Largeur * 50;
                    gridMap.Height = partie.Carte.Hauteur * 50;

                    loadGrid();
                    loadControlGauche();
                    loadControlDroit();
                }));
            });
            updateUnitUI();
        }

        private void updateUnitUI()
        {
        }

        private void loadControlGauche()
        {

            controlGauche.Children.Clear();

            Label joueurActuel = new Label();
            joueurActuel.Content = "C'est à " + partie.joueurActuel().Nom + " de jouer !";
            controlGauche.Children.Add(joueurActuel);

            Label nbTours = new Label();
            nbTours.Content = "Tour: "+ partie.NbTours +"/"+partie.Carte.NbToursMax;
            controlGauche.Children.Add(nbTours);

            foreach (JoueurConcret j in partie.ListJoueurs)
            {
                controlGauche.Children.Add(new GroupeJoueur(j, j == partie.joueurActuel()));
            }

            Button boutonNext = new Button();
            boutonNext.Content = "Tour fini !";
            boutonNext.Click += Button_Click;
            controlGauche.Children.Add(boutonNext);
        }

        private void loadControlDroit()
        {
            controlDroit.Children.Clear();

            GroupBox grpInfo = new GroupBox();
            grpInfo.Margin = new Thickness(5);
            grpInfo.Header = "Infos case";
            StackPanel panelGrp = new StackPanel();
            panelGrp.Orientation = Orientation.Horizontal;
            Rectangle rect = new Rectangle();
            rect.Width = 50;
            rect.Height = 50;
            int column = 0, row = 0;
            if (rectSelected!=null)
            {
                rect.Fill = tileStrateg.getViewTile(rectSelected.TileType);
                column = Grid.GetColumn(rectSelected);
                row = Grid.GetRow(rectSelected);
            }
            else
            {
                rect.Fill = Brushes.Black;
            }

            StackPanel panelInfo = new StackPanel();
            panelInfo.Margin = new Thickness(4);
            TextBlock typeCase = new TextBlock();
            typeCase.Text = "Type: ";
            if (rectSelected != null)
                typeCase.Text += "Type: "+(rectSelected.TileType).ToString();

            TextBlock nbUnite = new TextBlock();
            nbUnite.Text = "Nb Unités: ";
            if (partie.Carte.Unites[column][row] != null)
                nbUnite.Text += partie.Carte.Unites[column][row].Count();
            else
                nbUnite.Text += "0";

            panelGrp.Children.Add(rect);
            panelInfo.Children.Add(typeCase);
            panelInfo.Children.Add(nbUnite);

            panelGrp.Children.Add(panelInfo);
            grpInfo.Content = panelGrp;
            controlDroit.Children.Add(grpInfo);

            if (partie.Carte.Unites[column][row] != null && rectSelected != null)
            {
                ScrollViewer scrollInfoUnite = new ScrollViewer();
                scrollInfoUnite.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                scrollInfoUnite.Height = 400;
                StackPanel panelScroll = new StackPanel();
                foreach (Unite u in partie.Carte.Unites[column][row])
                {
                    if (u.Proprietaire == partie.joueurActuel())
                    {
                        GroupeUnite grp = new GroupeUnite(u);
                        grp.MouseDown += grpUnit_MouseDown;
                        panelScroll.Children.Add(grp);
                    }
                }
                scrollInfoUnite.Content = panelScroll;
                controlDroit.Children.Add(scrollInfoUnite);
            }
        }


        private void loadGrid()
        {
            bool allowRect = false;
            for (int l = 0; l < partie.Carte.Hauteur; l++)
            {
                for (int c = 0; c < partie.Carte.Largeur; c++)
                {
                    var tile = partie.Carte.Cases[c][l];
                    if (allowedMouv == null)
                        allowRect = false;
                    else
                        allowRect = allowedMouv[c][l]==1;
                    var rect = createRectangle(c, l, tile, partie.Carte.Unites[c][l], allowRect);
                    gridMap.Children.Add(rect);
                }
            }
        }

        private Tile createRectangle(int c, int l, ICase tile, List<IUnite> listUnite, bool allowed)
        {
            var rectangle = new Tile(tile, tileStrateg, listUnite, allowed);

            // mise à jour des attributs (column et Row) référencant la position dans la grille à rectangle
            Grid.SetColumn(rectangle, c);
            Grid.SetRow(rectangle, l);

            // enregistrement d'un écouteur d'evt sur le rectangle : 
            // source = rectangle / evt = MouseLeftButtonDown / délégué = rectangle_MouseLeftButtonDown
            //rectangle.MouseLeftButtonDown += new MouseButtonEventHandler(rectangle_MouseLeftButtonDown);
            
            //rectangle.MouseEnter += new MouseEventHandler(Rectangle_MouseEnter);
            //rectangle.MouseLeave += new MouseEventHandler(Rectangle_MouseLeave);
            rectangle.MouseDown += new MouseButtonEventHandler(Rectangle_MouseDown);
            return rectangle;
        }
        /*
        private void Rectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            if (rectSelected != (sender as Rectangle))
            {
                var rect = sender as Rectangle;
                var tile = rect.Tag as ICase;
                int column = Grid.GetColumn(rect);
                int row = Grid.GetRow(rect);
                if (rectSelected != rectOnOver)
                {
                    if (rectOnOver != null) rectOnOver.StrokeThickness = 0;
                }
                rectOnOver = rect;
                rectOnOver.Tag = tile;
                rect.StrokeThickness = 3;
                rect.Stroke = Brushes.Yellow;
            }
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
            e.Handled = true;
        }*/

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var rect = sender as Tile;
            var tile = rect.TileType;
            int column = Grid.GetColumn(rect);
            int row = Grid.GetRow(rect);
            
            if (rectSelected != null) rectSelected.rect.StrokeThickness = 0;
            rectSelected = rect;
            rect.rect.StrokeThickness = 3;
            rect.rect.Stroke = Brushes.Blue;
            if (listUnitSelected.Count > 0 && allowedMouv != null && allowedMouv[column][row]==1)
            {
                foreach (IUnite u in listUnitSelected)
                {
                    //if (partie.Carte.estAdjacente(column,row))
                    {
                        partie.Carte.deplaceUnite(u, column, row);
                        allowedMouv = null;
                    }
                }
                         allowedMouv = null;
                listUnitSelected.Clear();
                loadGrid();
            }
            loadControlDroit();
            e.Handled = true;
        }


        private void ScrollViewer_MouseDown(object sender, MouseButtonEventArgs e)
        {/*
            if (rectSelected != null) rectSelected.StrokeThickness = 0;
            rectSelected = null;
            loadControlDroit();
            e.Handled = true;*/
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
                tileStrateg = new ImageFactory();
            }
            else
            {
                tileStrateg = new RectangleFactory();
            }
            gridMap.Children.Clear();
            if (partie != null && partie.Carte != null)
                loadGrid();
            e.Handled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            partie.tourSuivant();
            loadControlGauche();
            loadControlDroit();
            e.Handled = true;
        }

        private void grpUnit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var grp = (GroupeUnite)sender;
            if (grp.Selected)
            {
                if (grp.Unit.PointsDepl > 0)
                {
                    int column = Grid.GetColumn(rectSelected);
                    int row = Grid.GetRow(rectSelected);
                    listUnitSelected.Add(grp.Unit);
                    //afficher proposition
                    //int[] coord = partie.Carte.getCoord(grp.Unit);
                    allowedMouv = partie.Carte.suggestion(grp.Unit, column, row);
                    loadGrid();
                }
            }
            else
                listUnitSelected.Remove(grp.Unit);
        }
    }
}
