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
using System.ComponentModel;
using System.Threading;

namespace InterfaceGraphique
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Partie partie;
        TileFactory tileFactory;
        List<Unite> listUnitSelected;
        SuggMap allowedMouv;
        private Semaphore _pool;
        private Semaphore _poolInit;


        public MainWindow()
        {
            InitializeComponent();
            allowedMouv = new SuggMap();
            listUnitSelected = new List<Unite>();
            tileFactory = new ImageFactory();
            _pool = new Semaphore(0, 1);
            _poolInit = new Semaphore(0, 1);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }


        private void updateUI()
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                allowedMouv = null;
                listUnitSelected.Clear();
                loadGrid();
                loadSuggestion();
                loadControlDroit();
                refreshControlGauche();
                _pool.Release();
            }));
        }

        private void startGame()
        {
            Task.Factory.StartNew(() =>
            {
                _poolInit.WaitOne();
                while (!partie.Finpartie)
                {
                    partie.joueurActuel().jouerTour(partie);
                    partie.tourSuivant();
                    updateUI();
                    System.Threading.Thread.Sleep(100);
                    _pool.WaitOne();
                }
                endGame();
            });
        }

        private void endGame()
        {
            MessageBox.Show(partie.getGagnant().Nom+" gagne la partie !");
        }

        private void initUI()
        {
            Console.WriteLine("initUI ");
            canvasMap.Children.Clear();
            canvasMap.Children.Add(selectionRectangle);
            canvasMap.Width = partie.Carte.Largeur * 50;
            canvasMap.Height = partie.Carte.Hauteur * 50;

            loadGrid();
            loadControlGauche();
            loadControlDroit();
            _poolInit.Release();
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
                    initUI();
                }));

                startGame();
            });
        }

        
        private void refreshControlGauche()
        {
            labelJoueurActuel.Content = "C'est à " + partie.joueurActuel().Nom + " de jouer !";
            labelNbTour.Content = "Tour: " + partie.NbTours + "/" + partie.Carte.NbToursMax;


            boutonFinir.IsEnabled = false;
            if (partie.joueurActuel() is JoueurConcret)
            {
                boutonFinir.IsEnabled = true;
            }

            foreach (GroupeJoueur grp in panelListeJoueur.Children)
            {
                grp.select(partie.joueurActuel());
                int nbUnit = partie.Carte.getNombreUniteRestante(grp.Joueur);
                grp.setNbUnite(nbUnit);
                if (nbUnit == 0)
                {
                    grp.IsEnabled = false;
                }
            }
                
        }

        private void loadControlGauche()
        {
            panelListeJoueur.Children.Clear();
            foreach (Joueur j in partie.ListJoueurs)
            {
                GroupeJoueur grp = new GroupeJoueur(j);
                int nbUniteRestante = partie.Carte.getNombreUniteRestante(j);
                if (nbUniteRestante == 0)
                    grp.IsEnabled = false;
                panelListeJoueur.Children.Add(grp);
            }

            refreshControlGauche();
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
                rect.Fill = tileFactory.getViewTile(selectionRectangle.Tag as ICase);
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


            List<Unite> tmp = partie.Carte.getUniteFromCoord(new Coordonnees(column, row));
            TextBlock nbUnite = new TextBlock();
            nbUnite.Text = "Nb Unités: ";
            nbUnite.Text += tmp.Count();

            panelGrp.Children.Add(rect);
            panelInfo.Children.Add(typeCase);
            panelInfo.Children.Add(nbUnite);

            panelGrp.Children.Add(panelInfo);
            grpInfo.Content = panelGrp;
            controlDroit.Children.Add(grpInfo);


            List<Unite> tmpLit = partie.Carte.getUniteFromCoordAndJoueur(new Coordonnees(column, row), partie.joueurActuel());

            if (tmpLit.Count>0 && selectionRectangle.Visibility == Visibility.Visible)
            {
                ScrollViewer scrollInfoUnite = new ScrollViewer();
                scrollInfoUnite.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                scrollInfoUnite.Height = 400;
                StackPanel panelScroll = new StackPanel();
                foreach (Unite u in tmpLit)
                {
                    GroupeUnite grp = new GroupeUnite(u);
                    grp.MouseDown += grpUnit_MouseDown;
                    panelScroll.Children.Add(grp);
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
                    var unite = partie.Carte.getUniteFromCoord(new Coordonnees(c, l));
                    var rect = createRectangle(c, l, tile, unite);
                    canvasMap.Children.Add(rect);
                }
            }
        }

        private void loadSuggestion()
        {
            if (allowedMouv != null)
            {
                foreach (var pair in allowedMouv)
                {
                    if (pair.Value.Sugg >= 1)
                    {
                        var rect = createSuggestion(pair.Key.X, pair.Key.Y);
                        rect.StrokeThickness = pair.Value.Sugg + 1;
                        canvasMap.Children.Add(rect);
                    }
                }
            }
        }

        private Tile createRectangle(int c, int l, ICase tile, List<Unite> listUnite)
        {
            var rectangle = new Tile(tile, tileFactory, listUnite);

            Canvas.SetLeft(rectangle, c * 50);
            Canvas.SetTop(rectangle, l * 50);
            Canvas.SetZIndex(rectangle,5);

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


            if (listUnitSelected.Count > 0 && allowedMouv.Count > 0)
            {
                partie.Carte.deplaceUnites(listUnitSelected, new Coordonnees(column, row), allowedMouv);
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
                tileFactory = new ImageFactory();
            }
            else
            {
                tileFactory = new RectangleFactory();
            }
            if (partie != null && partie.Carte != null)
                loadGrid();
            e.Handled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            partie.joueurActuel().finirTour();
            (sender as Button).IsEnabled = false;
            e.Handled = true;
        }

        private void grpUnit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var grp = (GroupeUnite)sender;
            if (grp.Selected) // si on selectionne l'unité
            {
                if (grp.Unit.PointsDepl > 0) // et qu'elle lui reste des points de déplacements
                {
                    listUnitSelected.Add(grp.Unit); // on l'ajout à la liste des unités séléctionnées
                    loadDataSuggestion(); // et on réactualise l'affichage des suggestions de déplacements
                }
            }
            else // sinon on la désélectionne
            {
                listUnitSelected.Remove(grp.Unit); // on la retire de la liste
                if (listUnitSelected.Count == 0) // si plus aucune unité n'est sélectionné
                {
                    loadGrid();
                    allowedMouv = null; // on supprime l'affichage  des suggestions
                    loadSuggestion();
                }
                else
                {
                    loadDataSuggestion(); //sinon on réactualise l'affichage des suggestions avec les unités restantes
                }
            }
        }

        private void loadDataSuggestion()
        {
            int column = (int)Canvas.GetLeft(selectionRectangle) / 50;
            int row = (int)Canvas.GetTop(selectionRectangle) / 50;
            Task.Factory.StartNew(() =>
            {
                Unite unit = null;
                foreach (Unite u in listUnitSelected) // on cherche l'unité avec le moins de points de deplacements
                {
                    if (unit == null)
                        unit = u;
                    else
                    {
                        if (unit.PointsDepl > u.PointsDepl)
                            unit = u;
                    }
                }
                allowedMouv = partie.joueurActuel().StrategySuggestion.getSuggestion(partie.Carte, unit); // on charge les suggestions pour cette unités ( et qui servira pour toutes les unités select)

                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    loadGrid();
                    loadSuggestion();
                }));
            });
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

            initUI();
            startGame();
        }
    }
}
