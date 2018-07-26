using Autofac.Extras.NLog;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using WebApiUtility.Domain.Contracts;
using WebApiUtility.Domain.Models.ValueObjects;

namespace WebApiUtility.Application.Services
{
    public class AccessApiService : IAccessApiService
    {
        private readonly string serverAddress;
        private readonly ILogger logger;

        public AccessApiService(string serverAddress, ILogger logger)
        {
            this.serverAddress = serverAddress;
            this.logger = logger;
        }
        public AccessToken GetAccessToken()
        {
            AccessToken accessToken = null;
            using (var client = GetClient())
            {
                if (accessToken == null)
                {
                    logger.Debug("Получаем ключ аутентификации");
#warning В код зашит уже зашифрованный clienID + secret.
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "Y2xpZW50MTpMNlFCdFpDclV5TndaZmpMbXpGVjhiWnpMR1MwME81Qw==");
#warning Авторизация зашита в код.
                    var payload = @"grant_type=password&username=Шестаков&password=Q1w2e3r4t5";
                    var content = new StringContent(payload, Encoding.UTF8, "application/x-www-form-urlencoded");
                    var response = client.PostAsync(@"api/token", content).GetAwaiter().GetResult();
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        accessToken = Newtonsoft.Json.JsonConvert.DeserializeObject<AccessToken>(responseContent);
                        logger.Debug("Успешно получен ключ аутентификации");
                        return accessToken;
                    }
                    logger.Error($"при запросе ключа от сервера произошла ошибка {response.StatusCode} - {response.ReasonPhrase}");
                }
                return accessToken;
            }
        }

        public HttpClient GetClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(serverAddress);
            logger.Debug("Создание экземпляра HttpClient");
            return client;
        }
    }
}
