using Raven.Client;
using Raven.Client.Document;

namespace MagStore.Mvc
{
    internal class RavenMvcBootstrapper
    {
        public static IDocumentStore ConnectToRavenInstance(string url, string apiKey)
        {
            return new DocumentStore
                {
                    Url = url,
                    ApiKey = apiKey
                };
        }
    }
}
