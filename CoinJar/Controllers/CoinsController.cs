using CoinJar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;

namespace CoinJar.Controllers
{
    public class CoinsController : ApiController
    {
        public static Models.CoinJar coinjar = new Models.CoinJar();
        CoinJarService coinJarService = new CoinJarService(coinjar);

        public XDocument xDocument;
        public CoinsController()
        {
                
        }
        public CoinsController(XDocument _xDocument)
        {
            this.xDocument = _xDocument;
        }

        // GET: api/Coins
        public decimal Get()
        {
            return coinJarService.iCoinJar.GetTotalAmount();
        }

        // POST: api/Coins
        public HttpResponseMessage Post([FromBody] Coin coin)
        {
            coinJarService.iCoinJar.AddCoin(coin);
            if (coinjar.coinAdded)
                return Request.CreateResponse<Coin>(HttpStatusCode.Created, coin);
            else
                return Request.CreateResponse<Coin>(HttpStatusCode.PreconditionFailed, coin);
        }

        // PUT: api/Coins/5
        public void Delete()
        {
            coinJarService.iCoinJar.Reset();
        }
    }
}
