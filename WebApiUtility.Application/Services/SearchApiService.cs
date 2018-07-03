using Autofac.Extras.NLog;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiUtility.Domain.Contracts;
using WebApiUtility.Domain.Models.ValueObjects;

namespace WebApiUtility.Application.Services
{
    /// <summary>
    /// Основоной класс поиска с помощью API
    /// </summary>
    public class SearchApiService : ISearch
    {
        private readonly IWebApiService webApiService;

        /// <summary>
        /// Конструктор класса поиска
        /// </summary>
        /// <param name="webApiService">Сервис взаимодействия с API</param>
        public SearchApiService(IWebApiService webApiService,ILogger logger)
        {
            this.webApiService = webApiService;
        }

        /// <summary>
        /// Метод поиска объекта по атрибуту в определнной группе объектов
        /// </summary>
        /// <param name="objectId">Id класса объекта для поиска</param>
        /// <param name="attibuteId">значение для поиска</param>
        /// <param name="filterTypes">Фильтр типа(по родительскому каталогу или по Id класса объекта)</param>
        /// <param name="searchCondition">Параметр условия поиска, значение Enum указывает по какому критерию будет поиск</param>
        /// <param name="searchOperatorType">Оператор операции поиска</param>
        /// <param name="address">Относительный адрес API сервиса (опциональное значение)</param>
        /// <returns>Список объектов Result</returns>
        public async Task<List<Result>> SearchObjectAsync(string objectId, string attibuteId, FilterTypes filterTypes, SearchConditionType searchCondition, SearchOperatorType searchOperatorType, string address = @"api/objects/search")
        {

            var findObject = MakeFindObject(objectId, attibuteId, filterTypes, searchCondition, searchOperatorType);

            var results = await Request(findObject, address);

            return results;
        }

        /// <summary>
        /// Метод поиска по атрибуту объекта
        /// </summary>
        /// <param name="attributeId">Класс объекта для поиска</param>
        /// <param name="searchCondition">Параметр условия поиска, значение Enum указывает по какому критерию будет поиск</param>
        /// <param name="filterTypes">Тип фильтра для уточнения условий поиска, по умолчанию поиск по Классу объекта</param>
        /// <param name="address">Относительный адрес API сервиса (опциональное значение)</param>
        /// <returns>Список объектов Result</returns>
        public async Task<List<Result>> SearchObjectAsync(string attributeId, SearchConditionType searchConditionType, SearchOperatorType searchOperatorType, string address = @"api/objects/search")
        {
            var findObject = MakeFindObject(attributeId, searchConditionType, searchOperatorType);

            var results = await Request(findObject, address);

            return results;
        }


        /// <summary>
        ///  Метод поиска по классу объекта
        /// </summary>
        /// <param name="objectId">Id класса объекта для поиска</param>
        /// <param name="filterTypes">Фильтр типа(по родительскому каталогу или по Id класса объекта)</param>
        /// <param name="address">Относительный адрес API сервиса (опциональное значение)</param>
        /// <returns></returns>
        public async Task<List<Result>> SearchObjectAsync(string objectId, FilterTypes filterTypes, string address = @"api/objects/search")
        {
            var findObject = MakeFindObject(objectId,filterTypes);

            var results = await Request(findObject, address);

            return results;
        }

        /// <summary>
        /// Метод поиска по имени объекта
        /// </summary>
        /// <param name="searchConditionType">Параметр условия поиска, значение Enum указывает по какому критерию будет поиск</param>
        /// <param name="searchOperatorType">Оператор операции поиска</param>
        /// <param name="name">Имя объекта</param>
        /// <param name="address">Относительный адрес API сервиса (опциональное значение)</param>
        /// <returns></returns>
        public async Task<List<Result>> SearchObjectAsync(SearchConditionType searchConditionType, SearchOperatorType searchOperatorType, string name, string address = @"api/objects/search")
        { 
            var findObject = MakeFindObject(searchConditionType, searchOperatorType, name);

            var results = await Request(findObject, address);

            return results;
        }



        /// <summary>
        /// Закрытый метод создания объекта поиска по аттрибуту в определнной группе объектов
        /// </summary>
        /// <param name="objecteId">Id объекта для фильтра значения</param>
        /// <param name="attributeId">значение для поиска</param>
        /// <param name="filterTypes">Фильтр типа(по родительскому каталогу или по Id класса объекта)</param>
        /// <param name="searchConditionType">Параметр условия поиска, значение Enum указывает по какому критерию будет поиск</param>
        /// <param name="searchOperatorType">Оператор операции поиска</param>
        /// <returns>Список объектов FindObject</returns>
        private FindObject MakeFindObject(string objectId, string attributeId, FilterTypes filterTypes, SearchConditionType searchConditionType, SearchOperatorType searchOperatorType)
        {
            var newFindObject = new FindObject();

            var findObject = new FindObject
            {
                Filters = new Filter[]
                {
                        new Filter(){
                            Type = (int)filterTypes,
                            Value = objectId
                        }
                },

                Conditions = new Condition[]
                {
                        new Condition {
                        Type = (int?)searchConditionType,
                        Operator = (int?)searchOperatorType,
                        Attribute = attributeId
                        }
                }
            };
            return findObject;
        }

