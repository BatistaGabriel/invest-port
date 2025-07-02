using FluentAssertions;
using InvestmentPortfolio.Domain.ValueObjects;

namespace InvestmentPortfolio.UnitTests.Domain.ValueObjects;

public class MoneyTests
{
    [Fact]
    public void Should_Create_Money_When_Valid_Values()
    {
        //Arrange
        decimal amount = 100.50m;
        string currency = "BRL";

        //Act
        var money = Money.Create(amount, currency);

        //Assert
        money.Amount.Should().Be(100.50m);
        money.Currency.Should().Be("BRL");
    }

    [Fact]
    public void Should_Throw_ArgumentException_When_Amount_Is_Negative()
    {
        //Act & Assert
        Action act = () => Money.Create(-100.50m, "BRL");
        act.Should().Throw<ArgumentException>()
            .WithMessage("Amount cannot be negative. (Parameter 'amount')");
    }

    [Fact]
    public void Should_Throw_ArgumentException_When_Currency_Is_Null()
    {
        //Act & Assert
        Action act = () => Money.Create(100.50m, string.Empty);
        act.Should().Throw<ArgumentException>()
            .WithMessage("Currency cannot be null or empty. (Parameter 'currency')");
    }

    [Fact]
    public void Should_Throw_ArgumentException_When_Currency_Is_Invalid()
    {
        //Act & Assert
        Action act = () => Money.Create(100.50m, "USD");
        act.Should().Throw<ArgumentException>()
            .WithMessage("Currency must be BRL. (Parameter 'currency')");
    }

    [Fact]
    public void Should_Create_Zero_Money()
    {
        //Act
        var zero = Money.Zero();

        //Assert
        zero.Amount.Should().Be(0);
        zero.Currency.Should().Be("BRL");
        zero.IsZero.Should().BeTrue();
    }

    [Fact]
    public void Should_Be_Equal_When_Same_Amount_And_Currency()
    {
        //Arrange
        var money1 = Money.Create(100.50m, "BRL");
        var money2 = Money.Create(100.50m, "BRL");

        //Assert
        money1.Should().Be(money2);
        (money1 == money2).Should().BeTrue();
        money1.Equals(money2).Should().BeTrue();
    }

    [Fact]
    public void Should_Not_Be_Equal_When_Different_Amount()
    {
        //Arrange
        var money1 = Money.Create(100.50m, "BRL");
        var money2 = Money.Create(200.75m, "BRL");

        //Assert
        money1.Should().NotBe(money2);
        (money1 != money2).Should().BeTrue();
        money1.Equals(money2).Should().BeFalse();
    }

    [Fact(Skip = "Test skipped due to currency validation logic")]
    public void Should_Not_Be_Equal_When_Different_Currency()
    {
        //Arrange
        var money1 = Money.Create(100.50m, "BRL");
        var money2 = Money.Create(100.50m, "USD");

        //Assert
        money1.Should().NotBe(money2);
        (money1 != money2).Should().BeTrue();
        money1.Equals(money2).Should().BeFalse();
    }

    [Fact]
    public void Should_Not_Be_Equal_To_Null()
    {
        //Arrange
        var money = Money.Create(100.50m, "BRL");

        //Assert
        money.Should().NotBeNull();
        (money == null).Should().BeFalse();
        money.Equals(null).Should().BeFalse();
    }

    [Fact]
    public void Should_Handle_Both_Null_As_Equal()
    {
        //Arrange
        Money money1 = null;
        Money money2 = null;

        //Assert
        (money1 == money2).Should().BeTrue();
        Equals(money1, money2).Should().BeTrue();
    }

    [Fact]
    public void Should_Have_Same_HashCode_When_Equal()
    {
        //Arrange
        var money1 = Money.Create(100.50m, "BRL");
        var money2 = Money.Create(100.50m, "BRL");

        //Assert
        money1.GetHashCode().Should().Be(money2.GetHashCode());
    }

    [Fact]
    public void Should_Have_Different_HashCode_When_Not_Equal()
    {
        //Arrange
        var money1 = Money.Create(100.50m, "BRL");
        var money2 = Money.Create(200.75m, "BRL");

        //Assert
        money1.GetHashCode().Should().NotBe(money2.GetHashCode());
    }

    [Fact]
    public void Should_Add_Money_With_Same_Currency()
    {
        //Arrange
        var money1 = Money.Create(100.50m, "BRL");
        var money2 = Money.Create(50.25m, "BRL");

        //Act
        var result = money1.Add(money2);

        //Assert
        result.Amount.Should().Be(150.75m);
        result.Currency.Should().Be("BRL");
    }

    [Fact]
    public void Should_Throw_When_Adding_Null()
    {
        //Arrange
        var money = Money.Create(100.50m, "BRL");

        //Act & Assert
        Action act = () => money.Add(null);
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("Cannot add null Money. (Parameter 'other')");
    }

    [Fact]
    public void Should_Subtract_Money_Successfully()
    {
        //Arrange
        var money1 = Money.Create(100.50m, "BRL");
        var money2 = Money.Create(50.25m, "BRL");

        //Act
        var result = money1.Subtract(money2);

        //Assert
        result.Amount.Should().Be(50.25m);
        result.Currency.Should().Be("BRL");
    }

    [Fact]
    public void Should_Throw_When_Subtraction_Results_Negative()
    {
        //Arrange
        var money1 = Money.Create(50.25m, "BRL");
        var money2 = Money.Create(100.50m, "BRL");

        //Act & Assert
        Action act = () => money1.Subtract(money2);
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Subtraction cannot result in a negative amount.");
    }

    [Fact]
    public void Should_Multiply_By_Factor()
    {
        //Arrange
        var money = Money.Create(100.50m, "BRL");
        decimal factor = 2.0m;

        //Act
        var result = money.Multiply(factor);

        //Assert
        result.Amount.Should().Be(201.00m);
    }

    [Fact]
    public void Should_Divide_By_Factor()
    {
        //Arrange
        var money = Money.Create(100.50m, "BRL");
        decimal factor = 2.0m;

        //Act
        var result = money.Divide(factor);

        //Assert
        result.Amount.Should().Be(50.25m);
    }
}
