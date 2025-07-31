﻿namespace Ordering.Domain.ValueObjects;

public record Payment
{
    public string CardName { get;  } = default!;
    public string CardNumber { get;  } = default!;
    public string ExpirationDate { get;  } = default!;
    public string CVV { get;  } = default!;
    public decimal PaymentMethod { get;  } = default!;
}
