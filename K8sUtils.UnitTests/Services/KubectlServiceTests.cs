using AutoFixture;
using FluentAssertions;
using K8sUtils.Exceptions;
using K8sUtils.Models.GetPodsResponse;
using K8sUtils.ProcessHosts;
using K8sUtils.Services;
using NSubstitute;
using Xunit;

namespace K8sUtils.UnitTests.Services;

public class KubectlServiceTests
{
    private readonly KubectlService _sut;
    private readonly IKubectlHost _kubectlHost;
    private readonly Fixture _fixture = new();
    
    public KubectlServiceTests()
    {
        _kubectlHost = Substitute.For<IKubectlHost>();
        _sut = new KubectlService(_kubectlHost);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task GetPodsAsync_NamespaceNotProvided_ThrowsArgumentException(string? @namespace)
    {
        // Arrange
        // Act
        var act = async () => await _sut.GetPodsAsync(@namespace);
        
        // Assert
        await act.Should().ThrowAsync<ArgumentException>().WithMessage("Namespace cannot be null or empty*");
    }
    
    [Fact]
    public async Task GetPodsAsync_NoPodsFound_ThrowsKubectlRuntimeException()
    {
        // Arrange
        _kubectlHost.ListPods(Arg.Any<string>()).Returns(
            _fixture.Build<GetPodsResponse>()
                .With(x => x.Items, () => [])
                .Create());

        // Act
        var act = async () => await _sut.GetPodsAsync("test");

        // Assert
        await act.Should().ThrowAsync<KubectlRuntimeException>().WithMessage("Unable to find any pods in the namespace*");
    }

    [Fact]
    public async Task GetPodsAsync_PodsFound_ReturnsPods()
    {
        // Arrange
        var expectedPods = _fixture.CreateMany<PodItem>(3).ToList();
        var root = _fixture.Build<GetPodsResponse>()
            .With(x => x.Items, () => expectedPods)
            .Create();
        _kubectlHost.ListPods(Arg.Any<string>()).Returns(root);
        
        // Act
        var result = await _sut.GetPodsAsync("test");
        
        // Assert
        result.ToList().Should().BeEquivalentTo(expectedPods);
        await _kubectlHost.Received(1).ListPods(Arg.Any<string>());
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task GetLogsAsync_PodNameNotProvided_ThrowsArgumentException(string? podName)
    {
        // Arrange
        // Act
        var act = async () => await _sut.GetLogsAsync(podName, "namespace");
        
        // Assert
        await act.Should().ThrowAsync<ArgumentException>().WithMessage("Pod name cannot be null or empty*");
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task GetLogsAsync_NamespaceNotProvided_ThrowsArgumentException(string? @namespace)
    {
        // Arrange
        // Act
        var act = async () => await _sut.GetLogsAsync("pod", @namespace);
        
        // Assert
        await act.Should().ThrowAsync<ArgumentException>().WithMessage("Namespace cannot be null or empty*");
    }
    
    [Fact]
    public async Task GetLogsAsync_ArgsProvided_CallsHostAndReturnsLogs()
    {
        // Arrange
        string[] expectedLogs = ["LOG", "What?"];
        _kubectlHost.GetLogs(Arg.Any<string>(), Arg.Any<string>()).Returns(expectedLogs);
        
        // Act
        var result = await _sut.GetLogsAsync("test", "ns");
        
        // Assert
        result.Should().BeEquivalentTo(expectedLogs);
        await _kubectlHost.Received(1).GetLogs(Arg.Any<string>(), Arg.Any<string>());
    }
}