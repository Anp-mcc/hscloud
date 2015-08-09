using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Ninject;
using WebStone.App_Start;
using WebStone.Infrastucture;

namespace WebStone
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            SetDependencyResolver();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        private void SetDependencyResolver()
        {
            var kernel = new StandardKernel();
            var ninjectDependencyResolver = new NinjectDependencyResolver(kernel);

            DependencyResolver.SetResolver(ninjectDependencyResolver);
           
            var hubActivator = new HubActivator(kernel);
            GlobalHost.DependencyResolver.Register(typeof(IHubActivator), () => hubActivator);
        }
    }
}
