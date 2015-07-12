using Raven.Client;

namespace DataAccess
{
    public interface IDatabaseCore
    {
        IDocumentSession OpenSession();
    }
}