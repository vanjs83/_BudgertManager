using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_BudgetManager.Startup))]
namespace _BudgetManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
