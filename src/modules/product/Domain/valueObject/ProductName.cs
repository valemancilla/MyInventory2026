namespace MyInventory2026.src.modules.product.Domain.valueObject;

public sealed record ProductName
{
    public string Value { get; }

    private ProductName(string value)
    {
        Value = value;
    }

    public static ProductName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Product name cannot be empty.", nameof(value));
        }

        var trimmed = value.Trim();
        if (trimmed.Length < 3)
        {
            throw new ArgumentException("Product name must have at least 3 characters.", nameof(value));
        }

        if (trimmed.Length > 50)
        {
            throw new ArgumentException("Product name cannot exceed 50 characters.", nameof(value));
        }

        return new ProductName(trimmed);
    }

    public override string ToString() => Value;
}
