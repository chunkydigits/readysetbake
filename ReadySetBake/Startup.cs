using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ReadySetBake.Startup))]
namespace ReadySetBake
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
