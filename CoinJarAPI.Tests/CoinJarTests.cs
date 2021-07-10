using CoinJar.Controllers;
using System;
using Xunit;
using FakeItEasy;

namespace CoinJarAPI.Tests
{
    public class CoinJarTests
    {
        [Fact]
        public void GetTotalAmount_Returns_The_Correct_Amount_Of_Coins()
        {
            decimal total = 100;
            //A.CallTo(() => ) 
            var controller = new CoinsController();

            var result = controller.Get();
        }
    }
}
