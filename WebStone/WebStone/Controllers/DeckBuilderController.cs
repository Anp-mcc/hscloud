using System.Web.Mvc;
using WebStone.Domain;
using WebStone.Models;

namespace WebStone.Controllers
{
    public class DeckBuilderController : Controller
    {
        public ActionResult Index(DeckBuilder deckBuilder)
        {
            var viewModel = new CreateDeckViewModel {};
            return View(viewModel);
        }
	}
}