using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele
{
    public class Coordonnees
    {
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

        public int X
        {
            get { return x; }
            set { x = value; }
        }
        private int y;

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

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + x.GetHashCode();
            hash = (hash * 7) + y.GetHashCode();
            return hash;
        }
    }
}
