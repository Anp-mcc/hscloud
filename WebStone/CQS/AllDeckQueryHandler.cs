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
    public class AllDeckQueryHandler : IQueryHandler<AllDeckQuery, AllDeckQueryResult>
    {
        private readonly IDatabaseCore _core;

        public AllDeckQueryHandler(IDatabaseCore core)
        {
            _core = core;
        }

        public AllDeckQueryResult Retrieve(AllDeckQuery query)
        {
            if (query == null)
                throw new ArgumentException("Cannot execute empty query");

            using (var session = _core.OpenSession())
            {
                var decksViewModel = session.Query<Deck>().ToList().Select(x => x.Map());
                return new AllDeckQueryResult { Decks = decksViewModel };
            }
        }
    }

    public class AllDeckQueryResult : IQueryResult
    {
        public IEnumerable<DeckViewModel> Decks { get; set; }
    }
}
