using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MVC.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public HttpClient _client;
        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _client = _httpClientFactory.CreateClient("Doge");
        }

        public IActionResult Index()
        {
            List<string> urls = new List<string>();
            // HttpClient client = new HttpClient();
            // client.DefaultRequestHeaders.Accept.Add(
            // new MediaTypeWithQualityHeaderValue("application/json"));
            // var client = _httpClientFactory.CreateClient("Doge");
            HttpResponseMessage response = _client.GetAsync("breeds/image/random/20").Result;
            if (response.IsSuccessStatusCode)
            {

                var dataObjects = response.Content.ReadAsStringAsync();
                var data = dataObjects.Result;
                dynamic res = JsonConvert.DeserializeObject(data);
                var fin = res.message;
                foreach (var s in fin)
                {
                    urls.Add(s.ToString());
                }
            }
            return View(urls);
        }

        public IActionResult Breed(string breed)
        {
            List<string> urls = new List<string>();
            List<string> breedings = new List<string>();
            // HttpClient client = new HttpClient();
            // client.DefaultRequestHeaders.Accept.Add(
            // new MediaTypeWithQualityHeaderValue("application/json"));
            //var client = _httpClientFactory.CreateClient("Doge");
            HttpResponseMessage breedList = _client.GetAsync("breeds/list/all").Result;
            var breedObjects = breedList.Content.ReadAsStringAsync();
            var breedData = breedObjects.Result;
            dynamic breedRes = JsonConvert.DeserializeObject(breedData);
            var breedInd = breedRes?.message;
            foreach (var s in breedInd)
            {
                breedings.Add(s.ToString().Split(":")[0]);
            }
            ViewData["Breeds"] = breedings;

            if (!(breed is null))
            {
                HttpResponseMessage response = _client.GetAsync($"breed/{breed.ToLower()}/images").Result;
                if (response.IsSuccessStatusCode)
                {

                    var dataObjects = response.Content.ReadAsStringAsync();
                    var data = dataObjects.Result;
                    dynamic res = JsonConvert.DeserializeObject(data);
                    var fin = res?.message;
                    foreach (var s in fin)
                    {
                        urls.Add(s.ToString());
                    }
                }
            }

            return View(urls);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}