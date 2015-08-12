using System.Web.Mvc;
using Ninject;
using WebStone.Domain;
using WebStone.ViewModels;

namespace WebStone.ModelBinder
{
    public class DeckBuilderModelBinder : IModelBinder
    {
        private readonly IKernel _kernel;

        public DeckBuilderModelBinder(IKernel kernel)
        {
            _kernel = kernel;
        }

        private const string SessionKey = "DeckBuilder";

        public object BindModel(ControllerContext context, ModelBindingContext bindingContext)
        {
            var deckBuilder = (DeckBuilder<DeckViewModel>)context.HttpContext.Session[SessionKey];

            if (deckBuilder == null)
            {
                deckBuilder = _kernel.Get<DeckBuilder<DeckViewModel>>();
                context.HttpContext.Session[SessionKey] = deckBuilder;
            }

            return deckBuilder;
        }
    }
}