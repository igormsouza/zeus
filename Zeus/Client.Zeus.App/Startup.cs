using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BHS.ProjetoBaseMvc.App.Startup))]
namespace BHS.ProjetoBaseMvc.App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
