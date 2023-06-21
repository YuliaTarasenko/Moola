namespace Moola.IntegrationsTests
{
    public class MyTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        public MyTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetIndesEnsureSuccessCode()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/");
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Home/Privacy")]
        [InlineData("/Home/Information")]
        [InlineData("/Incomes/Incomes")]
        [InlineData("/Expenses/Expenses")]
        public async Task GetSomePages(string url)
        {
            // Arrange
            var client = _factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
            // Act
            var response = await client.GetAsync(url);
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType?.ToString());
        }

        [Theory]
        [InlineData("/Users/Balance")]
        public async Task GetRedirectPagesIfNotAuth(string url)
        {
            // Arrange
            var client = _factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
            //Act
            var response = await client.GetAsync(url);
            // Assert
            // if it's not 200-299, it will throw an exception
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Contains("/Users/Login", response.Headers.Location?.OriginalString);
        }
    }
}