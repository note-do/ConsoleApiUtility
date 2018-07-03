using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiUtility.Domain.Contracts
{
    public interface IConfigurationService
    {
        /// <summary>
        /// Метод получения конфигурации
        /// </summary>
        /// <returns></returns>
        Dictionary<string, string> GetConfiguration();
    }
}
