using Nest;
using Elasticsearch.Net;

namespace ASPSearch.Models
{
    [ElasticsearchType(RelationName = "book", IdProperty = nameof(Id))]
    public class Cars
    {
        public int Id { get; set; }

        public string Location { get; set; }

        public string Zip { get; set; }

        public string Year { get; set; }

        public string Maker { get; set; }

        public string Model { get; set; }

        public string Type { get; set; }

        public string Eligibility { get; set; }
    }
}
