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
using System.Xml.Serialization;
using System.IO;

namespace InterfaceGraphique
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Partie partie;
        TileFactory tileStrateg;
        List<Unite> listUnitSelected;
        int[][] allowedMouv;


        public MainWindow()
        {
            InitializeComponent();
            //rectOnOver = null;
            allowedMouv = null;
            listUnitSelected = new List<Unite>();
            tileStrateg = new ImageFactory();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public void loadPartie(MonteurCarte monteurC, List<IJoueur> joueurs)
        {
            
            Task.Factory.StartNew(() =>
            {
                MonteurPartie1v1 monteurPartie = new MonteurPartie1v1();
                monteurPartie.creerPartie(monteurC, joueurs);
                partie = monteurPartie.Partie;

                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    canvasMap.Children.Clear();
                    canvasMap.Children.Add(selectionRectangle);
                    canvasMap.Width = partie.Carte.Largeur * 50;
                    canvasMap.Height = partie.Carte.Hauteur * 50;

                    loadGrid();
                    loadControlGauche();
                    loadControlDroit();
                }));
            });
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
            
            if (selectionRectangle.Visibility==Visibility.Visible)
            {
                rect.Fill = tileStrateg.getViewTile(selectionRectangle.Tag as ICase);
                column = (int)Canvas.GetLeft(selectionRectangle) / 50;
                row = (int)Canvas.GetTop(selectionRectangle) / 50;
            }
            else
            {
                rect.Fill = Brushes.Black;
            }

            StackPanel panelInfo = new StackPanel();
            panelInfo.Margin = new Thickness(4);
            TextBlock typeCase = new TextBlock();
            typeCase.Text = "Type: ";

            if (selectionRectangle.Visibility == Visibility.Visible)
                typeCase.Text += "Type: " + (selectionRectangle.Tag as ICase).ToString();
            
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

            if (partie.Carte.Unites[column][row] != null )//&& rectSelected != null)
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
            canvasMap.Children.Clear();
            canvasMap.Children.Add(selectionRectangle);
            for (int l = 0; l < partie.Carte.Hauteur; l++)
            {
                for (int c = 0; c < partie.Carte.Largeur; c++)
                {
                    var tile = partie.Carte.Cases[c][l];
                    var unite =  partie.Carte.Unites[c][l];
                    var rect = createRectangle(c, l, tile, unite);
                    canvasMap.Children.Add(rect);
                }
            }
        }

        private void loadSuggestion()
        {
            if (allowedMouv != null)
            {
                for (int l = 0; l < partie.Carte.Hauteur; l++)
                {
                    for (int c = 0; c < partie.Carte.Largeur; c++)
                    {
                        if (allowedMouv[c][l] >= 1)
                        {
                            var rect = createSuggestion(c, l);
                            rect.StrokeThickness = allowedMouv[c][l] + 1;
                            canvasMap.Children.Add(rect);
                        }
                    }
                }
            }
        }

        private Tile createRectangle(int c, int l, ICase tile, List<Unite> listUnite)
        {
            var rectangle = new Tile(tile, tileStrateg, listUnite);

            // mise à jour des attributs (column et Row) référencant la position dans la grille à rectangle
            Canvas.SetLeft(rectangle, c * 50);
            Canvas.SetTop(rectangle, l * 50);
            Canvas.SetZIndex(rectangle,5);

            // enregistrement d'un écouteur d'evt sur le rectangle : 
            // source = rectangle / evt = MouseLeftButtonDown / délégué = rectangle_MouseLeftButtonDown
            rectangle.MouseLeftButtonDown += new MouseButtonEventHandler(Rectangle_MouseDown);
            return rectangle;
        }

        private Rectangle createSuggestion(int c, int l)
        {
            var rectangle = new Rectangle();
            Canvas.SetLeft(rectangle, c * 50);
            Canvas.SetTop(rectangle, l * 50);
            Canvas.SetZIndex(rectangle, 10);

            rectangle.Stroke = Brushes.Red;
            rectangle.StrokeThickness = 2;

            rectangle.Width = 50;
            rectangle.Height = 50;

            //rectangle.MouseLeftButtonDown += new MouseButtonEventHandler(Rectangle_MouseDown);

            return rectangle;
        }
        
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var rect = sender as Tile;
            var tile = rect.TileType;
            int column = (int)Canvas.GetLeft(rect) / 50;
            int row = (int)Canvas.GetTop(rect) / 50;
            Canvas.SetLeft(selectionRectangle, Canvas.GetLeft(rect));
            Canvas.SetTop(selectionRectangle, Canvas.GetTop(rect));
            Canvas.SetZIndex(selectionRectangle, 999);
            selectionRectangle.Visibility = System.Windows.Visibility.Visible;
            selectionRectangle.Tag = tile;


            if (listUnitSelected.Count > 0 && allowedMouv != null && allowedMouv[column][row] > 0)
            {
                partie.Carte.deplaceUnites(listUnitSelected, column, row);
                listUnitSelected.Clear();
            }
            else
            {
                allowedMouv = null;
                loadSuggestion();
            }


            loadGrid();
            loadControlDroit();
        }


        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            NouvellePartie fen = new NouvellePartie();
            fen.Owner = this;
            fen.ShowDialog();
            e.Handled = true;
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
            if (partie != null && partie.Carte != null)
                loadGrid();
            e.Handled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            partie.tourSuivant();
            allowedMouv = null;
            loadGrid();
            loadSuggestion();
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
                    int column = (int)Canvas.GetLeft(selectionRectangle) / 50;
                    int row = (int)Canvas.GetTop(selectionRectangle) / 50;
                    listUnitSelected.Add(grp.Unit);

                    Task.Factory.StartNew(() =>
                    {
                        allowedMouv = partie.Carte.suggestion(grp.Unit, column, row);

                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            loadGrid();
                            loadSuggestion();
                        }));
                    });
                }
            }
            else
            {
                listUnitSelected.Remove(grp.Unit);
                if (listUnitSelected.Count == 0)
                {
                    loadGrid();
                    allowedMouv = null;
                    loadSuggestion();
                }
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (partie != null)
            {
                XmlSerializer mySerializer = new XmlSerializer(partie.GetType());
                StreamWriter myWriter = new StreamWriter("myFileName.xml");
                mySerializer.Serialize(myWriter, partie);
                myWriter.Close();
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            XmlSerializer mySerializer = new XmlSerializer(typeof(Partie1v1));
            FileStream myFileStream = new FileStream("myFileName.xml", FileMode.Open);
            partie = (Partie)mySerializer.Deserialize(myFileStream);
            partie.associeJoueursUnite();
            myFileStream.Close();

            canvasMap.Children.Clear();
            canvasMap.Children.Add(selectionRectangle);
            canvasMap.Width = partie.Carte.Largeur * 50;
            canvasMap.Height = partie.Carte.Hauteur * 50;
            loadControlDroit();
            loadControlGauche();
            loadGrid();
        }

    }
}
