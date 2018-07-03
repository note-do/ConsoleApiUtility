using Autofac;
using System.Configuration;
using WebApiConsoleClient.Configuration;
using WebApiConsoleClient.Contracts;
using WebApiConsoleClient.LaguageSupport;
using WebApiUtility.Application.Services;
using WebApiUtility.Domain.Contracts;

namespace WebApiConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<Autofac.Extras.NLog.NLogModule>();
            builder.RegisterType<ApplicationStart>().As<IApplication>();
            builder.RegisterType<ConfigurationService>().As<IConfigurationService>();
            builder.RegisterType<CommandService>().As<ICommandService>();
            builder.RegisterType<UpdateApiService>().As<IUpdate>();
            builder.RegisterType<SearchApiService>().As<ISearch>();
            builder.RegisterType<AccessApiService>().As<IAccessApiService>().WithParameter("serverAddress", ConfigurationManager.AppSettings["Address"]);
            builder.RegisterType<ApiClientService>().As<IWebApiService>();

            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IApplication>();
                app.Start();
            }
        }
    }
}
