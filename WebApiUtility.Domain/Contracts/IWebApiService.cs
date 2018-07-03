using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiUtility.Domain.Models.ValueObjects;

namespace WebApiUtility.Domain.Contracts
{
    public interface IWebApiService
    {
        /// <summary>
        /// Метод работы с запросом POST
        /// </summary>
        /// <param name="addressString">Адрес сервиса</param>
        /// <param name="findObject">Поиковый объект</param>
        /// <param name="skip">Значение для шейпинга данных указывающее количество пропускаемых элементов</param>
        /// <param name="take">Значение для шейпинга данных указывающее количество взятых из выборки элементов</param>
        /// <returns></returns>
        Task<dynamic> FindItemAsync(string addressString, FindObject findObject, int skip = 0, int take = 20);
        /// <summary>
        /// Метод работы с запросом PUT
        /// </summary>
        /// <param name="addressString">Адрес сервиса</param>
        /// <param name="objectId">Id объекта</param>
        /// <param name="attributes">Список атрибутов для обновления</param>
        /// <returns></returns>
        Task<dynamic> ChangeAttributeAsync(string addressString, string objectId, List<JObject> attributes);
    }
}