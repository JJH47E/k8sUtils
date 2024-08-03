using FluentAssertions;
using K8sUtils.ProcessHosts;
using K8sUtils.Services;
using NSubstitute;
using Xunit;

namespace K8sUtils.UnitTests.Services;

public class KubectlServiceTests
{
    private readonly KubectlService _sut;
    private readonly IKubectlHost _kubectlHost;
    
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
}