        /// <summary>
        /// Закрытый метод создания объекта поиска по атрибуту объекта
        /// </summary>
        /// <param name="attributeId">значение для поиска</param>
        /// <param name="searchConditionType">Параметр условия поиска, значение Enum указывает по какому критерию будет поиск</param>
        /// <param name="searchOperatorType">Оператор операции поиска</param>
        /// <returns>Список объектов FindObject</returns>
        private FindObject MakeFindObject(string attributeId, SearchConditionType searchConditionType, SearchOperatorType searchOperatorType)
        {
            var searchOperatorTypeInt = (int?)searchOperatorType;
            var searchConditionTypeInt = (int?)searchConditionType;
            var findObject = new FindObject
            {
                Conditions = new Condition[]
                {
                        new Condition {
                        Type = searchConditionTypeInt,
                        Operator = searchOperatorTypeInt,
                        Attribute = attributeId
                        }
                },
                Filters = new Filter[]
                {}
            };
            return findObject;
        }

        /// <summary>
        /// Закрытый метод создания объекта поиска по имени объекта
        /// </summary>
        /// <param name="searchConditionType">Параметр условия поиска, значение Enum указывает по какому критерию будет поиск</param>
        /// <param name="searchOperatorType">Оператор операции поиска</param>
        /// <param name="name">Имя объекта</param>
        /// <returns>Список объектов FindObject</returns>
        private FindObject MakeFindObject(SearchConditionType searchConditionType, SearchOperatorType searchOperatorType,string name)
        {
            var searchOperatorTypeInt = (int?)searchOperatorType;
            var searchConditionTypeInt = (int?)searchConditionType;
            var findObject = new FindObject
            {
                Conditions = new Condition[]
                {
                        new Condition {
                        Type = searchConditionTypeInt,
                        Operator = searchOperatorTypeInt,
                        Value = name
                        }
                },
                Filters = new Filter[]
                {}
            };
            return findObject;
        }

        /// <summary>
        /// Закрытый метод создания объекта поиска по фильтру объекта
        /// </summary>
        /// <param name="objectId">Значение для поиска по объекту</param>
        /// <param name="filterTypes">Тип фильтра</param>
        /// <returns>Список объектов FindObject</returns>
        private FindObject MakeFindObject(string objectId, FilterTypes filterTypes)
        {
            var findObject = new FindObject
            {
                Filters = new Filter[] {

                    new Filter
                    {
                        Type = (int)filterTypes,
                        Value = objectId
                    }
                }
            };
            return findObject;
        }

        /// <summary>
        /// Метод приведения JSON объекта к модели Result
        /// </summary>
        /// <param name="json">JSON объект</param>
        /// <returns>Рузультат операции приведения тип Result</returns>
        private Result ResultObjectMapping(dynamic json)
        {
            List<Attribute> attributes = new List<Attribute>();
            foreach (var item in json["Object"]["Attributes"])
            {
                foreach (var attribute in item)
                {
                    attributes.Add(new Attribute
                    {
                        Id = attribute["Id"],
                        AttributeType = attribute["Type"],
                        Value = attribute["Type"] != 8 ? attribute["Value"] : attribute["Value"]["Id"],
                        Name = attribute["Type"] != 8 ? null : attribute["Value"]["Name"]
                    });
                }
            }

            return new Result
            {
                ObjectId = json["Object"]["Id"],
                EntityId = json["Object"]["Entity"]["Id"],
                ObjectAttributes = attributes,
                ObjectName = json["Object"]["Name"],
            };
        }


        /// <summary>
        /// Метод работы с Api
        /// </summary>
        /// <param name="findObject">объект поиска</param>
        /// <param name="address">адрес сервиса</param>
        /// <returns></returns>
        private async Task<List<Result>> Request(FindObject findObject, string address)
        {
            List<Result> results = new List<Result>();
            var json = await webApiService.FindItemAsync(address, findObject);
            foreach (var item in json["Result"])
            {
                results.Add(ResultObjectMapping(item));
            }
            return results;
        }
    }
}
