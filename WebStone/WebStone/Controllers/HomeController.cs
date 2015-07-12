using System.Web.Mvc;
using CQS;
using CQS.Core;
using CQS.Query;

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
            var result = _dispatcher.Dispatch<QueryAllDeck, QueryAllDeckResult>(new QueryAllDeck());
           
            return View(result.Decks);
            
        }

        public virtual ActionResult CardList(string deckName)
        {
            return View();
        }
    }
}