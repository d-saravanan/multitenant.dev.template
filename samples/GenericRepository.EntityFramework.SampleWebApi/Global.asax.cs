using AutoMapper;
using MultiTenantRepositry.EF.Api.App_Start;
using System;
using System.Web.Http;

namespace MultiTenantRepositry.EF.Api
{
    public class Global : System.Web.HttpApplication
    {
        public static IMapper _mapper;

        protected void Application_Start(object sender, EventArgs e)
        {
            HttpConfiguration config = GlobalConfiguration.Configuration;
            RegisterRoutes(config);
            RegisterMappings();
            RegisterDependencies(config);
            config.Filters.Add(new Insights.ApiTimerAttribute());
        }

        public static void RegisterMappings()
        {
            _mapper = MapperConfig.RegisterMappings();
        }

        public static void RegisterRoutes(HttpConfiguration config)
        {
            RouteConfig.RegisterRoutes(config);
        }

        public static void RegisterDependencies(HttpConfiguration config)
        {
            DependencyRegistryConfig.RegisterDependencies(config, _mapper);
        }
    }
}