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
         System.Data.Entity.Database.SetInitializer(new SampleData());
            ConfigureAuth(app);

            app.MapSignalR();
        }
    }
}
