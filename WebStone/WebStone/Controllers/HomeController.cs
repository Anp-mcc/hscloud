using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using Entity;
using Raven.Client;
using WebStone.Models;

namespace WebStone.Controllers
{
    public partial class HomeController : Controller
    {
        private readonly IDocumentStore _documentStore;

        public HomeController(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public virtual ActionResult Index()
        {
            using (var session = _documentStore.OpenSession())
            {
                var decks = session.Query<Deck>();
                return View(decks);
            }
        }
        public virtual ActionResult GetDeck()
        {
            var decks = new DisplayDeckModel();
           
            return View(decks);
        }
    }
}