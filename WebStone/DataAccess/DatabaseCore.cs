using System;
using Raven.Client;
using Raven.Client.Document;

namespace DataAccess
{
    public class DatabaseCore : IDatabaseCore, IDisposable
    {
        private readonly DocumentStore _documentStore;

        public DatabaseCore(DocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public IDocumentSession OpenSession()
        {
            var session = _documentStore.OpenSession();
            session.Advanced.MaxNumberOfRequestsPerSession = 100;

            return session;
        }

        public void Dispose()
        {
            _documentStore.Dispose();
        }
    }
}
