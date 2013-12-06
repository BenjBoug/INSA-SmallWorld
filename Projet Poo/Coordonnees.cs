using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele
{
    class Coordonnees
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
    }
}
