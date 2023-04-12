using System;
using System.Linq;
using System.Threading.Tasks;
using ASPSearch.Models;
using ASPSearch.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nest;

namespace ASPSearch.Controllers
{
    public class SearchController : Controller
    {
        private readonly IElasticClient _elasticClient;
        private readonly ILogger<SearchController> _logger;

        public SearchController(
            IElasticClient elasticClient,
            ILogger<SearchController> logger)
        {
            _elasticClient = elasticClient;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string q, int from = 0)
        {
            if (string.IsNullOrEmpty(q))
            {
                var noResultsVM = new SearchViewModel { Term = "[No Search]" };
                return View(noResultsVM);
            }

            // Good article if you want to read an overview of the types of
            // queries available: https://qbox.io/blog/elasticsearch-queries-match-phrase-match
            // Specific usage for MultiMatch: https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/multi-match-usage.html
            var response = await _elasticClient.SearchAsync<Cars>(s =>
                s.Query(sq =>
                    sq.MultiMatch(mm => mm
                        .Query(q)
                        .Fuzziness(Fuzziness.Auto)
                    )
                ).Size(40)
                .From(from) // Paging, could add var here to finish up
            );

            var vm = new SearchViewModel
            {
                Term = q,
                Page = from // dunno if this will work as I expect?
            };

            if (response.IsValid)
                vm.Results = response.Documents?.ToList();
            else
                _logger.LogError(response.OriginalException, "Problem searching Elasticsearch for term {0}", q);

            return View(vm);
        }
    }
}
