using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Modele
{
    [Serializable]
    public class JoueurConcret : Joueur
    {
        public JoueurConcret(FabriquePeuple fab, String color, String nom)
            : base(fab, color, nom)
        {
            sem = new Semaphore(0, 1);
        }

        public JoueurConcret()
            : base()
        {
            sem = new Semaphore(0, 1);
        }

        [NonSerialized]
        private Semaphore sem;
        /// <summary>
        /// Pour un joueur concret, on bloque le Thread du jeu avec un sémaphore
        /// </summary>
        /// <param name="partie"></param>
        public override void jouerTour(Partie partie)
        {
            sem.WaitOne();
        }
        /// <summary>
        /// On libère le thread du jeu
        /// </summary>
        public override void finirTour()
        {
            sem.Release();
        }

    }
}
