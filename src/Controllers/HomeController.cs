using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using ASPSearch.Models;
using ASPSearch.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nest;
using Newtonsoft.Json;

namespace ASPSearch.Controllers
{
    public class HomeController : Controller
    {
        private readonly IElasticClient _elasticClient;
        private readonly ILogger<HomeController> _logger; 

        public HomeController(
            IElasticClient elasticClient,
            ILogger<HomeController> logger)
        {
            _elasticClient = elasticClient;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
           

            var path = Path.Combine(Environment.CurrentDirectory, "data", "washingtonElectricCarData_restructured.json");
            using var jsonFileReader = System.IO.File.OpenText(path);
            var cars = System.Text.Json.JsonSerializer.Deserialize<Cars[]>(jsonFileReader.ReadToEnd(),
                                   new JsonSerializerOptions
                                   {
                                       PropertyNameCaseInsensitive = true
                                   });

            foreach (var car in cars)
            {
                var existsResponse = await _elasticClient.DocumentExistsAsync<Cars>(car);

                // If the document already exists, we're going to update it; otherwise insert it
                // Note:  You may get existsResponse.IsValid = false for a number of issues
                // ranging from an actual server issue, to mismatches with indices (e.g. a
                // mismatch on the datatype of Id).
                if (existsResponse.IsValid && existsResponse.Exists)
                {
                    var updateResponse = await _elasticClient.UpdateAsync<Cars>(car, u => u.Doc(car));

                    if (!updateResponse.IsValid)
                    {
                        var errorMsg = "Problem updating document in Elasticsearch.";
                        _logger.LogError(updateResponse.OriginalException, errorMsg);
                        throw new Exception(errorMsg);
                    }
                }
                else
                {
                    var insertResponse = await _elasticClient.IndexDocumentAsync(car);

                    if (!insertResponse.IsValid)
                    {
                        var errorMsg = "Problem inserting document to Elasticsearch.";
                        _logger.LogError(insertResponse.OriginalException, errorMsg);
                        throw new Exception(errorMsg);
                    }
                }
            }

            var vm = new HomeViewModel
            {
                InsertedData = JsonConvert.SerializeObject(cars, Formatting.Indented)
            };

            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
