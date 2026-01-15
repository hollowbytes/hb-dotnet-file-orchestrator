using System.IO.Abstractions;
using AutoFixture;
using AutoFixture.AutoMoq;
using HbDotnetFileOrchestrator.Domain.Models;
using HbDotnetFileOrchestrator.Infrastructure.Storage.FileSystem;
using Moq;
using Shouldly;

namespace HbDotnetFileOrchestrator.Infrastructure.Tests.Storage.FileSystem;

public class FileSystemFileWriterTests
{
    private readonly IFixture _fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });

    [Fact]
    public async Task SaveAsync_GivenDirectoryDoesNotExist_ThenCreateDirectory()
    {
        // Arrange
        var fakeFile = _fixture.Create<ReceivedFile>();
        var fakeLocation = _fixture.Create<string>();

        var mockDirectory = _fixture.Freeze<Mock<IDirectory>>();
        mockDirectory.Setup(x => x.Exists(It.IsAny<string>())).Returns(false);
        _fixture.Register(() => mockDirectory.Object);
        
        // Act
        var sut = _fixture.Create<FileSystemFileWriter>();
        var actual = await sut.SaveAsync(fakeFile, fakeLocation, CancellationToken.None);

        // Actual
        mockDirectory.Verify(x => x.Exists(fakeLocation), Times.Once);
        mockDirectory.Verify(x => x.CreateDirectory(fakeLocation), Times.Once);
    }
    
    [Fact]
    public async Task SaveAsync_GivenDirectoryExists_ThenDoNotCreateDirectory()
    {
        // Arrange
        var fakeFile = _fixture.Create<ReceivedFile>();
        var fakeLocation = _fixture.Create<string>();

        var mockDirectory = _fixture.Freeze<Mock<IDirectory>>();
        mockDirectory.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);
        _fixture.Register(() => mockDirectory.Object);
        
        // Act
        var sut = _fixture.Create<FileSystemFileWriter>();
        var actual = await sut.SaveAsync(fakeFile, fakeLocation, CancellationToken.None);

        // Actual
        mockDirectory.Verify(x => x.Exists(fakeLocation), Times.Once);
        mockDirectory.Verify(x => x.CreateDirectory(It.IsAny<string>()), Times.Never);
    }
    
    
    [Fact]
    public async Task SaveAsync_GivenFileDoesNotExist_ThenCreateFile()
    {
        // Arrange
        var fakeFile = _fixture.Create<ReceivedFile>();
        var fakeLocation = _fixture.Create<string>();

        var expectedPath = Path.Combine(fakeLocation, fakeFile.Name);
        
        var mockFile = _fixture.Freeze<Mock<IFile>>();
        mockFile.Setup(x => x.Exists(It.IsAny<string>())).Returns(false);
        _fixture.Register(() => mockFile.Object);
        
        // Act
        var sut = _fixture.Create<FileSystemFileWriter>();
        var actual = await sut.SaveAsync(fakeFile, fakeLocation, CancellationToken.None);

        // Actual
        mockFile.Verify(x => x.Exists(expectedPath), Times.Once);
        mockFile.Verify(x => x.WriteAllBytesAsync(expectedPath, fakeFile.Contents, It.IsAny<CancellationToken>()), Times.Once);
        actual.IsSuccess.ShouldBeTrue();
    }
    
    [Fact]
    public async Task SaveAsync_GivenFileExists_ThenDoNotCreateFile()
    {
        // Arrange
        var fakeFile = _fixture.Create<ReceivedFile>();
        var fakeLocation = _fixture.Create<string>();
        
        var expectedPath = Path.Combine(fakeLocation, fakeFile.Name);

        var mockFile = _fixture.Freeze<Mock<IFile>>();
        mockFile.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);
        _fixture.Register(() => mockFile.Object);
        
        // Act
        var sut = _fixture.Create<FileSystemFileWriter>();
        var actual = await sut.SaveAsync(fakeFile, fakeLocation, CancellationToken.None);

        // Actual
        mockFile.Verify(x => x.Exists(expectedPath), Times.Once);
        mockFile.Verify(x => x.WriteAllBytesAsync(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<CancellationToken>()), Times.Never);
        actual.IsSuccess.ShouldBeFalse();
    }
}