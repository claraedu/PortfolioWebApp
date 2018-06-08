using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PortfolioViewer.Startup))]
namespace PortfolioViewer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
