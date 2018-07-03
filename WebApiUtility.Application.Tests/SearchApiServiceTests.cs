using Autofac.Extras.NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiUtility.Application.Services;
using WebApiUtility.Domain.Models.ValueObjects;
using Xunit;

namespace WebApiUtility.Application.Tests
{
    public class SearchApiServiceTests
    {
        [Fact]
        public void SearchObjectAsync_ObjectIdAttributeId_ResultList()
        {
            var logger = NSubstitute.Substitute.For<ILogger>();
            var service = NSubstitute.Substitute.For<AccessApiService>(@"https://devtest.io.neolant.su/", logger);
            var api = NSubstitute.Substitute.For<ApiClientService>(service, logger);
            var search = NSubstitute.Substitute.For<SearchApiService>(api, logger);

            var emptyGuid = Guid.Empty.ToString();

            var result = search.SearchObjectAsync(emptyGuid, emptyGuid, Domain.Models.ValueObjects.FilterTypes.Class, Domain.Models.ValueObjects.SearchConditionType.Attribute, Domain.Models.ValueObjects.SearchOperatorType.Exists).Result;

            Assert.IsType<List<Result>>(result);
        }
    }
}
