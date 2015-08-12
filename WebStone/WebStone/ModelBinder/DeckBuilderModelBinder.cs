using System.Web.Mvc;
using WebStone.Domain;

namespace WebStone.ModelBinder
{
    public class DeckBuilderModelBinder : IModelBinder
    {
        private const string SessionKey = "DeckBuilder";


        public object BindModel(ControllerContext context, ModelBindingContext bindingContext)
        {
            var deckBuilder = (DeckBuilder)context.HttpContext.Session[SessionKey];

            if (deckBuilder == null)
            {
                deckBuilder = new DeckBuilder();
                context.HttpContext.Session[SessionKey] = deckBuilder;
            }

            return deckBuilder;
        }
    }
}