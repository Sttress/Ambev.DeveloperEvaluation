using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class CartTests
{
    [Fact]
    public void Given_Cart_When_Add_One_Items_Should_Have_One()
    {
        // Arrange
        var cart = CartTestData.GenerateValid();
        var cartItem = CartItemTestData.GenerateValid();

        // Act
        cart.AddItems(cartItem);

        // Assert
        cart.Items.Should().HaveCount(1);
        cart.TotalSaleAmount.Should().Be(cartItem.Quantity * cartItem.UnitPrice);
    }

    [Fact]
    public void Given_Cart_When_Add_Two_Items_Should_Have_Two()
    {
        // Arrange
        var cart = CartTestData.GenerateValid();
        var cartItem1 = CartItemTestData.GenerateValid();
        var cartItem2 = CartItemTestData.GenerateValid();

        // Act
        cart.AddItems(cartItem1, cartItem2);

        // Assert
        cart.Items.Should().HaveCount(2);
        cart.TotalSaleAmount.Should().Be((cartItem1.Quantity * cartItem1.UnitPrice) + (cartItem2.Quantity * cartItem2.UnitPrice));
    }


    [Fact]
    public void Given_Cart_With_Item_When_Cancel_Purchase_Status_Should_Be_Cancelled()
    {
        // Arrange
        var cart = CartTestData.GenerateValid();
        var cartItem = CartItemTestData.GenerateValid();
        cart.AddItems(cartItem);

        // Act
        cart.Cancel(cart.BoughtBy);

        // Assert
        cartItem.PurchaseStatus.Should().Be(PurchaseStatus.Cancelled);
        cartItem.CancelledBy.Should().Be(cart.BoughtBy);
        cartItem.CancelledAt.Should().NotBeNull();
        cartItem.UpdatedAt.Should().NotBeNull();
    }


    [Fact]
    public void Given_Cart_When_Try_Change_With_Null_User_Should_Be_Modified()
    {
        // Arrange
        var customer = UserTestData.GenerateValidUser();
        var cart = CartTestData.GenerateValid();
        var cartItem = CartItemTestData.GenerateValid();
        cart.AddItems(cartItem);

        // Act
        Action method = () => cart.Change(null!, DateTime.UtcNow, "New branch store");

        // Assert
        method.Should()
            .Throw<ArgumentNullException>();
    }

    [Fact]
    public void Given_Cart_When_Change_Product_Infos_Should_Be_Modified()
    {
        // Arrange
        var customer = UserTestData.GenerateValidUser();
        var cart = CartTestData.GenerateValid();
        var cartItem = CartItemTestData.GenerateValid();
        cart.AddItems(cartItem);

        // Act
        cart.Change(customer, DateTime.UtcNow, "New branch store");

        // Assert
        cart.StoreName.Should().Be("New branch store");
        cart.SoldAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        cart.BoughtBy.Should().Be(customer);
        cart.BoughtById.Should().Be(customer.Id);
        cart.UpdatedAt.Should().NotBeNull();
    }

    [Fact]
    public void Given_Cart_When_Try_Delete_With_Null_User_Should_Be_Modified()
    {
        // Arrange
        var cart = CartTestData.GenerateValid();

        // Act
        Action method = () => cart.Delete(null!);

        // Assert
        method.Should()
            .Throw<ArgumentNullException>();
    }

    [Fact]
    public void Given_Cart_When_Delete_Purchase_Status_Should_Be_Deleted()
    {
        // Arrange
        var customer = UserTestData.GenerateValidUser();
        var cart = CartTestData.GenerateValid();
        var cartItem = CartItemTestData.GenerateValid();
        cart.AddItems(cartItem);

        // Act
        cart.Delete(customer);

        // Assert
        cart.CancelledAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        cart.CancelledBy.Should().Be(customer);
        cart.PurchaseStatus.Should().Be(PurchaseStatus.Deleted);
        cart.DeletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        cart.DeletedBy.Should().Be(customer);
        cart.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void Given_Cart_With_Items_When_Refresh_Total_Amount_Should_Be_Updated()
    {
        // Arrange
        var cart = CartTestData.GenerateValid();
        var cartItem = CartItemTestData.GenerateValid();
        cart.Items.Add(cartItem);

        // Act
        cart.RefreshTotalAmount();

        // Assert
        cart.TotalSaleAmount.Should().Be(cartItem.UnitPrice * cartItem.Quantity);
    }

    [Fact]
    public void Given_Cart_With_Three_Items_When_Delete_Items_Total_Should_Be_Updated()
    {
        // Arrange
        var cart = CartTestData.GenerateValid();
        var cartItem1 = CartItemTestData.GenerateValid();
        var cartItem2 = CartItemTestData.GenerateValid();
        var cartItem3 = CartItemTestData.GenerateValid();
        cart.AddItems(cartItem1, cartItem2, cartItem3);

        // Act
        cart.DeleteItems(cart.BoughtBy, cartItem1, cartItem2);

        // Assert
        cart.Items.Should().HaveCount(3);
        cart.TotalSaleAmount.Should().Be(cartItem3.UnitPrice * cartItem3.Quantity);

        cartItem1.PurchaseStatus.Should().Be(PurchaseStatus.Deleted);
        cartItem2.PurchaseStatus.Should().Be(PurchaseStatus.Deleted);
        cartItem3.PurchaseStatus.Should().Be(PurchaseStatus.Created);
    }



    [Fact]
    public void Given_Cart_With_Three_Items_When_Delete_Removed_Items_Should_Be_Deleted()
    {
        // Arrange
        var cart = CartTestData.GenerateValid();
        var cartItem1 = CartItemTestData.GenerateValid();
        var cartItem2 = CartItemTestData.GenerateValid();
        var cartItem3 = CartItemTestData.GenerateValid();
        cart.AddItems(cartItem1, cartItem2, cartItem3);

        // Act
        cart.DeleteItems(cart.BoughtBy, cartItem1, cartItem2);

        // Assert
        cartItem1.PurchaseStatus.Should().Be(PurchaseStatus.Deleted);
        cartItem2.PurchaseStatus.Should().Be(PurchaseStatus.Deleted);
        cartItem3.PurchaseStatus.Should().Be(PurchaseStatus.Created);
    }

}
