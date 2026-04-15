namespace Hex.Tpl.Project.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public DateTime CreatedAt { get; private set; }

    protected Product() { }

    public Product(string name, decimal price)
    {
        Id = Guid.NewGuid();
        Name = name;
        Price = price;
        CreatedAt = DateTime.UtcNow;

        Validate();
    }

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice <= 0)
            throw new DomainException("O preço deve ser maior que zero.");

        Price = newPrice;
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
            throw new DomainException("O nome do produto é obrigatório.");

        if (Price <= 0)
            throw new DomainException("O preço deve ser maior que zero.");
    }
}

public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
}
