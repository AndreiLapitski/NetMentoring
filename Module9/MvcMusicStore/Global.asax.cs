using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using MvcMusicStore.Controllers;
using MvcMusicStore.Monitoring;
using NLog;
using PerformanceCounterHelper;

namespace MvcMusicStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly ILogger logger;

        public MvcApplication()
        {
            logger = LogManager.GetCurrentClassLogger();
        }

        protected void Application_Start()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(HomeController).Assembly);
            builder.Register(l => LogManager.GetLogger("ForControllers")).As<ILogger>();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            using (CounterHelper<PerformanceCounter> counterHelper = 
                PerformanceHelper.CreateCounterHelper<PerformanceCounter>("Test project"))
            {
                counterHelper.RawValue(PerformanceCounter.LogInCounter, 0);
            }
        }

        protected void Application_Error()
        {
            var e = Server.GetLastError();
            logger.Error(e.Message);
        }
    }
}
