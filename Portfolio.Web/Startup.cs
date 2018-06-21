using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Portfolio.Web.Startup))]
namespace Portfolio.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
