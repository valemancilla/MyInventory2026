namespace MyInventory2026.src.modules.product.Domain.valueObject;

public sealed record ProductCodeInv
{
    public string Value { get; }

    private ProductCodeInv(string value)
    {
        Value = value;
    }

    public static ProductCodeInv Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Product inventory code cannot be empty.", nameof(value));
        }

        var trimmed = value.Trim();
        if (trimmed.Length > 10)
        {
            throw new ArgumentException("Product inventory code cannot exceed 10 characters.", nameof(value));
        }

        return new ProductCodeInv(trimmed);
    }

    public override string ToString() => Value;
}
