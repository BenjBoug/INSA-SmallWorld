using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele
{
    /// <summary>
    /// Représente une coordonnées en 2D. 
    /// </summary>
    [Serializable]
    public class Coordonnees
    {
        /// <summary>
        /// Construit une coordonnees x=0,y=0
        /// </summary>
        public Coordonnees()
        {
            x = 0;
            y = 0;
        }
        /// <summary>
        /// Construit une coordonnnees avec les valeurs (x,y)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Coordonnees(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        private int x;
        /// <summary>
        /// La valeur en X de la coordonnees
        /// </summary>
        public int X
        {
            get { return x; }
            set { x = value; }
        }
        private int y;
        /// <summary>
        /// La valeur en Y de la coordonnees
        /// </summary>
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public static bool operator ==(Coordonnees a, Coordonnees b)
        {
            return (a.x==b.x && a.y==b.y);
        }

        public static bool operator !=(Coordonnees a, Coordonnees b)
        {
            return (a.x != b.x || a.y != b.y);
        }

        public override bool Equals(object obj)
        {
            Coordonnees fooItem = obj as Coordonnees;

            return fooItem.X == this.X && fooItem.y == this.y;
        }
        /// <summary>
        /// Retourne la coordonnées de la liste la plus proche de l'instance.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public Coordonnees getClosiestCoord(List<Coordonnees> list)
        {
            Coordonnees plusProche = null;
            double distMin = int.MaxValue;
            foreach (Coordonnees c in list)
            {
                if (distance(c) < distMin)
                {
                    plusProche = c;
                    distMin = distance(c);
                }
            }

            return plusProche;
        }

        /// <summary>
        /// Calcul la distance entre deux coordonnées
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public double distance(Coordonnees b)
        {
            return Math.Sqrt(Math.Pow(this.X - b.X, 2) + Math.Pow(this.Y - b.Y, 2));
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + x.GetHashCode();
            hash = (hash * 7) + y.GetHashCode();
            return hash;
        }
    }
}
