using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using MultiTenantRepositry.EF.Core.Authorization;
using System.Web.Http;

[assembly: OwinStartup(typeof(MultiTenantRepositry.EF.Api.Startup))]
namespace MultiTenantRepositry.EF.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use(typeof(CustomAuthenticationMiddleware));
            app.UseWebApi(GlobalConfiguration.Configuration);
        }
    }
}
