namespace Ordering.Domain.ValueObjects;

public record Payment
{
    public string CardName { get;  } = default!;
    public string CardNumber { get;  } = default!;
    public string ExpirationDate { get;  } = default!;
    public string CVV { get;  } = default!;
    public decimal PaymentMethod { get;  } = default!;

    protected Payment() { }

    private Payment(string cardName, string cardNumber, string expirationDate, string cvv, decimal paymentMethod)
    {
        CardName = cardName;
        CardNumber = cardNumber;
        ExpirationDate = expirationDate;
        CVV = cvv;
        PaymentMethod = paymentMethod;
    }

    public static Payment Of(string cardName, string cardNumber, string expirationDate, string cvv, decimal paymentMethod)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(cardName);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(cardNumber);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(cvv);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length, 3);
        return new Payment(cardName, cardNumber, expirationDate, cvv, paymentMethod);
    }
}
