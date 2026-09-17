namespace TicketMaster.UnitTests.Domain;

using FluentAssertions;
using TicketMaster.Core.Domain.Events;
using TicketMaster.Core.Domain.Exceptions;

public class CapacityTests
{
    [Fact]
    public void Constructor_CreatesCapacityWithValidPositiveValue_WhenPassedValidValue()
    {
        // Arrange
        int validValue = 100;

        // Act
        var capacity = new Capacity(validValue);

        // Assert
        capacity.Value.Should().Be(validValue);
    }

    [Fact]
    public void Constructor_CreatesCapacityWithZeroValue_WhenPassedZero()
    {
        // Arrange
        int zeroValue = 0;

        // Act
        var capacity = new Capacity(zeroValue);

        // Assert
        capacity.Value.Should().Be(zeroValue);
    }

    [Fact]
    public void Constructor_ThrowsDomainException_WhenPassedNegativeValue()
    {
        // Arrange
        int negativeValue = -1;

        // Act & Assert
        var action = () => new Capacity(negativeValue);
        action.Should()
            .Throw<DomainException>()
            .And.Message.Should().Be("A validation error has occurred.");
    }

    [Theory]
    [InlineData(-10)]
    [InlineData(-100)]
    [InlineData(-1)]
    public void Constructor_ThrowsDomainException_WhenPassedVariousNegativeValues(int negativeValue)
    {
        // Act & Assert
        var action = () => new Capacity(negativeValue);
        action.Should()
            .Throw<DomainException>()
            .And.Message.Should().Be("A validation error has occurred.");
    }

    [Fact]
    public void Equality_AreEqual_WhenTwoCapacitiesHaveSameValue()
    {
        // Arrange
        var capacity1 = new Capacity(100);
        var capacity2 = new Capacity(100);

        // Act & Assert
        capacity1.Should().Be(capacity2);
    }

    [Fact]
    public void Equality_AreNotEqual_WhenTwoCapacitiesHaveDifferentValues()
    {
        // Arrange
        var capacity1 = new Capacity(100);
        var capacity2 = new Capacity(50);

        // Act & Assert
        capacity1.Should().NotBe(capacity2);
    }

    [Fact]
    public void Equality_IsNotEqual_WhenCapacityIsComparedWithNull()
    {
        // Arrange
        var capacity = new Capacity(100);

        // Act & Assert
        capacity.Should().NotBe(null);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(1000)]
    [InlineData(10000)]
    public void Constructor_StoresValueCorrectly_WhenPassedValidValues(int validValue)
    {
        // Act
        var capacity = new Capacity(validValue);

        // Assert
        capacity.Value.Should().Be(validValue);
    }
}