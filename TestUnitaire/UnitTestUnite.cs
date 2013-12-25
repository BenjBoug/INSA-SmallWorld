using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modele;

namespace TestUnitaire
{
    [TestClass]
    public class UnitTestUnite
    {
        [TestMethod]
        public void TestPointVie()
        {
            Unite u = new Unite();
            Assert.IsTrue(u.estEnVie());

            u.perdPV(1);
            Assert.AreEqual(u.PointsVieMax - 1, u.PointsVie);
        }
    }
}
