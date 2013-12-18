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
