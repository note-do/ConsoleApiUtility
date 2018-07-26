using Autofac.Extras.NLog;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiUtility.Domain.Contracts;
using WebApiUtility.Domain.Models.ValueObjects;

namespace WebApiUtility.Application.Services
{
    /// <summary>
    /// Основоной класс обновления данных с помощью API
    /// </summary>
    public class UpdateApiService : IUpdate
    {
#warning Плохая архитектура - Update зависит от Search.
        private readonly IWebApiService webApiService;
        private readonly ISearch search;
        private readonly ILogger logger;

        /// <summary>
        /// Конструктор класса обновления данных
        /// </summary>
        /// <param name="webApiService">Сервис взаимодействия с API</param>
        /// <param name="search">Сервис поиска с помощью API</param>
        public UpdateApiService(IWebApiService webApiService, ISearch search,ILogger logger)
        {
            this.webApiService = webApiService;
            this.search = search;
            this.logger = logger;
        }

        /// <summary>
        /// Основной метод обновления данных по классу объекта
        /// </summary>
        /// <param name="classId">Id класса объектов для поиска</param>
        /// <param name="existedAttributeId">Значение которое необходимо изменить</param>
        /// <param name="newValue">Значение на которое необходимо изменить</param>
        /// <param name="filterTypes">Фильтр значения для поиска объекта</param>
        /// <param name="address">Относительный адрес API сервиса (опциональное значение)</param>
        /// <returns>Список объектов Result</returns>
        public async Task<List<UpdateResult>> UpdateAttributeAsync(string classId, string existedAttributeId, string newValue, FilterTypes filterTypes, string address = @"api/objects")
        {
            var existedObjects = await search.SearchObjectAsync(classId, existedAttributeId, filterTypes, SearchConditionType.Attribute, SearchOperatorType.Exists);

            List<UpdateResult> results = new List<UpdateResult>();

            foreach (var @object in existedObjects)
            {
                var sortedattributes = SortingAttributes(@object, existedAttributeId, newValue);
                results.AddRange(await SendRequestAsync(address, @object.ObjectId, sortedattributes));
            }
            return results;
        }

        /// <summary>
        /// Основной метод обновления данных по атрибуту
        /// </summary>
        /// <param name="existedAttributeId">Значение которое необходимо изменить</param>
        /// <param name="newValue">Значение на которое необходимо изменить</param>
        /// <param name="newVsearchConditionTypealue">Значение на которое необходимо изменить</param>
        /// <param name="searchOperatorType">Значение на которое необходимо изменить</param>
        /// <param name="address">Относительный адрес API сервиса (опциональное значение)</param>
        /// <returns>Список объектов Result</returns>
        public async Task<List<UpdateResult>> UpdateAttributeAsync(string existedAttributeId, string newValue, SearchConditionType searchConditionType, SearchOperatorType searchOperatorType, string address = @"api/objects")
        {
            var existedObjects = await search.SearchObjectAsync(existedAttributeId, searchConditionType, searchOperatorType, address);

            List<UpdateResult> results = new List<UpdateResult>();

            foreach (var @object in existedObjects)
            {
                var sortedattributes = SortingAttributes(@object, existedAttributeId, newValue);
                results.AddRange(await SendRequestAsync(address, @object.ObjectId, sortedattributes));
            }
            return results;
        }

