using Autofac.Extras.NLog;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiUtility.Application.Services;
using WebApiUtility.Domain.Contracts;
using WebApiUtility.Domain.Models.ValueObjects;
using Xunit;

namespace WebApiUtility.Application.Tests
{
    public class ApiClientServiceTests
    {
        [Fact]
        public void FindItemAsync_FindItemObject_JsonDynamic()
        {
            //Assign
            var logger = NSubstitute.Substitute.For<ILogger>();
            var service = NSubstitute.Substitute.For<AccessApiService>(@"https://devtest.io.neolant.su/", logger);
            var api = NSubstitute.Substitute.For<ApiClientService>(service,logger);
            var findObject = NSubstitute.Substitute.For<FindObject>();
            //Act
            var act = api.FindItemAsync(@"api/objects/search", findObject).Result;
            //Assert
            Assert.IsType<dynamic>(act);
        }

        [Fact]
        public void ChangeAttributeAsync_FindItemObject_JsonDynamic()
        {
            //Assign
            var logger = NSubstitute.Substitute.For<ILogger>();
            var service = NSubstitute.Substitute.For<AccessApiService>(@"https://devtest.io.neolant.su/", logger);
            var api = NSubstitute.Substitute.For<ApiClientService>(service, logger);
            var findObject = NSubstitute.Substitute.For<FindObject>();
            var list = new List<JObject>();
            //Act
            var act = api.ChangeAttributeAsync(@"api/objects/search", Guid.Empty.ToString(), list).Result;
            //Assert
            Assert.IsType<dynamic>(act);
        }
    }
}
