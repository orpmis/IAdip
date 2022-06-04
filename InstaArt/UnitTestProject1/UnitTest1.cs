using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InstaArt;
using System.Linq;
using InstaArt.DbModel;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Authorizarion_Corret()
        {
            string log = "user1";
            string pass = "pass";

            users act = DataBase.Authorization(log, pass).Result;
            int expected = 1;

            Assert.AreEqual(expected, act.id);
        }
        [TestMethod]
        public void Authorizarion_InCorret()
        {
            string log = "user1";
            string pass = "pass";

            users act = DataBase.Authorization(log, pass).Result;
            int expected = 2;

            Assert.AreEqual(expected, act.id);
        }
        [TestMethod]
        public void NotFreeNick()
        {
            string log = "user1";

            bool act = DataBase.IsNicknameFree(log).Result;
            bool expected = false;

            Assert.AreEqual(expected, act);
        }
        [TestMethod]
        public void FreeNick()
        {
            string log = "user2";

            bool act = DataBase.IsNicknameFree(log).Result;
            bool expected = true;

            Assert.AreEqual(expected, act);
        }
        [TestMethod]
        public void FindRootFolder_ForPhoto22()
        {
            photos act = DataBase.GetContext().photos.Where(f => f.id == 22).FirstOrDefault();
            int expected = 20;

            Assert.AreEqual(expected, act.root.Value);
        }
        [TestMethod]
        public void FindRootFolder_ForPhoto2()
        {
            photos act = DataBase.GetContext().photos.Where(f => f.id == 2).FirstOrDefault();
            int? expected = null;

            Assert.AreEqual(expected, act.root);
        }
    }
}
