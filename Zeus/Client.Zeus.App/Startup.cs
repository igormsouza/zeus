using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Client.Zeus.App.Startup))]
namespace Client.Zeus.App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
