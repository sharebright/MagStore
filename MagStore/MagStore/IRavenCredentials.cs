namespace MagStore
{
    public interface IRavenCredentials
    {
        string Url { get; set; }
        string ApiKey { get; set; }
    }
}