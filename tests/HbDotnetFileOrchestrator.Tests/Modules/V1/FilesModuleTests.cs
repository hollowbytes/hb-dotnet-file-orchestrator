using System.Net;
using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;

namespace HbDotnetFileOrchestrator.Tests.Modules.V1;

public class FilesModuleTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly IFixture _fixture =  new Fixture().Customize(new AutoMoqCustomization {  ConfigureMembers = true });
    private readonly HttpClient _sut = factory.CreateClient();

    [Theory]
    [InlineData("", HttpStatusCode.BadRequest)]
    [InlineData("foo", HttpStatusCode.Created)]
    public async Task PostFileAsync_ReturnsOkAcceptedResult(string content, HttpStatusCode expectedStatusCode)
    {
        // Arrange
        using var form = new MultipartFormDataContent();        
        form.Add(new StringContent(content), "file", "file.txt");
        
        // Act
        var actual = await _sut.PostAsync("/api/v1/files", form);

        // Assert
        actual.StatusCode.ShouldBe(expectedStatusCode);
    }

    [Fact]
    public async Task GetFileAsync_ReturnsOkResult()
    {
        // Arrange
        var conversationId = _fixture.Create<Guid>();
        
        // Act
        var actual = await _sut.GetAsync($"/api/v1/files/{conversationId}");

        // Assert
        actual.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}