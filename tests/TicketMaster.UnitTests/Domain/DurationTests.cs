namespace TicketMaster.UnitTests.Domain;

using FluentAssertions;
using TicketMaster.Core.Domain.Events;
using TicketMaster.Core.Domain.Exceptions;

public class DurationTests
{
    [Fact]
    public void Constructor_CreatesDurationWithValidPositiveValue_WhenPassedValidValue()
    {
        // Arrange
        int validValue = 60;

        // Act
        var duration = new Duration(validValue);

        // Assert
        duration.Value.Should().Be(validValue);
    }

    [Fact]
    public void Constructor_CreatesDurationWithZeroValue_WhenPassedZero()
    {
        // Arrange
        int zeroValue = 0;

        // Act
        var duration = new Duration(zeroValue);

        // Assert
        duration.Value.Should().Be(zeroValue);
    }

    [Fact]
    public void Constructor_ThrowsDomainException_WhenPassedNegativeValue()
    {
        // Arrange
        int negativeValue = -1;

        // Act & Assert
        var action = () => new Duration(negativeValue);
        action.Should()
            .Throw<DomainException>()
            .And.Message.Should().Be("A validation error has occurred.");
    }

    [Theory]
    [InlineData(-5)]
    [InlineData(-60)]
    [InlineData(-1440)]
    public void Constructor_ThrowsDomainException_WhenPassedVariousNegativeValues(int negativeValue)
    {
        // Act & Assert
        var action = () => new Duration(negativeValue);
        action.Should()
            .Throw<DomainException>()
            .And.Message.Should().Be("A validation error has occurred.");
    }

    [Fact]
    public void Equality_AreEqual_WhenTwoDurationsHaveSameValue()
    {
        // Arrange
        var duration1 = new Duration(90);
        var duration2 = new Duration(90);

        // Act & Assert
        duration1.Should().Be(duration2);
    }

    [Fact]
    public void Equality_AreNotEqual_WhenTwoDurationsHaveDifferentValues()
    {
        // Arrange
        var duration1 = new Duration(90);
        var duration2 = new Duration(120);

        // Act & Assert
        duration1.Should().NotBe(duration2);
    }

    [Fact]
    public void Equality_IsNotEqual_WhenDurationIsComparedWithNull()
    {
        // Arrange
        var duration = new Duration(90);

        // Act & Assert
        duration.Should().NotBe(null);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(30)]
    [InlineData(60)]
    [InlineData(120)]
    [InlineData(1440)]
    public void Constructor_StoresValueCorrectly_WhenPassedValidValues(int validValue)
    {
        // Act
        var duration = new Duration(validValue);

        // Assert
        duration.Value.Should().Be(validValue);
    }
}