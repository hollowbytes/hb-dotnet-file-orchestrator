using System.IO.Abstractions;
using AutoFixture;
using AutoFixture.AutoMoq;
using HbDotnetFileOrchestrator.Application.Files.Models.Commands;
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
        var fakeCommand = _fixture.Create<FileWriterCommand>();

        var mockDirectory = _fixture.Freeze<Mock<IDirectory>>();
        mockDirectory.Setup(x => x.Exists(It.IsAny<string>())).Returns(false);
        _fixture.Register(() => mockDirectory.Object);
        
        // Act
        var sut = _fixture.Create<FileSystemFileWriter>();
        var actual = await sut.SaveAsync(fakeCommand, CancellationToken.None);

        // Actual
        mockDirectory.Verify(x => x.Exists(fakeCommand.Directory), Times.Once);
        mockDirectory.Verify(x => x.CreateDirectory(fakeCommand.Directory), Times.Once);
    }
    
    [Fact]
    public async Task SaveAsync_GivenDirectoryExists_ThenDoNotCreateDirectory()
    {
        // Arrange
        var fakeCommand = _fixture.Create<FileWriterCommand>();

        var mockDirectory = _fixture.Freeze<Mock<IDirectory>>();
        mockDirectory.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);
        _fixture.Register(() => mockDirectory.Object);
        
        // Act
        var sut = _fixture.Create<FileSystemFileWriter>();
        var actual = await sut.SaveAsync(fakeCommand, CancellationToken.None);

        // Actual
        mockDirectory.Verify(x => x.Exists(fakeCommand.Directory), Times.Once);
        mockDirectory.Verify(x => x.CreateDirectory(It.IsAny<string>()), Times.Never);
    }
    
    
    [Fact]
    public async Task SaveAsync_GivenFileDoesNotExist_ThenCreateFile()
    {
        // Arrange
        var fakeCommand = _fixture.Create<FileWriterCommand>();

        var mockFile = _fixture.Freeze<Mock<IFile>>();
        mockFile.Setup(x => x.Exists(It.IsAny<string>())).Returns(false);
        _fixture.Register(() => mockFile.Object);
        
        // Act
        var sut = _fixture.Create<FileSystemFileWriter>();
        var actual = await sut.SaveAsync(fakeCommand, CancellationToken.None);

        // Actual
        mockFile.Verify(x => x.Exists(fakeCommand.FullPath), Times.Once);
        mockFile.Verify(x => x.WriteAllBytesAsync(fakeCommand.FullPath, fakeCommand.File.Contents, It.IsAny<CancellationToken>()), Times.Once);
        actual.IsSuccess.ShouldBeTrue();
    }
    
    [Fact]
    public async Task SaveAsync_GivenFileExists_ThenDoNotCreateFile()
    {
        // Arrange
        var fakeCommand = _fixture.Create<FileWriterCommand>();
        
        var mockFile = _fixture.Freeze<Mock<IFile>>();
        mockFile.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);
        _fixture.Register(() => mockFile.Object);
        
        // Act
        var sut = _fixture.Create<FileSystemFileWriter>();
        var actual = await sut.SaveAsync(fakeCommand, CancellationToken.None);

        // Actual
        mockFile.Verify(x => x.Exists(fakeCommand.FullPath), Times.Once);
        mockFile.Verify(x => x.WriteAllBytesAsync(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<CancellationToken>()), Times.Never);
        actual.IsSuccess.ShouldBeFalse();
    }
}