using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Modele
{
    public class JoueurConcret : Joueur
    {
        public JoueurConcret(FabriquePeuple fab, String color, String nom)
            : base(fab, color, nom,new Suggestion())
        {
            sem = new Semaphore(0, 1);
        }

        public JoueurConcret()
            : base()
        {
            sem = new Semaphore(0, 1);
        }

        Semaphore sem;

        public override void jouerTour(Partie partie)
        {
            sem.WaitOne();
        }
        public override void finirTour()
        {
            sem.Release();
        }

    }
}
