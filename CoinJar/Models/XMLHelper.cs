using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace CoinJar.Models
{
    public class XMLHelper
    {
        XDocument xmlDoc = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/Coins.xml"));
        public decimal TotalAmount()
        {
            try
            {
                decimal total = xmlDoc.Descendants("Coins").Descendants("Coin")
                  .Select(x => (decimal)x.Element("Amount"))
                  .Sum();

                return total;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public bool AddCoin(ICoin coin)
        {
            bool coinAdded = false;
            try
            {
                xmlDoc.Element("Coins").Add(new XElement("Coin",
                    new XElement("Amount", coin.Amount),
                    new XElement("Volume", coin.Volume)
                ));
                xmlDoc.Save(HttpContext.Current.Server.MapPath("~/App_Data/Coins.xml"));

                coinAdded = true;
            }
            catch (Exception)
            {
                throw;
            }
            return coinAdded;
        }

        public void DeleteAll()
        {
            try
            {
                var coins = (from item in xmlDoc.Descendants("Coins").Descendants("Coin") select item).ToList();               
                coins.Remove();
                xmlDoc.Save(HttpContext.Current.Server.MapPath("~/App_Data/Coins.xml"));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public decimal GetRemainingVolume()
        {
            try
            {
                decimal volumeOccupied = xmlDoc.Descendants("Coins").Descendants("Coin")
                  .Select(x => (decimal)x.Element("Volume"))
                  .Sum();

                return 42 - volumeOccupied;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}