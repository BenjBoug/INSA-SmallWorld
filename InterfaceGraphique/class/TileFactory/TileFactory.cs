using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Modele;

namespace InterfaceGraphique
{
    abstract class TileFactory
    {
        public abstract Brush getViewTile(ICase c);
    }
}
