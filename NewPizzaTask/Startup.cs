using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewPizzaTask.Startup))]
namespace NewPizzaTask
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
