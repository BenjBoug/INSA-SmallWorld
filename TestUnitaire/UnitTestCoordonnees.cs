using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modele;
using System.Collections.Generic;

namespace TestUnitaire
{
    [TestClass]
    public class UnitTestCoordonnees
    {
        [TestMethod]
        public void TestCoordonneesEgalOperator()
        {
            Coordonnees coord1 = new Coordonnees(0, 1);
            Coordonnees coord2 = new Coordonnees(0, 1);
            Coordonnees coord3 = new Coordonnees(2, 1);

            Assert.AreEqual(new Coordonnees(0, 1), new Coordonnees(0, 1));
            Assert.AreEqual(coord1, coord1);
            Assert.AreEqual(coord1, coord2);
            Assert.AreNotEqual(coord2, coord3);
            Assert.AreNotEqual(new Coordonnees(0, 1), new Coordonnees(0, 2));

            Assert.IsTrue(coord1 == coord2);
            Assert.IsTrue(coord1 != coord3);
            Assert.IsFalse(coord3 == coord1);
            Assert.IsFalse(coord1 != coord2);
        }

        [TestMethod]
        public void TestDistance()
        {
            Coordonnees coord1 = new Coordonnees(0, 1);
            Coordonnees coord2 = new Coordonnees(0, 1);
            Coordonnees coord3 = new Coordonnees(0, 2);

            Assert.AreEqual(0.0, coord1.distance(coord1));
            Assert.AreEqual(0.0, coord1.distance(coord2));
            Assert.AreEqual(1.0, coord1.distance(coord3));
        }

        [TestMethod]
        public void TestGetClosiest()
        {
            List<Coordonnees> list = new List<Coordonnees>();

            list.Add(new Coordonnees(0, 0));
            list.Add(new Coordonnees(0, 1));
            list.Add(new Coordonnees(0, 2));

            Assert.AreEqual(new Coordonnees(0, 0), new Coordonnees(1, 0).getClosiestCoord(list));
            Assert.AreEqual(new Coordonnees(0, 1), new Coordonnees(1, 1).getClosiestCoord(list));
            Assert.AreEqual(new Coordonnees(0, 2), new Coordonnees(1, 2).getClosiestCoord(list));

        }
    }
}
