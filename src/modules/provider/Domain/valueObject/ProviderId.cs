namespace MyInventory2026.src.modules.provider.Domain.valueObject;

public sealed record ProviderId
{
    public string Value { get; }

    private ProviderId(string value)
    {
        Value = value;
    }

    public static ProviderId Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Provider id cannot be empty.", nameof(value));
        }

        return new ProviderId(value.Trim());
    }

    public override string ToString() => Value;
}