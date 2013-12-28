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
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int FPS = 500;
        const int TAILLE = 50;

        private Partie partie;
        private TileFactory tileFactory;
        private List<Unite> listUnitSelected;
        private SuggMap allowedMouv;
        private Suggestion sugg;
        private Semaphore _pool;
        private Semaphore _poolInit;
        private SelectedRect selectionRectangle;

        private StackPanel panelScroll;


        public MainWindow()
        {
            InitializeComponent();
            allowedMouv = new SuggMap();
            sugg = new Suggestion();
            listUnitSelected = new List<Unite>();
            tileFactory = new ImageFactory();
            _pool = new Semaphore(0, 1);
            _poolInit = new Semaphore(0, 1);
            selectionRectangle = new SelectedRect();
            selectionRectangle.Visibility = System.Windows.Visibility.Collapsed;
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (UIElement ch in controlGauche.Children)
            {
                ch.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Actualise l'interace graphique dans le Thread de la fenêtre
        /// </summary>
        private void rafraichirInterface()
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                allowedMouv = null;
                listUnitSelected.Clear();
                afficheCarte();
                afficherSuggestions();
                actualisePanneauDroit();
                actualisePanneauGauche();
                _pool.Release();
            }));
        }
        /// <summary>
        /// Créer le thread et la boucle principale du jeu, et le démarre
        /// </summary>
        private void commencerJeu()
        {
            Task.Factory.StartNew(() =>
            {
                _poolInit.WaitOne();
                while (!partie.Finpartie)
                {
                    partie.joueurActuel().jouerTour(partie);
                    partie.tourSuivant();
                    rafraichirInterface();
                    System.Threading.Thread.Sleep(FPS);
                    _pool.WaitOne();
                }
                finJeu();
            });
        }
        /// <summary>
        /// Affiche le gagnant
        /// </summary>
        private void finJeu()
        {
            MessageBox.Show(partie.getGagnant().Nom+" gagne la partie !");
        }
        /// <summary>
        /// Initialise l'interface graphique
        /// </summary>
        private void initialiseInterface()
        {
            Console.WriteLine("initUI ");
            canvasMap.Children.Clear();
            canvasMap.Children.Add(selectionRectangle);
            canvasMap.Width = partie.Carte.Largeur * 50;
            canvasMap.Height = partie.Carte.Hauteur * 50;

            afficheCarte();
            chargerPanneauGauche();
            chargerPanneauDroit();

            foreach (UIElement ch in controlGauche.Children)
            {
                ch.Visibility = Visibility.Visible;
            }

            _poolInit.Release();
        }
        /// <summary>
        /// Créer la partie, initialise l'interface graphique et démarre la partie
        /// </summary>
        /// <param name="monteurC"></param>
        /// <param name="joueurs"></param>
        public void chargerPartie(MonteurCarte monteurC, List<Joueur> joueurs)
        {
            
            Task.Factory.StartNew(() =>
            {
                MonteurPartie1v1 monteurPartie = new MonteurPartie1v1();
                monteurPartie.creerPartie(monteurC, joueurs);
                partie = monteurPartie.Partie;

                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    initialiseInterface();
                }));

                commencerJeu();
            });
        }

        /// <summary>
        /// Actualise les éléments graphique sur le panneau gauche de la fenêtre
        /// </summary>
        private void actualisePanneauGauche()
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

        /// <summary>
        /// Actualise les éléments graphique sur le panneau droit de la fenêtre
        /// </summary>
        private void actualisePanneauDroit()
        {
            panelScroll.Children.Clear();
            if (selectionRectangle.Visibility == Visibility.Visible)
            {
                List<Unite> tmpLit = partie.Carte.getUniteFromCoord(selectionRectangle.Coord);
                if (tmpLit.Count > 0)
                {
                    foreach (Unite u in tmpLit)
                    {
                        GroupeUnite grp = new GroupeUnite(u);
                        if (partie.joueurActuel() == u.Proprietaire)
                            grp.MouseDown += grpUnit_MouseDown;
                        panelScroll.Children.Add(grp);
                    }
                }
            }
        }


        /// <summary>
        /// charge les éléments graphiques du panneau gauche
        /// </summary>
        private void chargerPanneauGauche()
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

            actualisePanneauGauche();
        }

        /// <summary>
        /// charge les éléments graphique du panneau droit
        /// </summary>
        private void chargerPanneauDroit()
        {
            controlDroit.Children.Clear();

            GroupeInfosCase grpInfo = new GroupeInfosCase();
            selectionRectangle.SelectChanged += grpInfo.actualiseData;
            controlDroit.Children.Add(grpInfo);


            ScrollViewer scrollInfoUnite = new ScrollViewer();
            scrollInfoUnite.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            panelScroll = new StackPanel();

            actualisePanneauDroit();

            scrollInfoUnite.Content = panelScroll;
            scrollInfoUnite.Height = 415;
            controlDroit.Children.Add(scrollInfoUnite);

        }

        /// <summary>
        /// Charge le canvas avec les éléments de la carte
        /// </summary>
        private void afficheCarte()
        {
            canvasMap.Children.Clear();
            canvasMap.Children.Add(selectionRectangle);
            for (int l = 0; l < partie.Carte.Hauteur; l++)
            {
                for (int c = 0; c < partie.Carte.Largeur; c++)
                {
                    var tile = partie.Carte.Cases[c][l];
                    var unite = partie.Carte.getUniteFromCoord(new Coordonnees(c, l));
                    var rect = creerTile(c, l, tile, unite);
                    canvasMap.Children.Add(rect);
                }
            }
        }
        /// <summary>
        /// Affiche les rectangles représentant les suggestions
        /// </summary>
        private void afficherSuggestions()
        {
            if (allowedMouv != null)
            {
                foreach (var pair in allowedMouv)
                {
                    if (pair.Value.Sugg >= 1)
                    {
                        var rect = creerRectSugg(pair.Key.X, pair.Key.Y);
                        rect.StrokeThickness = pair.Value.Sugg + 1;
                        canvasMap.Children.Add(rect);
                    }
                }
            }
        }
        /// <summary>
        /// Construit une case pour le canvas, et affiche l nombre d'unités présente sur la case
        /// </summary>
        /// <param name="c">la colonne</param>
        /// <param name="l">la ligne</param>
        /// <param name="tile">la case</param>
        /// <param name="listUnite">la liste d'unités présentent</param>
        /// <returns></returns>
        private Tile creerTile(int c, int l, ICase tile, List<Unite> listUnite)
        {
            var rectangle = new Tile(tile, tileFactory, listUnite);

            Canvas.SetLeft(rectangle, c * 50);
            Canvas.SetTop(rectangle, l * 50);
            Canvas.SetZIndex(rectangle,5);

            rectangle.MouseLeftButtonDown += new MouseButtonEventHandler(Rectangle_MouseDown);
            return rectangle;
        }

        private Rectangle creerRectSugg(int c, int l)
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
            int column = (int)Canvas.GetLeft(rect) / 50;
            int row = (int)Canvas.GetTop(rect) / 50;
            Canvas.SetLeft(selectionRectangle, Canvas.GetLeft(rect));
            Canvas.SetTop(selectionRectangle, Canvas.GetTop(rect));
            Canvas.SetZIndex(selectionRectangle, 999);
            selectionRectangle.Visibility = Visibility.Visible;
            selectionRectangle.TileSelected = rect;
            selectionRectangle.Coord = new Coordonnees(column, row);


            if (listUnitSelected.Count > 0 && allowedMouv.Count > 0)
            {
                partie.Carte.deplaceUnites(listUnitSelected, new Coordonnees(column, row), allowedMouv);
                listUnitSelected.Clear();
            }
            else
            {
                allowedMouv = null;
                afficherSuggestions();
            }


            afficheCarte();
            actualisePanneauDroit();
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
                afficheCarte();
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
                    afficheCarte();
                    allowedMouv = null; // on supprime l'affichage  des suggestions
                    afficherSuggestions();
                }
                else
                {
                    loadDataSuggestion(); //sinon on réactualise l'affichage des suggestions avec les unités restantes
                }
            }
        }
        /// <summary>
        /// Charge les suggestions et les affiche
        /// </summary>
        private void loadDataSuggestion()
        {
            Task.Factory.StartNew(() =>
            {
                Unite unit = uniteAvecMoinsDepl();
                allowedMouv = sugg.getSuggestion(partie.Carte, unit); // on charge les suggestions pour cette unités ( et qui servira pour toutes les unités select)

                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    afficheCarte();
                    afficherSuggestions();
                }));
            });
        }
        /// <summary>
        /// Retourne l'unité avec le moins de points de déplacement
        /// </summary>
        /// <returns></returns>
        private Unite uniteAvecMoinsDepl()
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
            return unit;
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (partie != null)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.FileName = "saveSmallWorld"; // Default file name
                dlg.DefaultExt = ".sav"; // Default file extension
                dlg.Filter = "Save SmallWorld (.sav)|*.sav"; // Filter files by extension

                // Show save file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                // Process save file dialog box results
                if (result == true)
                {
                    // Save document
                    string filename = dlg.FileName;

                    /*
                    IFormatter formatter = new BinaryFormatter();
                    Stream stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
                    formatter.Serialize(stream, partie);
                    stream.Close();
                    */
                    
                    XmlSerializer mySerializer = new XmlSerializer(partie.GetType());
                    StreamWriter myWriter = new StreamWriter(filename);
                    mySerializer.Serialize(myWriter, partie);
                    myWriter.Close();
                }
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            openFileDialog1.FileName = null;
            openFileDialog1.Filter = "Save SmallWorld (.sav)|*.sav";

            string openFileName;

            Nullable<bool> res = openFileDialog1.ShowDialog();

            if (res == true)
            {
                openFileName = openFileDialog1.FileName;
                try
                {
                    /*
                    IFormatter formatter = new BinaryFormatter();
                    Stream stream = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                    partie = (Partie)formatter.Deserialize(stream);
                    stream.Close();
                    */
                    
                    XmlSerializer mySerializer = new XmlSerializer(typeof(Partie1v1));
                    partie = (Partie)mySerializer.Deserialize(openFileDialog1.OpenFile());

                    partie.associeJoueursUnite();
                    initialiseInterface();
                    commencerJeu();
                }
                catch (Exception)
                {
                    MessageBox.Show("Un erreur s'est produite pendant l'ouverture de la sauvegarde.");
                }
            }
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
           EditeurCarte fen = new EditeurCarte();
            fen.Owner = this;
            fen.ShowDialog();
            e.Handled = true;
        }

        private void default_Click(object sender, RoutedEventArgs e)
        {
            tileFactory = new ImageFactory();

            if (partie != null && partie.Carte != null)
                afficheCarte();
            e.Handled = true;
        }

        private void groovy_Click(object sender, RoutedEventArgs e)
        {
            tileFactory = new ImageFactory("groovy");

            if (partie != null && partie.Carte != null)
                afficheCarte();
            e.Handled = true;
        }

        private void tropical_Click(object sender, RoutedEventArgs e)
        {
            tileFactory = new ImageFactory("tropical");

            if (partie != null && partie.Carte != null)
                afficheCarte();
            e.Handled = true;
        }

        private void noStyle_Click(object sender, RoutedEventArgs e)
        {
            tileFactory = new RectangleFactory();

            if (partie != null && partie.Carte != null)
                afficheCarte();
            e.Handled = true;
        }

        private void campaign_Click(object sender, RoutedEventArgs e)
        {
            tileFactory = new ImageFactory("campaign");

            if (partie != null && partie.Carte != null)
                afficheCarte();
            e.Handled = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
