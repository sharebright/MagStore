namespace MagStore
{
    public class RavenCredentials:IRavenCredentials
    {
        public string Url { get; set; }
        public string ApiKey { get; set; }
    }
}