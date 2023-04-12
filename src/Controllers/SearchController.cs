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
        public async Task<IActionResult> Index(string q, int page = 1, int pageSize = 30)
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
                ).Size(pageSize)
                .From((page - 1) * pageSize) // Paging, could add var here to finish up
            );
            
      
            var vm = new SearchViewModel
            {
                Term = q,
                NextPage = page,
                PageSize = pageSize
            };
            if (page > 1)
            {
               vm.PrevPage = page - 1;
            }

            if (response.IsValid)
            {
                if (response.Total > page * pageSize)
                {
                    vm.NextPage = page + 1;
                }
                else
                {
                    vm.NextPage = -1; //there is probably a better way to do this but meh
                }
                vm.Results = response.Documents?.ToList();
            }
            else
                _logger.LogError(response.OriginalException, "Problem searching Elasticsearch for term {0}", q);

            return View(vm);
        }
    }
}
