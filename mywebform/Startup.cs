using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(mywebform.Startup))]
namespace mywebform
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
