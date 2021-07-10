using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinJar.Models
{
    public interface ICoinJar
    {
        void AddCoin(ICoin coin);
        decimal GetTotalAmount();
        void Reset();
    }
    public interface ICoin
    {
        decimal Amount { get; set; }
        decimal Volume { get; set; }
    }
}
