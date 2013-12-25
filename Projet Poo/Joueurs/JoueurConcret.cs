using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Modele
{
    public class JoueurConcret : Joueur, IDisposable
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

        ~JoueurConcret()
        {
            Dispose(false);
        }

        private Semaphore sem;
        private bool disposed = false;

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


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    sem.Dispose();
                }

                disposed = true;
            }
        }
    }
}
