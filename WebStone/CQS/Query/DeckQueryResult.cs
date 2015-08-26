using CQS.Core;
using Entity;

namespace CQS.Query
{
    public class DeckQueryResult : IQuery
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public PlayerClass Hero { get; set; }
    }
}