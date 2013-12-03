using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modele
{
    public interface IPartie
    {


        void tourSuivant();
        void ajoutJoueur(Joueur j);
        void joueurSuivant();
        Joueur joueurActuel();

    }
}
