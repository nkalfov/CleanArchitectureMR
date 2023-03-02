
using CleanArchitecture.Infrastructure.Models;
using Xunit;

namespace CleanArchitecture.Infrastructure.Tests;

public class SaleEventModelTests
{
    [Fact(DisplayName = "Parameterless Constructor Initiates Properties to Default Values")]
    public void ParameterlessConstructor()
    {
        // Arrange
        var expectedProductId = 0L;
        var expectedQuantity = 0;

        // Act
        var actual = new SaleEventModel();

        // Assert
        Assert.Equal(expectedProductId, actual.ProductId);
        Assert.Equal(expectedQuantity, actual.Quantity);
    }

    [Fact(DisplayName = "Object Initializer Initiates Properties to Desired Values")]
    public void ObjectInitializer()
    {
        // Arrange
        var expectedProductId = 12L;
        var expectedQuantity = 2;

        // Act
        var actual = new SaleEventModel
        {
            ProductId = expectedProductId,
            Quantity = expectedQuantity
        };

        // Assert
        Assert.Equal(expectedProductId, actual.ProductId);
        Assert.Equal(expectedQuantity, actual.Quantity);
    }

    [Fact(DisplayName = "Using All-Parameters Constructor Initiates All Properties to Desired Values")]
    public void AllParametersConstructor()
    {
        // Arrange
        var expectedProductId = 13L;
        var expectedQuantity = 3;

        // Act
        var actual = new SaleEventModel(expectedProductId, expectedQuantity);

        // Assert
        Assert.Equal(expectedProductId, actual.ProductId);
        Assert.Equal(expectedQuantity, actual.Quantity);
    }
}
