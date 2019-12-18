using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BazarMVC.Startup))]
namespace BazarMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
