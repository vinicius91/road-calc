
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(RoadCalc.Startup))]
namespace RoadCalc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}