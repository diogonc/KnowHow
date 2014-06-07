using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KnowHow.Startup))]
namespace KnowHow
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