        /// <summary>
        /// Метод инкапсуляции работы с API
        /// </summary>
        /// <param name="address">Относительный адрес API сервиса</param>
        /// <param name="objectId">Id класса объекта</param>
        /// <param name="updatedAttributes">Список атрибутов для изменения</param>
        /// <returns></returns>
        private async Task<List<UpdateResult>> SendRequestAsync(string address, string objectId, List<JObject> updatedAttributes)
        {
            var json = await webApiService.ChangeAttributeAsync(address, objectId, updatedAttributes);
            var result = GetUpdateResultAsync(json);
            return result;

        }
#warning oldValue некорректный комментарий. Это id атрибута
#warning Спагетти код SortAttribute
        /// <summary>
        /// Обслуживающий метод сортировки атрибутов
        /// </summary>
        /// <param name="object">Объект результата поиска</param>
        /// <param name="oldValue">Значение которое необходимо заменить</param>
        /// <param name="newValue">Значение которым будет производится замена</param>
        /// <returns></returns>
        private List<JObject> SortingAttributes(Result @object, string oldValue, string newValue)
        {
            List<JObject> updatedAttributes = new List<JObject>();
            foreach (var attribute in @object.ObjectAttributes)
            {
                var attributeValue = SortAttribute(attribute, oldValue, newValue);

                if (attributeValue != null)
                {
                    updatedAttributes.Add(attributeValue);
                }
                logger.Error($"{nameof(attributeValue)} значение равно null");
            }
            return updatedAttributes;
        }

        /// <summary>
        /// Обслуживающий метод сортировки конкретного атрибута
        /// </summary>
        /// <param name="attribute">Объект атрибута</param>
        /// <param name="oldValue">Значение которое необходимо заменить</param>
        /// <param name="newValue">Значение которым будет производится замена</param>
        /// <returns></returns>
        private JObject SortAttribute(Attribute attribute, string oldValue, string newValue)
        {
            if (attribute.Id.ToLower() == oldValue.ToLower() && attribute.AttributeType != 8)
            {
                return SetAttribute(attribute, newValue);
            }
            if (attribute.Id.ToLower() == oldValue.ToLower() && attribute.AttributeType == 8)
            {
                return SetAttributeObject(attribute, newValue);
            }
            return null;
        }

        /// <summary>
        /// Метод обработки результата запроса
        /// </summary>
        /// <param name="json">Json объект для обработки</param>
        /// <returns></returns>
        private List<UpdateResult> GetUpdateResultAsync(dynamic json)
        {
            var result = new List<UpdateResult>();

            foreach (var item in json)
            {
                result.Add(new UpdateResult
                {
                    Id = item["id"]
                });
            }
            return result;
        }

        /// <summary>
        /// Закрытый метод установки значения "Ссылка на объект" аттрибутов
        /// </summary>
        /// <param name="attribute">Текущий атрибут </param>
        /// <param name="upadtedValue">Значение на которое проиводится замена</param>
        /// <returns>Обновленный атрибут</returns>
        private JObject SetAttributeObject(Attribute attribute, string upadtedValue)
        {
#warning Недостаточный критерий для нахождения нужного объекта
            var searchValue = search.SearchObjectAsync(SearchConditionType.Name, SearchOperatorType.Equals,upadtedValue).Result.FirstOrDefault();
#warning Если поиск не вернул результаты будет ошибка
            var value = new JObject();
            value.Add("Id", searchValue.ObjectId);
            value.Add("Name", searchValue.ObjectName);

            var updatedAttribute = new JObject();
            updatedAttribute.Add("Id", attribute.Id);
            updatedAttribute.Add("Name", "forvalidation");
            updatedAttribute.Add("Type", attribute.AttributeType);
            updatedAttribute.Add("Value", value);

            return updatedAttribute;
        }
        /// <summary>
        /// Закрытый метод установки значения в случае обновления атрибута
        /// </summary>
        /// <param name="attribute">Текущий атрибут</param>
        /// <param name="newValue">Значение на которое проиводится замена</param>
        /// <returns>Обновленный атрибут</returns>
        private JObject SetAttribute(Attribute attribute, string newValue)
        {
            var updatedAttribute = new JObject();
            updatedAttribute.Add("Id", attribute.Id);
            updatedAttribute.Add("Name", "forvalidation");
            updatedAttribute.Add("Type", attribute.AttributeType);
            updatedAttribute.Add("Value", newValue);
            return updatedAttribute;
        }
    }
}
