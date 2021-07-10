using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoinJar.Models
{
    public class CoinJar : ICoinJar
    {
        XMLHelper xmlHelper = new XMLHelper();
        public bool coinAdded = false;
        public void AddCoin(ICoin coin)
        {
            if (coin.Volume <= xmlHelper.GetRemainingVolume())
                coinAdded = xmlHelper.AddCoin(coin);
            else
                coinAdded = false;
        }

        public decimal GetTotalAmount()
        {
            return xmlHelper.TotalAmount();
        }

        public void Reset()
        {
            xmlHelper.DeleteAll();
        }
    }

    public class CoinJarService
    {
        public ICoinJar iCoinJar;
        public CoinJarService(ICoinJar _iCoinJar)
        {
            iCoinJar = _iCoinJar;
        }
    }
}