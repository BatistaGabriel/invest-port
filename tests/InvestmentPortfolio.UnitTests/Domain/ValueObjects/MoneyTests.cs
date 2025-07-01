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
        zero.IsZero().Should().BeTrue();
    }
}
