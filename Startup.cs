using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Project_497.Startup))]
namespace Project_497
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
