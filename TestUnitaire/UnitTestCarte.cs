using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibCLR;
using Modele;
namespace TestUnitaire
{
    [TestClass]
    public class UnitTestCarte
    {
        [TestMethod]
        public void TestMonteurDemo()
        {
            Carte c;
            MonteurCarte monteur = new MonteurDemo(new Aleatoire());
            monteur.creerCarte();
            c = monteur.Carte;
            Assert.IsNotNull(c);
            Assert.IsNotNull(c.Cases);
        }
        [TestMethod]
        public void TestMonteurPetite()
        {
            Carte c;
            MonteurCarte monteur = new MonteurPetite(new Aleatoire());
            monteur.creerCarte();
            c = monteur.Carte;
            Assert.IsNotNull(c);
            Assert.IsNotNull(c.Cases);
        }
        [TestMethod]
        public void TestMonteurNormale()
        {
            Carte c;
            MonteurCarte monteur = new MonteurNormale(new Aleatoire());
            monteur.creerCarte();
            c = monteur.Carte;
            Assert.IsNotNull(c);
            Assert.IsNotNull(c.Cases);
        }

        [TestMethod]
        public void TestDeplacement()
        {
            Carte c = creerCarteRapide();

            Unite unite = new Unite();
            unite.Coord = new Coordonnees(0,0);
            unite.Proprietaire = new JoueurConcret(new FabriquePeupleGaulois(), "Red", "j");
            unite.IdProprietaire = 0;

            SuggMap sugg = unite.StrategySuggestion.getSuggestion(c,unite);
            
            c.deplaceUnite(unite,new Coordonnees(0,1),sugg);
            Assert.AreEqual<Coordonnees>(unite.Coord, new Coordonnees(0, 1));

        }

        private Carte creerCarteRapide()
        {
            Carte c;
            MonteurCarte monteur = new MonteurDemo(new Aleatoire());
            monteur.creerCarte();
            c = monteur.Carte;

            for(int i=0;i<5;i++)
            {
                for (int j=0;j<5;j++)
                {
                    c.Cases[i][j] = c.FabriqueCase.getCase((int)Case.CaseInt.Plaine);
                }
            }

            return c;
        }
    }
}
