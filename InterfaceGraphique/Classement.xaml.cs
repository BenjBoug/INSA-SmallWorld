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
using System.Windows.Shapes;
using Modele;

namespace InterfaceGraphique
{
    /// <summary>
    /// Logique d'interaction pour Classement.xaml
    /// </summary>
    public partial class Classement : Window
    {
        public Classement(List<Joueur> classement)
        {
            InitializeComponent();
            int i = 0;
            int k = 1;
            foreach (Joueur j in classement)
            {
                classementPanel.Children.Add(new JoueurClassement(j,i+1));
                int index = classement.IndexOf(j);
                if (index+1 < classement.Count && classement[index + 1].Points < j.Points)
                    i=k;
                k++;
            }
        }
    }
}
