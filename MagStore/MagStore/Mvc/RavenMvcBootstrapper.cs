using Raven.Client;
using Raven.Client.Document;

namespace MagStore.Mvc
{
    public class RavenMvcBootstrapper
    {
        public static IDocumentStore ConnectToRavenInstance(string url, string apiKey)
        {
            var documentStore = new DocumentStore
                {
                    Url = url,
                    ApiKey = apiKey
                };
            documentStore.Initialize();
            return documentStore;
        }
    }
}
