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
        unsafe public void TestGenerationMap()
        {
            WrapperMapAleatoire wrapMap = new WrapperMapAleatoire();

            //génére une carte 5x5 avec au moins 5 chiffres différents de 0 à 4
            //int** map = wrapMap.generer(5, 5);
        }
        [TestMethod]
        public void TestMonteurDemo()
        {
            Carte c;
            MonteurCarte monteur = new MonteurNormale();
            monteur.creerCarte();
            c = monteur.Carte;
            Assert.IsNotNull(c);
            Assert.IsNotNull(c.Cases);
            Assert.AreEqual<int>(c.Largeur, 5);
            Assert.AreEqual<int>(c.Hauteur, 5);
            Assert.AreEqual<int>(c.NbToursMax, 10);
            Assert.AreEqual<int>(c.NbUniteParPeuble, 5);
        }
        [TestMethod]
        public void TestMonteurPetite()
        {
            Carte c;
            MonteurCarte monteur = new MonteurNormale();
            monteur.creerCarte();
            c = monteur.Carte;
            Assert.IsNotNull(c);
            Assert.IsNotNull(c.Cases);
            Assert.AreEqual<int>(c.Largeur, 5);
            Assert.AreEqual<int>(c.Hauteur, 5);
            Assert.AreEqual<int>(c.NbToursMax, 15);
            Assert.AreEqual<int>(c.NbUniteParPeuble, 8);
        }
        [TestMethod]
        public void TestMonteurNormale()
        {
            Carte c;
            MonteurCarte monteur = new MonteurNormale();
            monteur.creerCarte();
            c = monteur.Carte;
            Assert.IsNotNull(c);
            Assert.IsNotNull(c.Cases);
            Assert.AreEqual<int>(c.Largeur, 5);
            Assert.AreEqual<int>(c.Hauteur, 5);
            Assert.AreEqual<int>(c.NbToursMax, 25);
            Assert.AreEqual<int>(c.NbUniteParPeuble, 15);
        }
    }
}
