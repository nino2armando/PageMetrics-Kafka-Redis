using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using PageMetrics.PersistentDataStore;
using ServiceStack.Redis;

namespace PageMetrics.API
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public IRedisClientsManager ClientManger;
        private const string RedisUri = "127.0.0.1:6379";

        protected void Application_Start()
        {
            ClientManger = new PooledRedisClientManager(RedisUri);

            ConfigureDependencyResolver(GlobalConfiguration.Configuration);

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void ConfigureDependencyResolver(HttpConfiguration configuration)
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            builder.RegisterType<PageRepository>().As<IPageRepository>().PropertiesAutowired().InstancePerApiRequest();
            builder.Register<IRedisClient>(c => ClientManger.GetClient()).InstancePerApiRequest();

            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(builder.Build());
        }

        protected void Application_OnEnd()
        {
            ClientManger.Dispose();
        }
    }
}