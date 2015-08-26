using System;
using System.Linq;
using CQS.Core;
using CQS.Mappers;
using CQS.Query;
using CQS.QueryResult;
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
                
                queryResult.Cards = session.Query<Card>().
                    Where(x => x.PlayerClass == query.SelectedHero || x.Type == CardType.Spell).
                    Skip(query.NumberOfCardsOnPage * (query.CurrentPage - 1)).Take(query.NumberOfCardsOnPage).
                    ToList().
                    Select(x => x.Map());
            }
            return queryResult;
        }
    }
}