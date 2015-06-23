using System.Collections.Generic;
using System.Web.Mvc;
using WebStone.Models;

namespace WebStone.Controllers
{
    public partial class HomeController : Controller
    {
        public virtual ActionResult Index()
        {
            return View();
        }
        public virtual ActionResult GetDeck()
        {
            var decks = new DisplayDeckModel();
           
            return View(decks);
        }
    }
}