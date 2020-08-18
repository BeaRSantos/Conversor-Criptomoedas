using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Teste_Accenture_V1.Models;

namespace Teste_Accenture_V1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            var busca = client.GetAsync(new Uri($"{_configuration["URLAPI"]}/Moeda/GetMoedas")).GetAwaiter().GetResult();
            //var retorno = JsonSerializer.Deserialize<RetornoMoedas>(busca.Content.ReadAsStringAsync().Result);
            var retorno = JsonConvert.DeserializeObject<RetornoMoedas>(busca.Content.ReadAsStringAsync().Result);
            ViewData["Moedas"] = retorno.Resultados;
            return View();
        }

        public IActionResult ExibirCotacao([FromQuery]string id, string basename, string quotename)
        {
            HttpClient client = new HttpClient();
            var busca = client.GetAsync(new Uri($"{_configuration["URLAPI"]}/Moeda/GetCotacao?id=" + id + "&basename=" + basename + "&quotename=" + quotename)).GetAwaiter().GetResult();
            var cotacoes = busca.Content.ReadAsStringAsync().Result;
            if (string.IsNullOrEmpty(cotacoes)) 
            return PartialView("CotacaoView", new List<RetornoCotacao>());
            var retorno = System.Text.Json.JsonSerializer.Deserialize<List<RetornoCotacao>>(cotacoes);
            return PartialView("CotacaoView", retorno);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
