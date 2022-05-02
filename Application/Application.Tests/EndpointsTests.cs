using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Application.Tests;

public class ApiWebApplicationFactory : WebApplicationFactory<Program>
{
    // protected override void ConfigureWebHost(IWebHostBuilder builder)
    // {
    // Is be called after the `ConfigureServices` from the Startup
    // which allows you to overwrite the DI with mocked instances
    // builder.ConfigureTestServices(services =>
    // {
    //     services.AddTransient<IWeatherForecastConfigService, WeatherForecastConfigMock>();
    // });
    // }
}

public class EndpointsTests : IClassFixture<ApiWebApplicationFactory>
{
    private readonly HttpClient _client;

    public EndpointsTests(ApiWebApplicationFactory fixture)
    {
        _client = fixture.CreateClient();
    }

    [Fact]
    public async Task GET_users_success()
    {
        var response = await _client.GetAsync("/users");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GET_cities_success()
    {
        var response = await _client.GetAsync("/cities");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GET_recommendations_success()
    {
        var response = await _client.GetAsync("/recommendations");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GET_non_existent_endpoint_fails()
    {
        var response = await _client.GetAsync("/non-existent");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}