using System.Collections.Generic;
using System.Linq;
using CQS.Core;
using CQS.Query;
using CQS.QueryResult;
using WebStone.Mapper;
using WebStone.ViewModels;

namespace WebStone.Factories
{
    public class DeckViewModelFactory : IDeckFactory<DeckViewModel>
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public DeckViewModelFactory(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        public DeckViewModel Create(IEnumerable<string> cardIds)
        {
            var result = _queryDispatcher.Dispatch<CardsByIdsQuery, CardsByIdsQueryResult>(new CardsByIdsQuery {CardIds = cardIds});

            return new DeckViewModel {Cards = result.Cards.Select(x => x.Map()), Name = "Default"};
        }
    }
}