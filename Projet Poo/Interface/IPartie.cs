using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public interface IPartie
    {

        void initialisation();

        void tourSuivant();

        void joueurSuivant(IJoueur j);

    }
}
