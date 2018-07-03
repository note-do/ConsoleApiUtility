using Autofac.Extras.NLog;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using WebApiUtility.Domain.Contracts;

namespace WebApiConsoleClient.Configuration
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly ILogger logger;
        private readonly NameValueCollection configurationCollection;

        public ConfigurationService(ILogger logger)
        {
            this.logger = logger;
            configurationCollection = ConfigurationManager.AppSettings;

        }

        /// <summary>
        /// Метод получения конфигурация для получения параметров из app.config
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetConfiguration()
        {
            Dictionary<string, string> configuration = new Dictionary<string, string>();

            foreach (var item in configurationCollection.AllKeys)
            {
                configuration.Add(item, configurationCollection[item]);
            }

            return configuration;
        }
    }
}
