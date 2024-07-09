using JsonConverterApp.Core.Interfaces;
using JsonConverterApp.Infrastructure;
using Moq;
using Moq.Protected;


namespace JsonConverterApp.Test
{
    public class CompanyDetailsTest
    {
        private readonly Mock<IReadDataHandler> _mockReadData;

        public CompanyDetailsTest()
        {
            _mockReadData = new Mock<IReadDataHandler>();
        }
        [Fact]
        public void GetDataAsync_Should_Return_Valid_Json_Data()
        {
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();

            HttpResponseMessage httpResponseMessage = new()
            {
                Content = new StringContent(@"<?xml version=""1.0"" encoding=""UTF-8""?>
<Data>
	<id>1</id>
	<name>OpenPolytechnic</name>
	<description>..is awesome</description>
</Data>")
            };

            // Set up the SendAsync method behavior.
            httpMessageHandlerMock
                .Protected() // <= this is most important part that it need to setup.
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponseMessage);

            // create the HttpClient
            var httpClient = new HttpClient(httpMessageHandlerMock.Object)
            {
                BaseAddress = new System.Uri("http://localhost") // It should be in valid uri format.
            };

            var expectedResult = "{\"Data\":{\"id\":\"1\",\"name\":\"OpenPolytechnic\",\"description\":\"..is awesome\"}}";

            ApiHandler apiDataHandler = new(httpClient);
            var subjectUnderTest = new CompanyDetails(apiDataHandler);

            //Act
           // var result = apiDataHandler.ReadFileAsync("http://localhost").Result;
           var result = subjectUnderTest.GetCompany(2);

            //Assert
            Assert.Equal(expectedResult, result.Result.Replace("\r\n", "").Replace("\n", "").Replace("\r", ""));
;
        }
    }
}
