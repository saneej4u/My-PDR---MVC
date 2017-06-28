using Microsoft.Owin;
using NewPDR.Data;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewPDR.Web.Startup))]
namespace NewPDR.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        
           // Database.SetInitializer<YourContextType>(new CreateDatabaseIfNotExists());
            ConfigureAuth(app);

            app.MapSignalR();
        }
    }
}
