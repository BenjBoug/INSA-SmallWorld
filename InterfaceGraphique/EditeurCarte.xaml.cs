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
        private int tailleCarte;
        private Carte map;

        public EditeurCarte()
        {
            InitializeComponent();
            //map = new Carte();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        { }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        { }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        { }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        { }

    }
}
