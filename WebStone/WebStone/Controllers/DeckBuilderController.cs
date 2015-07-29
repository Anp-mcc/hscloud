using System.Web.Mvc;
using WebStone.Models;

namespace WebStone.Controllers
{
    public class DeckBuilderController : Controller
    {
        private readonly CreateDeckViewModel _createDeckViewModel;

        public DeckBuilderController(CreateDeckViewModel createDeckViewModel)
        {
            _createDeckViewModel = createDeckViewModel;
        }

        public ActionResult Index()
        {
            return View(_createDeckViewModel);
        }
	}
}