using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoinJar.Models
{
    public class Coin : ICoin
    {
        public decimal Amount { get; set; } = 0;
        public decimal Volume { get; set; } = 0;
    }
}