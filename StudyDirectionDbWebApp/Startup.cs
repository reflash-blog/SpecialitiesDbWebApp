using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudyDirectionDbWebApp.Startup))]
namespace StudyDirectionDbWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
