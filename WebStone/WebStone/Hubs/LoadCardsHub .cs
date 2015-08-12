using System.Linq;
using CQS.Core;
using CQS.Query;
using CQS.QueryResult;
using Entity;
using Microsoft.AspNet.SignalR;

namespace WebStone.Hubs
{
    public class LoadCardsHub : Hub
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public LoadCardsHub(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        public void LoadCards(CardType heroClass, int currentPage, int cardsPerPage)
        {
            var query = new CardForClassQuery
            {
                SelectedHero = heroClass,
                CurrentPage = currentPage,
                NumberOfCardsOnPage = cardsPerPage
            };

            var result = _queryDispatcher.Dispatch<CardForClassQuery, CardsForClassQueryResult>(query);

            var cards = result.Cards.ToList();

            Clients.Caller.showCards(cards);
        }
    }
}