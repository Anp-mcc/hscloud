using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Ninject;
using WebStone.App_Start;
using WebStone.Domain;
using WebStone.Infrastucture;
using WebStone.ModelBinder;
using WebStone.ViewModels;

namespace WebStone
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var kernel = new StandardKernel();

            AreaRegistration.RegisterAllAreas();

            SetDependencyResolver(kernel);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(DeckBuilder<DeckViewModel>), new DeckBuilderModelBinder(kernel));
        }

        private void SetDependencyResolver(IKernel kernel)
        {
            var ninjectDependencyResolver = new NinjectDependencyResolver(kernel);

            DependencyResolver.SetResolver(ninjectDependencyResolver);
           
            var hubActivator = new HubActivator(kernel);
            GlobalHost.DependencyResolver.Register(typeof(IHubActivator), () => hubActivator);
        }
    }
}
