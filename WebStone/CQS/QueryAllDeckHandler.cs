using System;
using System.Collections.Generic;
using System.Linq;
using CQS.Core;
using CQS.Mappers;
using CQS.Models;
using CQS.Query;
using DataAccess;
using Entity;

namespace CQS
{
    public class QueryAllDeckHandler : IQueryHandler<QueryAllDeck, QueryAllDeckResult>
    {
        private readonly IDatabaseCore _core;

        public QueryAllDeckHandler(IDatabaseCore core)
        {
            _core = core;
        }

        public QueryAllDeckResult Retrieve(QueryAllDeck query)
        {
            if (query == null)
                throw new ArgumentException("Cannot execute empty query");

            using (var session = _core.OpenSession())
            {
                var decksViewModel = session.Query<Deck>().ToList().Select(x => x.Map());
                return new QueryAllDeckResult { Decks = decksViewModel };
            }
        }
    }

    public class QueryAllDeckResult : IQueryResult
    {
        public IEnumerable<DeckViewModel> Decks { get; set; }
    }
}
