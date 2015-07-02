using System.Web.Mvc;
using Entity;

namespace WebStone.HtmlHelpers
{
    //TODO insert image icons instead of this
    public static class DeckHelpers
    {
        public static MvcHtmlString HeroClassToIcon(this HtmlHelper html, HeroClass heroClass)
        {
            var tag = new TagBuilder("i") {InnerHtml = heroClass.ToString()};
            return MvcHtmlString.Create(tag.ToString());
        }
    }
}