namespace MyInventory2026.src.modules.provider.Domain.valueObject;

public sealed record ProviderName
{
    public string Value { get; }

    private ProviderName(string value)
    {
        Value = value;
    }

    public static ProviderName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Provider name cannot be empty.", nameof(value));
        }

        if (value.Trim().Length < 3)
        {
            throw new ArgumentException("Provider name must have at least 3 characters.", nameof(value));
        }

        return new ProviderName(value.Trim());
    }

    public override string ToString() => Value;
}