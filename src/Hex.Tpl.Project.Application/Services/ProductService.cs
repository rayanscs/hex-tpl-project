namespace Hex.Tpl.Project.Application.Services;

using Hex.Tpl.Project.Domain.Entities;
using Hex.Tpl.Project.Domain.Ports.Driven;
using Hex.Tpl.Project.Domain.Ports.Driving;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Product> CreateAsync(string name, decimal price)
    {
        var product = new Product(name, price);
        return await _repository.AddAsync(product);
    }

    public async Task<Product?> GetByIdAsync(Guid id)
        => await _repository.GetByIdAsync(id);

    public async Task<IEnumerable<Product>> GetAllAsync()
        => await _repository.GetAllAsync();

    public async Task UpdatePriceAsync(Guid id, decimal newPrice)
    {
        var product = await _repository.GetByIdAsync(id)
            ?? throw new DomainException("Produto não encontrado.");

        product.UpdatePrice(newPrice);
        await _repository.UpdateAsync(product);
    }

    public async Task DeleteAsync(Guid id)
        => await _repository.DeleteAsync(id);
}
