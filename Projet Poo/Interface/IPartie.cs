using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public interface IPartie
    {


        void tourSuivant();
        void ajoutJoueur(IJoueur j);
        void joueurSuivant();
        IJoueur joueurActuel();

    }
}
