namespace InvestmentPortfolio.Domain.ValueObjects;

public class Money
{
    public decimal Amount { get; }
    public string Currency { get; }
    public bool IsZero() => Amount == 0;

    private Money(decimal amount, string currency)
    {
        ValidateAmount(amount);
        ValidateCurrency(currency);

        Amount = amount;
        Currency = currency;
    }

    private static void ValidateAmount(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount cannot be negative.", nameof(amount));
        }

        var roundedAmount = Math.Round(amount, 2);
        if (roundedAmount != amount)
        {
            throw new ArgumentException("Amount must have at most two decimal places.", nameof(amount));
        }
    }

    private static void ValidateCurrency(string currency)
    {
        if (string.IsNullOrWhiteSpace(currency))
        {
            throw new ArgumentException("Currency cannot be null or empty.", nameof(currency));
        }

        if (currency != "BRL")
        {
            throw new ArgumentException("Currency must be BRL.", nameof(currency));
        }
    }

    public static Money Create(decimal amount, string currency)
    {
        ValidateAmount(amount);
        ValidateCurrency(currency);
        return new Money(amount, currency);
    }

    public static Money Zero(string currency = "BRL")
    {
        return new Money(0, currency);
    }
}
