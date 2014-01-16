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
        private string filename;
        public string Filename
        {
            set { filename = value; }
        }
        private bool saved;
        
        public bool Saved 
        {
            set { saved = value; }
        }
        private bool neverSaved;
        
        public bool NeverSaved
        {
            set { neverSaved = value; }
        }

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
            selectionRectangle.Visibility = Visibility.Collapsed;
            filename = "saveSmallWorld";
            saved = true;
            neverSaved = true;
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
            Dispatcher.BeginInvoke(new Action(() =>
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
        /// Affiche le classement dans une fenêtre
        /// </summary>
        private void finJeu()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                canvasMap.Children.Clear();
                for (int l = 0; l < partie.Carte.Hauteur; l++)
                {
                    for (int c = 0; c < partie.Carte.Largeur; c++)
                    {
                        var tile = partie.Carte.Cases[c][l];
                        var unite = partie.Carte.getUniteFromCoord(new Coordonnees(c, l));
                        var rect = creerTile(c, l, tile, unite);
                        rect.rect1.StrokeThickness = 0;
                        canvasMap.Children.Add(rect);
                    }
                }
                actualisePanneauGauche();
                Classement fen = new Classement(partie.Classement.ToList());
                fen.Owner = this;
                fen.ShowDialog();
            }));
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

            selectionRectangle.Visibility = Visibility.Collapsed;

            afficheCarte();
            actualisePanneauDroit();
            chargerPanneauGauche();            

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
                MonteurPartieLocale monteurPartie = new MonteurPartieLocale();
                monteurPartie.creerPartie(monteurC, joueurs);
                partie = monteurPartie.Partie;

                Dispatcher.BeginInvoke(new Action(() =>
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
            if (!partie.Finpartie)
                labelJoueurActuel.Content = "C'est à " + partie.joueurActuel().Nom + " de jouer !";
            else
                labelJoueurActuel.Content = "La partie est terminée";
            labelNbTour.Content = "Tour: " + partie.NbTours + "/" + partie.Carte.NbToursMax;

            boutonFinir.IsEnabled = false;
            if (partie.joueurActuel() is JoueurConcret && !partie.Finpartie)
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
                actualiseData();
                grpInfo.Visibility = Visibility.Visible;

                List<Unite> tmpLit = partie.Carte.getUniteFromCoord(selectionRectangle.Coord);
                if (tmpLit.Count > 0 /*&& tmpLit[0].IdProprietaire == partie.IndiceJoueurActuel*/)
                {
                    foreach (Unite u in tmpLit)
                    {
                        GroupeUnite grp = new GroupeUnite(u);
                        if (partie.joueurActuel() == u.Proprietaire)
                        {
                            grp.MouseDown += grpUnit_MouseDown;
                            grp.Selectable = true;
                            grp.Cursor = Cursors.Hand;
                        }
                        panelScroll.Children.Add(grp);
                    }
                }
            }
            else
                grpInfo.Visibility = Visibility.Collapsed;
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
                        rect.StrokeThickness = 3;
                        rect.Stroke = new SolidColorBrush(SuggCouleurFactory.getCouleur(pair.Value.Sugg-1));
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
            var rectangle = new Tile(tile, tileFactory, listUnite, (SolidColorBrush)new BrushConverter().ConvertFromString(partie.joueurActuel().Couleur));

            Canvas.SetLeft(rectangle, c * 50);
            Canvas.SetTop(rectangle, l * 50);
            Canvas.SetZIndex(rectangle,1);

            rectangle.MouseLeftButtonDown += new MouseButtonEventHandler(Rectangle_MouseDown);
            return rectangle;
        }

        private Rectangle creerRectSugg(int c, int l)
        {
            var rectangle = new Rectangle();
            Canvas.SetLeft(rectangle, c * 50);
            Canvas.SetTop(rectangle, l * 50);
            Canvas.SetZIndex(rectangle, 2);

            rectangle.Stroke = Brushes.White;
            rectangle.StrokeThickness = 2;
            rectangle.StrokeDashOffset = 4;
            if (selectionRectangle.Coord == new Coordonnees(c, l))
                rectangle.Opacity = 0;
            rectangle.Width = TAILLE;
            rectangle.Height = TAILLE;
            //rectangle.RadiusX = 3;
            //rectangle.RadiusY = 3;

            return rectangle;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!partie.Finpartie)
            {
                var rect = sender as Tile;
                int column = (int)Canvas.GetLeft(rect) / 50;
                int row = (int)Canvas.GetTop(rect) / 50;
                Canvas.SetLeft(selectionRectangle, Canvas.GetLeft(rect));
                Canvas.SetTop(selectionRectangle, Canvas.GetTop(rect));
                Canvas.SetZIndex(selectionRectangle, 3);
                selectionRectangle.selectRect.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString(partie.joueurActuel().Couleur);
                selectionRectangle.Visibility = Visibility.Visible;
                selectionRectangle.TileSelected = rect;
                selectionRectangle.Coord = new Coordonnees(column, row);

                if (listUnitSelected.Count > 0 && allowedMouv.Count > 0)
                {
                    partie.Carte.deplaceUnites(listUnitSelected, new Coordonnees(column, row), allowedMouv);
                    listUnitSelected.Clear();
                    saved = false;
            }
            else
            {
                allowedMouv = null;
                afficherSuggestions();
            }


            afficheCarte();
            actualisePanneauDroit();
        }
        }

        private void actualiseData()
        {
            if (selectionRectangle.TileSelected != null)
            {
                rect.Fill = new Tile(selectionRectangle.TileSelected.TileType, tileFactory, new List<Unite>(), Brushes.White).Background;
                int column = (int)Canvas.GetLeft(selectionRectangle) / 50;
                int row = (int)Canvas.GetTop(selectionRectangle) / 50;
                typeCase.Text = "Terrain : " + (selectionRectangle.TileSelected.TileType).ToString();
                nbUnite.Text = "Unités sur la case : " + partie.Carte.getUniteFromCoord(selectionRectangle.Coord).Count;
                coord.Text = "Coordonnées : ( " + (column+1).ToString() + " ; " + (row+1).ToString() + " )";
            }
        }

        private void MenuItem_Click_Nouvelle(object sender, RoutedEventArgs e)
        {
            NouvellePartie fen = new NouvellePartie();
            fen.Owner = this;

            if (saved || partie == null || partie.Finpartie)
            fen.ShowDialog();
            else
            {
                MessageBoxResult result = MessageBox.Show("Voulez-vous sauvegarder cette partie avant d'en créer une nouvelle ?", "Partie non sauvegardée", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        if (sauvegarder())
                            fen.ShowDialog();
                        break;
                    case MessageBoxResult.No:
                        fen.ShowDialog();
                        break;
                    case MessageBoxResult.Cancel:
                    default:
                        break;
                }
            }
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
            selectionRectangle.Visibility = Visibility.Collapsed;
            actualiseData();
            saved = false;
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

        private void MenuItem_Click_SauvegarderSous(object sender, RoutedEventArgs e)
        {
            sauvegarderSous();
        }

        private void MenuItem_Click_Sauvegarder(object sender, RoutedEventArgs e)
        {
            sauvegarder();
        }

        private bool sauvegarderSous()
        {
            if (partie != null)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.FileName = filename; // Default file name
                dlg.DefaultExt = ".sav"; // Default file extension
                dlg.Filter = "Save SmallWorld (.sav)|*.sav"; // Filter files by extension

                // Show save file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                // Process save file dialog box results
                if (result == true)
                {
                    // Save document
                    filename = dlg.FileName;

                    XmlSerializer mySerializer = new XmlSerializer(partie.GetType());
                    StreamWriter myWriter = new StreamWriter(filename);
                    mySerializer.Serialize(myWriter, partie);
                    myWriter.Close();
                    saved = true;
                    neverSaved = false;
                    return true;
                }
            }
            return false;
        }

        private bool sauvegarder()
        {
            if (neverSaved)
                return sauvegarderSous();
            else
            {
                XmlSerializer mySerializer = new XmlSerializer(partie.GetType());
                StreamWriter myWriter = new StreamWriter(filename);
                mySerializer.Serialize(myWriter, partie);
                myWriter.Close();
                saved = true;
                return true;
            }
        }

        private void MenuItem_Click_Ouvrir(object sender, RoutedEventArgs e)
        {
            if (saved || partie == null || partie.Finpartie)
                openDialog();
            else
            {
                MessageBoxResult result = MessageBox.Show("Voulez-vous sauvegarder cette partie avant de charger la nouvelle ?", "Partie non sauvegardée", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        if (sauvegarder())
                            openDialog();
                        break;
                    case MessageBoxResult.No:
                        openDialog();
                        break;
                    case MessageBoxResult.Cancel:
                    default:
                        break;
                }
            }
        }

        private void openDialog()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            openFileDialog1.FileName = null;
            openFileDialog1.Filter = "Save SmallWorld (.sav)|*.sav";

            Nullable<bool> res = openFileDialog1.ShowDialog();

            if (res == true)
                ouvrir(openFileDialog1.FileName);
        }

        private void ouvrir(string filepath)
        {
            Stream file = null; 
            try
            {
                file = File.OpenRead(filepath);

                XmlSerializer mySerializer = new XmlSerializer(typeof(PartieLocale));
                partie = (Partie)mySerializer.Deserialize(file);

                file.Close();

                partie.associeJoueursUnite();
                filename = filepath;
                saved = true;
                neverSaved = false;
                sauvegarderMenuItem.IsEnabled = true;
                sauvegarderSousMenuItem.IsEnabled = true;
                initialiseInterface();
                commencerJeu();
            }
            catch (Exception)
            {
                MessageBox.Show("Un erreur s'est produite pendant l'ouverture de la sauvegarde.");
                file.Close();
            }
        }

        private void Save_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (saved || partie == null || partie.Finpartie)
                    ouvrir(files[0]);
                else
                {
                    MessageBoxResult result = MessageBox.Show("Voulez-vous sauvegarder cette partie avant de charger la nouvelle ?", "Partie non sauvegardée", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            if (sauvegarder())
                                ouvrir(files[0]);
                            break;
                        case MessageBoxResult.No:
                            ouvrir(files[0]);
                            break;
                        case MessageBoxResult.Cancel:
                        default:
                            break;
                    }
                }           
            }
        }

        private void MenuItem_Click_Editeur(object sender, RoutedEventArgs e)
        {
           EditeurCarte fen = new EditeurCarte();
            fen.Owner = this;
            fen.ShowDialog();
            e.Handled = true;
        }

        private void styleUncheck()
        {
            defaultStyle.IsChecked = false;
            groovy.IsChecked = false;
            tropical.IsChecked = false;
            noStyle.IsChecked = false;
            campaign.IsChecked = false;
        }


        private void default_Click(object sender, RoutedEventArgs e)
        {
            tileFactory = new ImageFactory();
            styleUncheck();
            defaultStyle.IsChecked = true;

            if (partie != null && partie.Carte != null)
                afficheCarte();
            actualiseData();
            e.Handled = true;
        }

        private void groovy_Click(object sender, RoutedEventArgs e)
        {
            tileFactory = new ImageFactory("groovy");
            styleUncheck();
            groovy.IsChecked = true;
            
            if (partie != null && partie.Carte != null)
                afficheCarte();
            actualiseData();
            e.Handled = true;
        }

        private void tropical_Click(object sender, RoutedEventArgs e)
        {
            tileFactory = new ImageFactory("tropical");
            styleUncheck();
            tropical.IsChecked = true;
            
            if (partie != null && partie.Carte != null)
                afficheCarte();
            actualiseData();
            e.Handled = true;
        }

        private void noStyle_Click(object sender, RoutedEventArgs e)
        {
            tileFactory = new RectangleFactory();
            styleUncheck();
            noStyle.IsChecked = true;
            
            if (partie != null && partie.Carte != null)
                afficheCarte();
            actualiseData();
            e.Handled = true;
        }

        private void campaign_Click(object sender, RoutedEventArgs e)
        {
            tileFactory = new ImageFactory("campaign");
            styleUncheck();
            campaign.IsChecked = true;
            
            if (partie != null && partie.Carte != null)
                afficheCarte();
            actualiseData();
            e.Handled = true;
        }

        private void MenuItem_Click_Quitter(object sender, RoutedEventArgs e)
        {
            if (saved || partie == null || partie.Finpartie)
                Close();
            else
            {
                MessageBoxResult result = MessageBox.Show("Voulez-vous sauvegarder avant de quitter ?", "Partie non sauvegardée", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        if(sauvegarder())
                            Close();
                        break;
                    case MessageBoxResult.No:
                        Close();
                        break;
                    case MessageBoxResult.Cancel:
                    default:
                        break;
                }
            }
        }

        private void Button_Click_Reduire(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void help_Click(object sender, RoutedEventArgs e)
        {
            Aide a = new Aide();
            a.ShowDialog();
        }
    }
}
