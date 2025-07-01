using FluentAssertions;
using InvestmentPortfolio.Domain.ValueObjects;

namespace InvestmentPortfolio.UnitTests;

public class ReferenceValidationTests{
    [Fact]
    public void Should_Reference_Domain_Successfully(){
        var moneyType = typeof(Money);
        moneyType.Should().NotBeNull();
        moneyType.Namespace.Should().Be("InvestmentPortfolio.Domain.ValueObjects");
    }
}