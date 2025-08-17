namespace Ordering.Domain.ValueObjects;

public record OrderName
{
    private const int DefaultLength = 5;
    public string Value { get; }

    private OrderName(string value)
    {
        Value = value;
    }

    public static OrderName Of(string value)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(value);
        //ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, DefaultLength);
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("Order name cannot be empty.");
        }
        if (value.Length > 100)
        {
            throw new DomainException("Order name cannot exceed 100 characters.");
        }
        return new OrderName(value);
    }
}
