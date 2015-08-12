using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CQS.Core;
using CQS.Query;
using CQS.QueryResult;
using Entity;
using Raven.Client.Linq;
using WebStone.Domain;
using WebStone.Mapper;
using WebStone.Models;
using WebStone.ViewModels;

namespace WebStone.Controllers
{
    public class DeckBuilderController : Controller
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public DeckBuilderController(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        public ActionResult Index(DeckBuilder<DeckViewModel> deckBuilder, string cardId = null)
        {
            var query = _queryDispatcher.Dispatch<CardForClassQuery, CardsForClassQueryResult>(new CardForClassQuery { SelectedHero = CardType.Priest, CurrentPage = 1, NumberOfCardsOnPage = 10});

            var cardViewModels = query.Cards.Select(x => x.Map());
            var cards = cardViewModels as IList<CardViewModel> ?? cardViewModels.ToList();
            var viewModel = new CreateDeckViewModel { Cards = cards };
            
            if(cardId != null)
                deckBuilder.Push(cardId);

            viewModel.Deck = deckBuilder.Build();

            return View(viewModel);
        }
	}
}