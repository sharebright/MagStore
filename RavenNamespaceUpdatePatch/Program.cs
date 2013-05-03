using System.Linq;
using MagStore.Entities;
using MagStore.Infrastructure.Interfaces;
using Raven.Abstractions.Data;
using Raven.Client.Document;
using Raven.Json.Linq;

namespace RavenNamespaceUpdatePatch
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var documentStore = new DocumentStore
                {
                    ApiKey = "b23f5154-30b0-48f8-b619-76ee77d0234d",
                    Url = "https://ec2-eu4.cloudbird.net/databases/c818ddc6-dc4b-4b57-a439-4329fff0e61b.rdbtest-mag"
                };
            documentStore.Initialize();

            var toUpdate = documentStore.DatabaseCommands.GetDocuments(0, 500);

            var types =
                typeof (Catalogue).Assembly.GetTypes()
                                  .Where(t => typeof (IRavenEntity).IsAssignableFrom(t) && t.IsClass);

            // if (type.FullName.Contains("IRavenEntity")) continue;
            foreach (var d in toUpdate)
            {
                string oldNamespace = "RavenDbMembership";
                string newNamespace = "MagStore";
                var clrType = d.Metadata["Raven-Clr-Type"]
                    .ToString()
                    .Replace(oldNamespace, newNamespace);

                var p = new PatchRequest
                {
                    Type = PatchCommandType.Modify,
                    Name = "@metadata",
                    Nested = new[]
                        {
                            new PatchRequest
                                {
                                    Type = PatchCommandType.Set,
                                    Name = "Raven-Clr-Type",
                                    Value = new RavenJValue(clrType)
                                },
                        }
                };

                documentStore.DatabaseCommands.Patch(d.Key, new[] {p});
            }
        }
    }
}