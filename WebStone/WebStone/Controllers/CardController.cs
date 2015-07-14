using System.Web.Mvc;
using CQS.Core;
using CQS.Query;
using CQS.QueryHandler;

namespace WebStone.Controllers
{
    public class CardController : Controller
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public CardController(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        public ActionResult Index(int page = 1)
        {
            var queryResult = _queryDispatcher.Dispatch<AllCardsQuery, AllCardsQueryResult>(new AllCardsQuery { CurrentPage = page});
            return View(queryResult);
        }
	}
}