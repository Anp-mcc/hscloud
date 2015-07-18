using Raven.Client;

namespace DataAccess
{
    public class DatabaseCore : IDatabaseCore
    {
        private readonly IDocumentStore _documentStore;

        public DatabaseCore(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public IDocumentSession OpenSession()
        {
            var session = _documentStore.OpenSession();
            session.Advanced.MaxNumberOfRequestsPerSession = 100;

            return session;
        }
    }
}
