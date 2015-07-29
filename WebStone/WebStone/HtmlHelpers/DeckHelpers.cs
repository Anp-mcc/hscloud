using System.Web.Mvc;
using Entity;

namespace WebStone.HtmlHelpers
{
    //TODO make some resource manager to provide names for resources
    public static class DeckHelpers
    {
        public static MvcHtmlString HeroClassToIcon(this HtmlHelper html, HeroClass heroClass)
        {
            var builder = new TagBuilder("img");

            builder.MergeAttribute("src", string.Format("/Content/Image/Icon/Icon_{0}_64.png", heroClass));

            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString Image(this HtmlHelper helper, string heroName)
        {
            var builder = new TagBuilder("img");

            builder.MergeAttribute("src", string.Format("/Content/Image/Icon/Icon_{0}_64.png", heroName));

            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }

    }
}