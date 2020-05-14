using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ScannerApp.Startup))]
namespace ScannerApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
