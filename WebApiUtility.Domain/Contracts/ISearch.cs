using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiUtility.Domain.Models.ValueObjects;

namespace WebApiUtility.Domain.Contracts
{
    public interface ISearch
    {
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
        Task<List<Result>> SearchObjectAsync(string objectId, string attibuteId, FilterTypes filterTypes, SearchConditionType searchConditionType, SearchOperatorType searchOperatorType, string address = @"api/objects/search");
        /// <summary>
        /// Метод поиска по атрибуту объекта
        /// </summary>
        /// <param name="attributeId">Класс объекта для поиска</param>
        /// <param name="searchCondition">Параметр условия поиска, значение Enum указывает по какому критерию будет поиск</param>
        /// <param name="filterTypes">Тип фильтра для уточнения условий поиска, по умолчанию поиск по Классу объекта</param>
        /// <param name="address">Относительный адрес API сервиса (опциональное значение)</param>
        /// <returns>Список объектов Result</returns>
        Task<List<Result>> SearchObjectAsync(string attributeId, SearchConditionType searchConditionType, SearchOperatorType searchOperatorType, string address = @"api/objects/search");
        /// <summary>
        ///  Метод поиска по классу объекта
        /// </summary>
        /// <param name="objectId">Id класса объекта для поиска</param>
        /// <param name="filterTypes">Фильтр типа(по родительскому каталогу или по Id класса объекта)</param>
        /// <param name="address">Относительный адрес API сервиса (опциональное значение)</param>
        /// <returns></returns>
        Task<List<Result>> SearchObjectAsync(string objectId, FilterTypes filterTypes, string address = @"api/objects/search");
        /// <summary>
        /// Метод поиска по имени объекта
        /// </summary>
        /// <param name="searchConditionType">Параметр условия поиска, значение Enum указывает по какому критерию будет поиск</param>
        /// <param name="searchOperatorType">Оператор операции поиска</param>
        /// <param name="name">Имя объекта</param>
        /// <param name="address">Относительный адрес API сервиса (опциональное значение)</param>
        /// <returns></returns>
        Task<List<Result>> SearchObjectAsync(SearchConditionType searchConditionType, SearchOperatorType searchOperatorType, string name, string address = @"api/objects/search");

    }
}