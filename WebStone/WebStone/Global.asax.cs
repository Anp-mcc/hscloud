using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebStone.App_Start;
using WebStone.Infrastucture;

namespace WebStone
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            DependencyResolver.SetResolver(new NinjectDependencyResolver());

            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
