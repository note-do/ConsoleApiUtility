using System.Net.Http;
using WebApiUtility.Domain.Models.ValueObjects;

namespace WebApiUtility.Domain.Contracts
{
    public interface IAccessApiService
    {
        /// <summary>
        /// Метод получения ключа аутентификации
        /// </summary>
        /// <returns>Объект ключа аутентификации</returns>
        AccessToken GetAccessToken();
        /// <summary>
        /// Метод получения экземпляра HttpClient
        /// </summary>
        /// <returns>Объект HttpClient</returns>
        HttpClient GetClient();
    }
}