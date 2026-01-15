using System.Net.Mime;
using AutoFixture;
using AutoFixture.AutoMoq;
using HbDotnetFileOrchestrator.Infrastructure.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using Moq;
using Shouldly;

namespace HbDotnetFileOrchestrator.Infrastructure.Tests.Http;

public class MetadataProviderTests
{
    private readonly IFixture _fixture = new Fixture().Customize(new AutoMoqCustomization {  ConfigureMembers = true });
    
    [Fact]
    public async Task GetMetadataAsync_GivenHttpContextAccessorNotFound_ThenThrowException()
    {
        // Arrange
        var mockHttpContext = _fixture.Freeze<Mock<IHttpContextAccessor>>();
        mockHttpContext.Setup(x => x.HttpContext).Returns((HttpContext?) null);
        
        // Act
        var sut = _fixture.Create<MetadataProvider>();
        var actual =  () => sut.GetMetadataAsync();
        
        // Assert
        await actual.ShouldThrowAsync<InvalidOperationException>();
    }
    
    [Fact]
    public async Task GetMetadataAsync_GivenHttpContextAccessor_ThenMapFormProperties()
    {
        // Arrange
        var fakeHeaders = _fixture.Create<Dictionary<string, StringValues>>();
        var fakeHttpContext = new DefaultHttpContext();
        
        var boundary = "----WebKitFormBoundary7MA4YWxkTrZu0gW";
        var multipartContent = new MultipartFormDataContent(boundary);
        
        var stringContent = new StringContent("testuser");
        multipartContent.Add(stringContent, "username");

        var requestBodyStream = new MemoryStream();
        await multipartContent.CopyToAsync(requestBodyStream);
        requestBodyStream.Position = 0; 

        fakeHttpContext.Request.Body = requestBodyStream;
        fakeHttpContext.Request.ContentType = $"multipart/form-data; boundary={boundary}"; 
        
        foreach (var header in fakeHeaders)
        {
            fakeHttpContext.Request.Headers.Append(header.Key, header.Value);
        }
        
        var mockHttpContextAccessor = _fixture.Freeze<Mock<IHttpContextAccessor>>();
        mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(fakeHttpContext);
        
        // Act
        var sut = _fixture.Create<MetadataProvider>();
        var actual = await sut.GetMetadataAsync();
        
        // Assert
        foreach (var headers in actual.Headers)
        {
            headers.Value.ShouldBe(fakeHttpContext.Request.Headers[headers.Key]);
        }
    }
    
    [Fact]
    public async Task GetMetadataAsync_GivenHttpContextAccessor_ThenMapQueryString()
    {
        // Arrange
        var fakeDictionary = _fixture.Create<Dictionary<string, string>>();
        var fakeQuery = new QueryBuilder(fakeDictionary).ToQueryString();
        
        var fakeHttpContext = new DefaultHttpContext();
        fakeHttpContext.Request.QueryString = fakeQuery;
        
        var mockHttpContextAccessor = _fixture.Freeze<Mock<IHttpContextAccessor>>();
        mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(fakeHttpContext);
        
        // Act
        var sut = _fixture.Create<MetadataProvider>();
        var actual = await sut.GetMetadataAsync();
        
        // Assert
        foreach (var query in actual.Query)
        {
            query.Value.ShouldBe(fakeHttpContext.Request.Query[query.Key]);
        }
    }
    
    [Fact]
    public async Task GetMetadataAsync_GivenHttpContextAccessor_ThenMapHeaders()
    {
        // Arrange
        var fakeDictionary = _fixture.Create<Dictionary<string, string>>();
        
        var fakeHttpContext = new DefaultHttpContext();
        
        foreach (var header in fakeDictionary)
        {
            fakeHttpContext.Request.Headers.Append(header.Key, header.Value);
        };
        
        var mockHttpContextAccessor = _fixture.Freeze<Mock<IHttpContextAccessor>>();
        mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(fakeHttpContext);
        
        // Act
        var sut = _fixture.Create<MetadataProvider>();
        var actual = await sut.GetMetadataAsync();
        
        // Asset
        foreach (var header in actual.Headers)
        {
            header.Value.ShouldBe(fakeHttpContext.Request.Headers[header.Key]);
        }
    }
    
    
    [Fact]
    public async Task GetMetadataAsync_GivenHttpContextAccessor_ThenMapRoutes()
    {
        // Arrange
        var fakeDictionary = _fixture.Create<Dictionary<string, string?>>();
        
        var fakeHttpContext = new DefaultHttpContext();
        fakeHttpContext.Request.RouteValues = new RouteValueDictionary(fakeDictionary);
        
        var mockHttpContextAccessor = _fixture.Freeze<Mock<IHttpContextAccessor>>();
        mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(fakeHttpContext);
        
        // Act
        var sut = _fixture.Create<MetadataProvider>();
        var actual = await sut.GetMetadataAsync();
        
        // Asset
        foreach (var route in actual.RouteValues)
        {
            route.Value.ShouldBe(fakeHttpContext.Request.RouteValues[route.Key]);
        }
    }
}