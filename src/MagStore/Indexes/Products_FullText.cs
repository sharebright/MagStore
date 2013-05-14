using System.Linq;
using MagStore.Entities;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace MagStore.Indexes
{
    public class Products_FullText : AbstractIndexCreationTask<Product, Products_FullText.Result>
    {
        public Products_FullText()
        {
            Map = products => products.Select(product => new { Fields = new object[] { product.Tags, product.Name, product.Description } });

            Indexes.Add(x => x.Fields, FieldIndexing.Analyzed);
        }

        public class Result
        {
            public string Fields { get; set; }
        }
    }
}