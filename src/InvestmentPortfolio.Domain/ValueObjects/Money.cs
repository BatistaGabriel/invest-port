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

    private void ValidateSameCurrency(Money other)
    {
        if (Currency != other.Currency)
        {
            throw new InvalidOperationException($"Cannot perform operations with different currencies: {Currency} and {other.Currency}.");
        }
    }

    ///<summary>
    /// Factory method Creates a new instance of Money with the specified amount and currency. <br />
    ///</summary>
    /// <param name="amount"><br />The amount of money, must be non-negative and have at most two decimal places.</param>
    /// <param name="currency"><br />The currency of the money, must be "BRL".</param>
    /// <returns><br />A new instance of Money.</returns>
    public static Money Create(decimal amount, string currency)
    {
        ValidateAmount(amount);
        ValidateCurrency(currency);
        return new Money(amount, currency);
    }

    /// <summary>
    /// Factory method to create a Money instance with zero amount in the specified currency.
    /// </summary>
    /// <param name="currency"><br />The currency of the money, must be "BRL".</param>
    /// <returns><br />A new instance of Money with zero amount.</returns>
    public static Money Zero(string currency = "BRL")
    {
        return new Money(0, currency);
    }

    /// <summary>
    /// Checks if the current instance is equal to another Money instance.
    /// </summary>
    /// <remarks>
    /// This method overrides the default Equals method to provide a custom equality check for Money instances.
    /// </remarks>
    /// <inheritdoc />
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
    /// <param name="other">The other Money instance to compare with.</param>
    /// <returns><br />True if both instances have the same Amount and Currency, otherwise false.</returns>
    /// <inheritdoc />
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
    /// <remarks>
    /// This method overrides the default GetHashCode method to provide a custom hash code based on Amount and Currency.
    /// </remarks>
    /// <inheritdoc />
    /// <returns><br />A hash code that represents the current instance.</returns>  
    public override int GetHashCode()
    {
        //Using a tuple to generate a hash code based on Amount and Currency
        return HashCode.Combine(Amount, Currency);
    }

    /// <summary>
    /// Overloaded operator == to check if two Money instances are equal.
    /// </summary>
    /// <remarks>
    /// This operator allows for a more natural syntax when comparing Money instances.
    /// </remarks>
    /// <param name="left">The left Money instance.</param>
    /// <param name="right">The right Money instance.</param>
    /// <returns><br />True if both instances are equal, otherwise false.</returns>
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
    /// <remarks>
    /// This operator allows for a more natural syntax when comparing Money instances.
    /// </remarks>
    /// <param name="left">The left Money instance.</param>
    /// <param name="right">The right Money instance.</param>   
    /// <returns><br />True if both instances are not equal, otherwise false.</returns>
    public static bool operator !=(Money left, Money right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Adds another Money instance to the current instance.
    /// <br />The currencies must match, otherwise an exception is thrown.
    /// </summary>
    /// <param name="other">The Money instance to add.</param>
    /// <returns>A new Money instance with the summed amount.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the other Money instance is null.</exception>
    public Money Add(Money other)
    {
        if (other is null)
            throw new ArgumentNullException(nameof(other), "Cannot add null Money.");

        ValidateSameCurrency(other);
        return new Money(Amount + other.Amount, Currency);
    }

    /// <summary>
    /// Subtracts another Money instance from the current instance.
    /// <br />The currencies must match, otherwise an exception is thrown.
    /// </summary>
    /// <param name="other">The Money instance to subtract.</param>
    /// <returns>A new Money instance with the subtracted amount.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the other Money instance is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the subtraction results in a negative amount.</exception>
    public Money Subtract(Money other)
    {
        if (other is null)
            throw new ArgumentNullException(nameof(other), "Cannot subtract null Money.");

        ValidateSameCurrency(other);
        var result = Amount - other.Amount;
        if (result < 0)
            throw new InvalidOperationException("Subtraction cannot result in a negative amount.");

        return new Money(result, Currency);
    }

    /// <summary>
    /// Multiplies the current Money instance by a decimal factor.
    /// <br />The factor must be non-negative, otherwise an exception is thrown.
    /// </summary>
    /// <param name="factor">The decimal factor to multiply by.</param>
    /// <returns>A new Money instance with the multiplied amount.</returns>
    /// <exception cref="ArgumentException">Thrown when the factor is negative.</exception>
    public Money Multiply(decimal factor)
    {
        if (factor < 0)
            throw new ArgumentException("Factor cannot be negative.", nameof(factor));

        var result = Amount * factor;
        return new Money(Math.Round(result, 2), Currency);
    }
    
    /// <summary>
    /// Divides the current Money instance by a decimal divisor.
    /// </summary>
    /// <param name="divisor">The decimal divisor to divide by.</param>
    /// <returns>A new Money instance with the divided amount.</returns>
    /// <exception cref="ArgumentException">Thrown when the divisor is less than or equal to zero.</exception>
    public Money Divide(decimal divisor)
    {
        if (divisor <= 0)
            throw new ArgumentException("Divisor must be greater than zero.", nameof(divisor));

        var result = Amount / divisor;
        return new Money(Math.Round(result, 2), Currency);
    }
}
