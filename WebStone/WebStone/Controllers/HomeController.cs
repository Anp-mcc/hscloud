using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Entity;
using Raven.Client;
using WebStone.Mapper;

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
                var decks = session.Query<Deck>().Select(x => x.Map());
                return View(decks);
            }
        }

        public virtual ActionResult CardList(string deckName)
        {
            return View();
        }
    }
}