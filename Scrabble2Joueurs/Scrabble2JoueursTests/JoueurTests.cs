using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scrabble2Joueurs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble2Joueurs.Tests
{
    [TestClass()]
    public class JoueurTests
    {
        [TestMethod()]
        public void JoueurTest()
        {
            Joueur J1 = new Joueur("DaNoob");
            Assert.AreEqual("DaNoob", J1.GetNom(), "Le nom du joueur doit être DaNoob");
        }

        [TestMethod()]
        public void AjouterMotTest()
        {
            Joueur J1 = new Joueur("DaNoob");

            J1.AjouterMot("Anticonstitutionnellement");

            double expected = J1.GetTotalPoints();

            Assert.AreEqual("Anticonstitutionnellement", J1.GetLesMots()[0]);

            Assert.AreEqual(28,expected,"Beaucoup de points");

            J1.AjouterMot("Xylophone");

            expected = J1.GetTotalPoints();

            Assert.AreEqual(60, expected, "Encore plus de points");
            Assert.AreEqual("Xylophone", J1.GetLesMots()[1]);

            J1.AjouterMot("Schizothymiques");

            expected = J1.GetTotalPoints();

            Assert.AreEqual(109, expected, "IT'S OVER NINE THOUSAND !!!!");
        }

        [TestMethod()]
        public void GetTotalPointsTest()
        {
            Joueur J1 = new Joueur("DaNoob");
            J1.AjouterMot("Anticonstitutionnellement");
            double expected = J1.GetTotalPoints();
            Assert.AreEqual(28, expected, "Beaucoup de points");
        }

        [TestMethod()]
        public void GetNbMotsTest()
        {
            Joueur J1 = new Joueur("DaNoob");
            J1.AjouterMot("Anticonstitutionnellement");
            J1.AjouterMot("Xylophone");
            J1.AjouterMot("Schizothymiques");
            Assert.AreEqual(3, J1.GetNbMots());
        }

        [TestMethod()]
        public void GetLesMotsTest()
        {
            Joueur J1 = new Joueur("DaNoob");
            J1.AjouterMot("Anticonstitutionnellement");
            J1.AjouterMot("Xylophone");
            J1.AjouterMot("Schizothymiques");
            Assert.AreEqual("Anticonstitutionnellement", J1.GetLesMots()[0]);
            Assert.AreEqual("Xylophone", J1.GetLesMots()[1]);
            Assert.AreEqual("Schizothymiques", J1.GetLesMots()[2]);
        }

        [TestMethod()]
        public void MotMeilleurTest()
        {
            Joueur J1 = new Joueur("DaNoob");

            J1.AjouterMot("Anticonstitutionnellement");
            J1.AjouterMot("Xylophone");
            J1.AjouterMot("Schizothymiques");

            Assert.AreEqual("Schizothymiques",J1.MotMeilleur(),"Schizo...");
        }
    }
}