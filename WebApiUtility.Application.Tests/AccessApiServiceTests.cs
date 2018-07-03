using Autofac.Extras.NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApiUtility.Application.Services;
using WebApiUtility.Domain.Contracts;
using WebApiUtility.Domain.Models.ValueObjects;
using Xunit;

namespace WebApiUtility.Application.Tests
{
    public class AccessApiServiceTests
    {
        [Fact]
        public void GetAccessToken_returnAccessToken()
        {
            //Assign
            var logger = NSubstitute.Substitute.For<ILogger>();
            var service = NSubstitute.Substitute.For<AccessApiService>(@"https://devtest.io.neolant.su/", logger);
            //Act
            var token = service.GetAccessToken();
            //Assert
            Assert.IsType<AccessToken>(token);
        }

        [Fact]
        public void GetHttpClient_returnHttpClient()
        {
            //Assign
            var logger = NSubstitute.Substitute.For<ILogger>();
            var service = NSubstitute.Substitute.For<AccessApiService>(@"https://devtest.io.neolant.su/", logger);
            //Act
            var client = service.GetClient();

            Assert.IsType<HttpClient>(client);
        }
    }
}
