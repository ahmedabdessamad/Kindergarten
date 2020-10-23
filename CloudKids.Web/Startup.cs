using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CloudKids.Web.Startup))]
namespace CloudKids.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
