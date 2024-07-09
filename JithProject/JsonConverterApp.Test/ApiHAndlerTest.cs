using JsonConverterApp.Core.Interfaces;
using JsonConverterApp.Infrastructure;
using Moq;
using Moq.Protected;

namespace JsonConverterApp.Test
{
    public class ApiHAndlerTest
    {
        private Mock<IReadDataHandler> _mockReadDataHandler;
        public ApiHAndlerTest()
        {
            _mockReadDataHandler = new Mock<IReadDataHandler>();
        }

        [Fact]
        public void GetDataAsync_Should_Return_Valid_Data()
        {
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();

            HttpResponseMessage httpResponseMessage = new()
            {
                Content = new StringContent(@"{<Data><id>1</id><name>OpenPolytechnic</name><description>..isawesome</description></Data>}")
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

            var expectedResult = "{<Data><id>1</id><name>OpenPolytechnic</name><description>..isawesome</description></Data>}";

            ApiHandler apiDataHandler = new(httpClient);
            //Act
            var result = apiDataHandler.ReadFileAsync("http://localhost").Result;
         
            //Assert
            Assert.Equal(expectedResult, result);
;        }
    }
}