using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebStone.Startup))]

namespace WebStone
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}