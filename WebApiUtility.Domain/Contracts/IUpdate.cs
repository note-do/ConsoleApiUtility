using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiUtility.Domain.Models.ValueObjects;

namespace WebApiUtility.Domain.Contracts
{
    public interface IUpdate
    {
        /// <summary>
        /// Основной метод обновления данных по классу объекта
        /// </summary>
        /// <param name="classId">Id класса объектов для поиска</param>
        /// <param name="existedAttributeId">Значение которое необходимо изменить</param>
        /// <param name="newValue">Значение на которое необходимо изменить</param>
        /// <param name="filterTypes">Фильтр значения для поиска объекта</param>
        /// <param name="address">Относительный адрес API сервиса (опциональное значение)</param>
        /// <returns>Список объектов Result</returns>
        Task<List<UpdateResult>> UpdateAttributeAsync(string classId, string existedAttributeId, string newValue, FilterTypes filterTypes, string address = @"api/objects");

        /// <summary>
        /// Основной метод обновления данных по атрибуту
        /// </summary>
        /// <param name="existedAttributeId">Значение которое необходимо изменить</param>
        /// <param name="newValue">Значение на которое необходимо изменить</param>
        /// <param name="newVsearchConditionTypealue">Значение на которое необходимо изменить</param>
        /// <param name="searchOperatorType">Значение на которое необходимо изменить</param>
        /// <param name="address">Относительный адрес API сервиса (опциональное значение)</param>
        /// <returns>Список объектов Result</returns>
        Task<List<UpdateResult>> UpdateAttributeAsync(string existedAttributeId, string newValue, SearchConditionType searchConditionType, SearchOperatorType searchOperatorType, string address = @"api/objects");
    }
}
