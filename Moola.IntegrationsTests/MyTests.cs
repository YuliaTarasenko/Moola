using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;

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
        [InlineData("/Users/Balance")]
        public async Task GetSomePages(string url)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/Users/Balance")]
        public async Task Get_SecurePageIsReturnedForAnAuthenticatedUser(string url)
        {
            // Arrange
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                    services.AddAuthentication(defaultScheme: "TestScheme")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                            "TestScheme", _ => { }));
            })
                .CreateClient(new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false,
                });

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(scheme: "TestScheme");
            //Act
            var response = await client.GetAsync(url);
            // Assert
            response.EnsureSuccessStatusCode();
            // if it's not 200-299, it will throw an exception
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType?.ToString());
        }
    }
}