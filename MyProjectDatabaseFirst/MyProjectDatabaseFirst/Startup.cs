using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyProjectDatabaseFirst.Startup))]
namespace MyProjectDatabaseFirst
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
