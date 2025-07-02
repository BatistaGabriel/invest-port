namespace InvestmentPortfolio.Domain.ValueObjects;

/// <summary>
/// Represents a monetary value with a specific currency.
/// This class encapsulates the amount and currency, ensuring that the amount is non-negative 
/// and has at most two decimal places, and that the currency is valid (currently only "BRL" is supported).
/// <br /><br />It provides methods to create instances of Money, check if the amount is zero, and validate the amount and currency values.
/// <br /><br />The class is immutable, meaning once an instance is created, its state cannot be changed.
/// </summary>
public class Money : IEquatable<Money>
{
    /// <summary>
    /// Gets the amount of money.
    /// <br />The amount is non-negative and has at most two decimal places.
    /// </summary>
    public decimal Amount { get; }
    /// <summary>
    /// Gets the currency of the money.
    /// <br />Currently, only "BRL" (Brazilian Real) is supported.
    /// </summary>
    public string Currency { get; }
    /// <summary>
    /// Checks if the amount of money is zero.
    /// </summary>
    public bool IsZero => Amount == 0;

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

    ///<summary>
    /// Factory method Creates a new instance of Money with the specified amount and currency. <br />
    /// <param name="amount"><br />The amount of money, must be non-negative and have at most two decimal places.</param>
    /// <param name="currency"><br />The currency of the money, must be "BRL".</param>
    /// <returns><br />A new instance of Money.</returns>
    ///</summary>
    public static Money Create(decimal amount, string currency)
    {
        ValidateAmount(amount);
        ValidateCurrency(currency);
        return new Money(amount, currency);
    }

    /// <summary>
    /// Factory method to create a Money instance with zero amount in the specified currency.
    /// </summary>
    /// <param name="currency"></param>
    /// <returns></returns>
    public static Money Zero(string currency = "BRL")
    {
        return new Money(0, currency);
    }

    /// <summary>
    /// Checks if the current instance is equal to another Money instance.
    /// </summary>
    public override bool Equals(object obj)
    {
        //Null check
        if (obj is null) return false;

        //Same reference check
        if (ReferenceEquals(this, obj)) return true;

        //Type check + delegate
        return obj is Money other && Equals(other);
    }

    /// <summary>
    /// Checks if the current instance is equal to another Money instance.
    /// <br />This method is used to compare two Money instances for equality based on their Amount and Currency
    /// </summary>
    public bool Equals(Money other)
    {
        //Null check
        if (other is null) return false;

        //Same reference check
        if (ReferenceEquals(this, other)) return true;

        //Value comparison
        return Amount == other.Amount && Currency == other.Currency;
    }

    /// <summary>
    /// Generates a hash code for the current instance.
    /// </summary>
    public override int GetHashCode()
    {
        //Using a tuple to generate a hash code based on Amount and Currency
        return HashCode.Combine(Amount, Currency);
    }

    /// <summary>
    /// Overloaded operator == to check if two Money instances are equal.
    /// </summary>
    public static bool operator ==(Money left, Money right)
    {
        //Handle nulls
        if (left is null && right is null) return true;
        if (left is null || right is null) return false;

        return left.Equals(right);
    }

    /// <summary>
    /// Overloaded operator != to check if two Money instances are not equal.
    /// </summary>
    public static bool operator !=(Money left, Money right)
    {
        return !(left == right);
    }
}
