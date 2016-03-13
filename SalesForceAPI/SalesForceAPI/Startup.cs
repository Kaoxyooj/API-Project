using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SalesForceAPI.Startup))]
namespace SalesForceAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
