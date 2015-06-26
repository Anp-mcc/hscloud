using System.Linq;
using System.Web.Mvc;
using Entity;
using Raven.Client;
using WebStone.Mapper;
using WebStone.Models;

namespace WebStone.Controllers
{
    public partial class HomeController : Controller
    {
        private readonly IDocumentStore _documentStore;
        private readonly DeckMapper _deckMapper;

        public HomeController(IDocumentStore documentStore, DeckMapper deckMapper)
        {
            _documentStore = documentStore;
            _deckMapper = deckMapper;
        }

        public virtual ActionResult Index()
        {
            using (var session = _documentStore.OpenSession())
            {
                var decks = session.Query<Deck>().Select(_deckMapper.Map);
                return View(decks);
            }
        }
        public virtual ActionResult GetDeck()
        {
            var decks = new DeckViewModel();
           
            return View(decks);
        }
    }
}