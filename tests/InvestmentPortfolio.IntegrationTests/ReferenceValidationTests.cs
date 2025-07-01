using FluentAssertions;
using InvestmentPortfolio.Domain.ValueObjects;

namespace InvestmentPortfolio.IntegrationTests;

public class ReferenceValidationTests{
    [Fact]
    public void Should_Reference_All_Projects_Successfully(){
        typeof(Money).Should().NotBeNull();
    }
}