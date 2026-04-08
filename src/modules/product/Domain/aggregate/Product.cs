using MyInventory2026.src.modules.product.Domain.valueObject;

namespace MyInventory2026.src.modules.product.Domain.aggregate;

public sealed class Product
{
    public int Id { get; private set; }
    public ProductCodeInv CodeInv { get; private set; } = null!;
    public ProductName NameProduct { get; private set; } = null!;
    public int Stock { get; private set; }
    public int StockMin { get; private set; }
    public int StockMax { get; private set; }

    private Product()
    {
    }

    public static Product CreateNew(string codeInv, string nameProduct, int stock, int stockMin, int stockMax)
    {
        ValidateStocks(stock, stockMin, stockMax);
        return new Product
        {
            Id = 0,
            CodeInv = ProductCodeInv.Create(codeInv),
            NameProduct = ProductName.Create(nameProduct),
            Stock = stock,
            StockMin = stockMin,
            StockMax = stockMax
        };
    }

    public static Product Create(int id, string codeInv, string nameProduct, int stock, int stockMin, int stockMax)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Persisted product id must be greater than zero.", nameof(id));
        }

        ValidateStocks(stock, stockMin, stockMax);
        return new Product
        {
            Id = id,
            CodeInv = ProductCodeInv.Create(codeInv),
            NameProduct = ProductName.Create(nameProduct),
            Stock = stock,
            StockMin = stockMin,
            StockMax = stockMax
        };
    }

    private static void ValidateStocks(int stock, int stockMin, int stockMax)
    {
        if (stock < 0 || stockMin < 0 || stockMax < 0)
        {
            throw new ArgumentException("Stock values cannot be negative.");
        }

        if (stockMin > stockMax)
        {
            throw new ArgumentException("stock_min cannot be greater than stck_max.");
        }
    }
}
