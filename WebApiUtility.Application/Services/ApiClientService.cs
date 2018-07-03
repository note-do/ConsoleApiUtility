using Autofac.Extras.NLog;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebApiUtility.Domain.Contracts;
using WebApiUtility.Domain.Models.ValueObjects;

namespace WebApiUtility.Application.Services
{
    public class ApiClientService : IWebApiService
    {
        private readonly IAccessApiService accessApiService;
        private readonly ILogger logger;

        public ApiClientService(IAccessApiService accessApiService,ILogger logger)
        {
            this.accessApiService = accessApiService;
            this.logger = logger;
        }

        public async Task<dynamic> FindItemAsync(string addressString, FindObject findObject, int skip = 0, int take = 20)
        {
            using (var client = await GetClientAsync())
            {
                client.DefaultRequestHeaders.Add("X-HTTP-Method-Override", "GET");
                var content = Newtonsoft.Json.JsonConvert.SerializeObject(findObject);
                var payload = GetStringContent(content);
                var httpResponse = await client.PostAsync($"{addressString}?skip={skip}&take={take}", payload);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    var dynamicJson = JsonConvert.DeserializeObject<dynamic>(responseContent);
                    logger.Debug($"Запрос успешно отправлен {httpResponse.StatusCode}");
                    return dynamicJson;
                }
                logger.Error($"на сервере произошла ошибка {httpResponse.StatusCode} - {httpResponse.ReasonPhrase}");
                return null;
            }
        }
        public async Task<dynamic> ChangeAttributeAsync(string addressString,string objectId, List<JObject> attributes)
        {
            using (var client = await GetClientAsync())
            {
                var content = Newtonsoft.Json.JsonConvert.SerializeObject(attributes);
                var payload = GetStringContent(content);

                var httpResponse = await client.PutAsync($"{addressString}/{objectId}/attributes", payload);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var attr = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(await httpResponse.Content.ReadAsStringAsync());
                    logger.Debug($"Запрос успешно отправлен {httpResponse.StatusCode}");
                    return attr;
                }
                logger.Error($"на сервере произошла ошибка {httpResponse.StatusCode} - {httpResponse.ReasonPhrase}");
                return null;
            }
        }

        private StringContent GetStringContent(string content)
        {
            return new StringContent(content, Encoding.UTF8, "application/json");
        }

        private async Task<HttpClient> GetClientAsync()
        {
            return await Task.Run(() =>
            {
                var token = accessApiService.GetAccessToken();
                var client = accessApiService.GetClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"{token.TokenType}", $"{token.AccessTokenValue}");
                client.DefaultRequestHeaders.Add(@"X-Requested-With", @"XMLHttpRequest");
                return client;
            });
        }


    }
}

