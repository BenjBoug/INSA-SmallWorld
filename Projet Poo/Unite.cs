using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Modele
{
    public class Unite : IUnite
    {
        public Unite()
        {
            pointsAttaque = 2;
            pointsDefense = 1;
            pointsDepl = 1;
            pointsVie = 2;
            pointsVieMax = pointsVie;
        }

        private int pointsDefense;

        public int PointsDefense
        {
            get { return pointsDefense; }
            set { pointsDefense = value; }
        }
        private int pointsAttaque;

        public int PointsAttaque
        {
            get { return pointsAttaque; }
            set { pointsAttaque = value; }
        }

        private int pointsVieMax;

        public int PointsVieMax
        {
            get { return pointsVieMax; }
            set { pointsVieMax = value; }
        }


        private int pointsVie;

        public int PointsVie
        {
            get { return pointsVie; }
            set { pointsVie = value; }
        }
        private int pointsDepl;

        public int PointsDepl
        {
            get { return pointsDepl; }
            set { pointsDepl = value; }
        }
        private Joueur proprietaire;

        [XmlIgnore]
        public Joueur Proprietaire
        {
            get { return proprietaire; }
            set { proprietaire = value; idProp = proprietaire.Id; }
        }

        private int idProp;
        public int IdProprietaire
        {
            get { return idProp; }
            set { idProp = value; }
        }


        public void attaquer(IUnite adversaire)
        {
            throw new NotImplementedException();
        }

        public void perdPV(int nb)
        {
            PointsVie -= nb;
            if (PointsVie < 0)
                PointsVie = 0;
        }

        public bool estEnVie()
        {
            return PointsVie > 0;
        }


    }
}
