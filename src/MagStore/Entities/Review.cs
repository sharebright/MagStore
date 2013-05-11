namespace MagStore.Entities
{
    public class Review
    {
        public string Id { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public string DateTime { get; set; }
        public bool Authorised { get; set; }
    }
}
