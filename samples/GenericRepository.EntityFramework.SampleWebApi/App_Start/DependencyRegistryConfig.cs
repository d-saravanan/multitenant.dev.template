using Autofac;
using Autofac.Extras.DynamicProxy;
using Autofac.Integration.WebApi;
using AutoMapper;
using MultiTenancy.Core.ProviderContracts;
using MultiTenancy.Core.Providers;
using MultiTenantRepository;
using MultiTenantRepository.EntityFramework;
using MultiTenantRepositry.EF.Api.Intercetors;
using MultiTenantRepositry.EF.Api.ShardMaps;
using MultiTenantRepositry.EF.Core;
using MultiTenantRepositry.EF.Core.Entities;
using MultiTenantRepositry.EF.Core.Services;
using MultiTenantServices.ServiceLogger;
using System.Reflection;
using System.Web.Http;

namespace MultiTenantRepositry.EF.Api.App_Start
{
    public class DependencyRegistryConfig
    {
        public static void RegisterDependencies(HttpConfiguration config, IMapper mapper)
        {
            var builder = new ContainerBuilder();

            // Register Cross cutting concerns. Make this first always 
            RegisterXCuttingConcerns(builder);

            // Register REST Api Controllers
            RegisterApiControllers(builder);

            // Register IEntitiesContext
            RegisterContexts(builder);

            // Register repositories
            RegisterRepositories(builder);

            // Register Services
            RegisterServices(builder);

            // Register the interceptors
            RegisterInterceptors(builder);

            // Register IMappingEngine
            RegisterMappers(mapper, builder);

            config.DependencyResolver = new AutofacWebApiDependencyResolver(builder.Build());
        }

        private static void RegisterInterceptors(ContainerBuilder builder)
        {
            //builder.RegisterType<MethodInterceptors>().As<Castle.DynamicProxy.IInterceptor>().InstancePerLifetimeScope();
            builder.Register(_ => new MethodInterceptors(new PerfLogger()));
        }

        private static void RegisterXCuttingConcerns(ContainerBuilder builder)
        {
            builder.RegisterType<PerfLogger>().As<ILogger>();
        }

        private static void RegisterApiControllers(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
        }

        private static void RegisterMappers(IMapper mapper, ContainerBuilder builder)
        {
            builder.Register(_ => mapper).As<IMapper>().SingleInstance();
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<CountryService>()
                            .As<MultiTenantServices.Services.MultiTenantServices<Country, int>>().InstancePerRequest()
                            .EnableClassInterceptors()
                            .InterceptedBy(typeof(MethodInterceptors));

            builder.RegisterType<ClaimsContextDataProvider>().As<IUserContextDataProvider>();
        }

        private static void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterType<MultiTenantRepository<Country>>()
                               .As<IMultiTenantRepository<Country, int>>().InstancePerRequest();
            builder.RegisterType<MultiTenantRepository<Resort>>()
                   .As<IMultiTenantRepository<Resort,int>>().InstancePerRequest();
            builder.RegisterType<MultiTenantRepository<Hotel>>()
                   .As<IMultiTenantRepository<Hotel, int>>().InstancePerRequest();
        }

        private static void RegisterContexts(ContainerBuilder builder)
        {
            builder.Register(_ => new AccommodationEntities(new ClaimsContextDataProvider(), new ConfigShardMapResolver()))
                               .As<IEntitiesContext>()
                               .InstancePerRequest();
        }
    }
}