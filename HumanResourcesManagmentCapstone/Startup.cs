using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HumanResourcesManagmentCapstone.Startup))]
namespace HumanResourcesManagmentCapstone
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
