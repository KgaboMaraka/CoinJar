using CoinJar.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using System.Xml;
using System.Net.Http;
using System.Web.Http.Hosting;
using System.Web.Http;
using CoinJar.Models;
using System.Net;

namespace CoinJar.Tests.Controllers
{
    /// <summary>
    /// Summary description for CoinsControllerTest
    /// </summary>
    [TestClass]
    public class CoinsControllerTest
    {
        //public CoinsControllerTest()
        //{
        //    //
        //    // TODO: Add constructor logic here
        //    //
        //}



        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void GetTotalAmount_Returns_The_Correct_Amount_Of_Coins()
        {
            //ARRANGE
            decimal testToTal = 0;
            using (new FakeHttpContext.FakeHttpContext())
            {
                XDocument xmlDoc = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/Coins.xml"));

                    testToTal = xmlDoc.Descendants("Coins").Descendants("Coin")
                          .Select(x => (decimal)x.Element("Amount"))
                          .Sum();

                var controller = new CoinsController(xmlDoc);

                //ACT
                decimal result = controller.Get();

                //ASSERT
                Assert.AreEqual(testToTal, result);

            }
        }

        [TestMethod]
        public void AddCoin_Shoud_Return_HttpStatusCode_Created()
        {
            //ARRANGE
            using (new FakeHttpContext.FakeHttpContext())
            {
                XDocument xmlDoc = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/Coins.xml"));

                var coin = new Coin()
                {
                    Amount = 35,
                    Volume = 9
                };

                var controller = new CoinsController(xmlDoc);
                
                controller.Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                };

                //ACT
                var response = controller.Post(coin); 

                //ASSERT                
                Assert.IsTrue(CoinAdded(coin), "AddCoin method in ICoinJar not invoked");
                Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            }
        }

        [TestMethod]
        public void AddCoin_Shoud_Return_HttpStatusCode_PreconditionFailed()
        {
            //ARRANGE
            using (new FakeHttpContext.FakeHttpContext())
            {
                XDocument xmlDoc = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/Coins.xml"));

                var coin = new Coin()
                {
                    Amount = 35,
                    Volume = 45
                };

                var controller = new CoinsController(xmlDoc);

                controller.Request = new HttpRequestMessage()
                {
                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
                };

                //ACT
                var response = controller.Post(coin);

                //ASSERT                
                Assert.IsFalse(CoinAdded(coin), "AddCoin method in ICoinJar has been invoked");
                Assert.AreEqual(HttpStatusCode.PreconditionFailed, response.StatusCode);

            }
        }

        public bool CoinAdded(Coin coin)
        {
            try
            {
                XDocument xmlDoc = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/Coins.xml"));
                var items = (from item in xmlDoc.Descendants("Coins").Descendants("Coin") select item).ToList();
                XElement addedCoin = items.Where(p => p.Element("Amount").Value == coin.Amount.ToString() && p.Element("Volume").Value == coin.Volume.ToString()).FirstOrDefault();

                if (addedCoin != null)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }   
        }
    }
}
