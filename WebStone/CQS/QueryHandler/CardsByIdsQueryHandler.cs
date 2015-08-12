using System;
using System.Linq;
using CQS.Core;
using CQS.Mappers;
using CQS.Query;
using CQS.QueryResult;
using DataAccess;
using Entity;
using Raven.Client.Linq;

namespace CQS.QueryHandler
{
    public class CardsByIdsQueryHandler : IQueryHandler<CardsByIdsQuery, CardsByIdsQueryResult>
    {
        private readonly IDatabaseCore _core;

        public CardsByIdsQueryHandler(IDatabaseCore core)
        {
            _core = core;
        }

        public CardsByIdsQueryResult Retrieve(CardsByIdsQuery query)
        {
            if(query == null)
                throw new ArgumentNullException("query");

            var queryResult = new CardsByIdsQueryResult();

            using (var session = _core.OpenSession())
            {
                var cards = session.Query<Card>().Where(x => x.Id.In(query.CardIds));
                queryResult.Cards = cards.ToList().Select(x => x.Map());
            }

            return queryResult;
        }
    }
}