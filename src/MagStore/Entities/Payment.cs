using MagStore.Infrastructure.Interfaces;

namespace MagStore.Entities
{
    public class Payment : IRavenEntity
    {
        public string Id { get; set; }
    }
}
