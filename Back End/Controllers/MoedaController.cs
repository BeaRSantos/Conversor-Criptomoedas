using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TesteAccenture.Controllers;

namespace TesteAccenture.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoedaController : ControllerBase
    {
        
        [HttpGet("GetMoedas")]
        public IActionResult GetMoedas()
        {
            HttpClient client = new HttpClient();
            var busca = client.GetAsync(new Uri("https://api.coinlore.net/api/tickers/?start=0&limit=5")).GetAwaiter().GetResult();
            var retorno = JsonSerializer.Deserialize<Root>(busca.Content.ReadAsStringAsync().Result);
            return Ok(new {Resultados = retorno.data});
        }

        [HttpGet("GetCotacao")]
        public IActionResult GetCotacao([FromQuery]string id, string basename, string quotename)
        {
            HttpClient client = new HttpClient();
            var busca = client.GetAsync(new Uri($"https://api.coinlore.net/api/coin/markets/?id={id}")).GetAwaiter().GetResult();
            var retorno = JsonSerializer.Deserialize<List<Quote>>(busca.Content.ReadAsStringAsync().Result);
            var cotacoes = new List<Quote>();
            foreach(var quote in retorno){
               if(quote._base==basename)
                    if(quote.quote==quotename)
                        cotacoes.Add(quote);
            }
            return Ok(cotacoes);

        }
         
    }

    
     class Datum    {
        public string id { get; set; } 
        public string symbol { get; set; } 
        public string name { get; set; } 
        public string nameid { get; set; } 
        public int rank { get; set; } 
        public string price_usd { get; set; } 
        public string percent_change_24h { get; set; } 
        public string percent_change_1h { get; set; } 
        public string percent_change_7d { get; set; } 
        public string price_btc { get; set; } 
        public string market_cap_usd { get; set; } 
        public double volume24 { get; set; } 
        public double volume24a { get; set; } 
        public string csupply { get; set; } 
        public string tsupply { get; set; } 
        public string msupply { get; set; } 
    }

     class Info    {
        public int coins_num { get; set; } 
        public int time { get; set; } 
    }

     class Root    {
        public List<Datum> data { get; set; } 
        public Info info { get; set; } 
    }

    public class Quote
    {
        public string name { get; set; }
        [System.Text.Json.Serialization.JsonPropertyName("base")]
        public string _base { get; set; }
        public string quote { get; set; }
        public float price { get; set; }
        public float price_usd { get; set; }
        public float volume { get; set; }
        public float volume_usd { get; set; }
        public int time { get; set; }
    }
}
