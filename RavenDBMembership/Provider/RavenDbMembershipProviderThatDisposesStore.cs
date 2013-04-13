using System;

namespace RavenDBMembership.Provider
{
    public class RavenDbMembershipProviderThatDisposesStore : Provider.RavenDbMembershipProvider, IDisposable
    {
        public void Dispose()
        {
            if (DocumentStore != null)
                DocumentStore.Dispose();

            DocumentStore = null;
        }
    }
}