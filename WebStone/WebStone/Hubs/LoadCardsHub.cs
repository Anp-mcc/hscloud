using System.Linq;
using CQS.Core;
using CQS.Models;
using CQS.Query;
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

        public void LoadCards()
        {
            var result = _queryDispatcher.Dispatch<CardForClassQuery, CardsForClassQueryResult>(new CardForClassQuery());
            var cards = result.Cards.ToList();

            Clients.Caller.showCards(cards);
        }
    }
}