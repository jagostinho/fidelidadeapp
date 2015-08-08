using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClubeUniversitario.Startup))]
namespace ClubeUniversitario
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
