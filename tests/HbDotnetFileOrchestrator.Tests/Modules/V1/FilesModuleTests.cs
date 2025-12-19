using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;

namespace HbDotnetFileOrchestrator.Tests.Modules.V1;

public class FilesModuleTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _sut = factory.CreateClient();

    [Fact]
    public async Task PostFileAsync_ReturnsOkAcceptedResult()
    {
        // Arrange

        // Act
        var actual = await _sut.PostAsync("/api/v1/files", new StringContent(""));

        // Assert
        actual.StatusCode.ShouldBe(HttpStatusCode.Accepted);
    }

    [Fact]
    public async Task GetFileAsync_ReturnsOkResult()
    {
        // Arrange

        // Act
        var actual = await _sut.GetAsync("/api/v1/files");

        // Assert
        actual.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}