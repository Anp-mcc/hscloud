using System.Web.Mvc;
using CQS;
using CQS.Core;
using CQS.Query;
using CQS.QueryHandler;

namespace WebStone.Controllers
{
    public partial class HomeController : Controller
    {
        private readonly IQueryDispatcher _dispatcher;

        public HomeController(IQueryDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public virtual ActionResult Index()
        {
            var result = _dispatcher.Dispatch<AllDeckQuery, AllDeckQueryResult>(new AllDeckQuery());
           
            return View(result.Decks);
            
        }

        public virtual ActionResult CardList(string deckId)
        {
            var result = 
                _dispatcher.Dispatch<DeckWithCardsQuery, DeckWithCardsQueryResult>(new DeckWithCardsQuery()
                {
                    Id = deckId
                });
            return View(result.CardNames);
        }
    }
}