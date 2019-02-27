using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LinkedIn_Test.Startup))]
namespace LinkedIn_Test
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
