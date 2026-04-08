namespace MyInventory2026.src.modules.product.Domain.valueObject;

public sealed record ProductId
{
    public int Value { get; }

    private ProductId(int value)
    {
        Value = value;
    }

    public static ProductId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("Product id must be greater than zero.", nameof(value));
        }

        return new ProductId(value);
    }

    public override string ToString() => Value.ToString();
}
