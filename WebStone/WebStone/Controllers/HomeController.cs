using System.Linq;
using System.Web.Mvc;
using Entity;
using Raven.Client;

namespace WebStone.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDocumentStore _documentStore;

        public HomeController(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public ActionResult Index()
        {
            using (var session = _documentStore.OpenSession())
            {
                var decks = session.Query<Deck>();
                return View(decks);
            }
        }
    }
}