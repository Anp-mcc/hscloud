using System;
using System.Linq;
using CQS.Core;
using CQS.Mappers;
using CQS.Models;
using CQS.Query;
using DataAccess;
using Entity;

namespace CQS.QueryHandler
{
    public class CardForClassQueryHandler : IQueryHandler<CardForClassQuery, CardsForClassQueryResult>
    {
        private readonly IDatabaseCore _core;

        public CardForClassQueryHandler(IDatabaseCore core)
        {
            _core = core;
        }

        public CardsForClassQueryResult Retrieve(CardForClassQuery query)
        {
            if(query == null)
                throw new NullReferenceException();

            var queryResult = new CardsForClassQueryResult();
            using (var session = _core.OpenSession())
            {
                queryResult.Cards = session.Query<Card>().Take(9).ToList().Select(x => x.Map());
            }

            return queryResult;
        }
    }
}