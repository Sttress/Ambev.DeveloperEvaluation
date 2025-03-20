using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class ProductTests
{
    [Fact(DisplayName = "Product updated at should change not be null when change values")]
    public void Given_Product_When_Change_Info_Then_UpdatedAt_Should_Not_Be_Null()
    {
        // Arrange
        var product = ProductTestData.GenerateValid();

        // Act
        product.Change("Modified name", 1, "Modified description", "https://image.jpg", new(), new() { Name = "Modified category" });

        // Assert
        product.Title.Should().Be("Modified name");
        product.Price.Should().Be(1);
        product.Description.Should().Be("Modified description");
        product.Image.Should().Be("https://image.jpg");
        product.Category.Should().NotBeNull();
        product.Category.Name.Should().Be("Modified category");
        product.UpdatedAt.Should().NotBeNull();
    }

    [Fact]
    public void Given_Product_When_Call_Same_Category_Name_Result_Should_Be_False()
    {
        // Arrange
        var product = ProductTestData.GenerateValid();

        // Act
        var isSameCategoryName = product.SameCategoryName("Modified name");

        // Assert
        isSameCategoryName.Should().BeFalse();
    }


    /// <summary>
    /// Tests that validation fails when product properties are invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid product data")]
    public void Given_InvalidProductData_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var product = new Product();

        // Act
        var result = product.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }
}
