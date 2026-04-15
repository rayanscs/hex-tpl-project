namespace Hex.Tpl.Project.Domain.Ports.Driving;

using Hex.Tpl.Project.Domain.Entities;

public interface IProductService
{
    Task<Product> CreateAsync(string name, decimal price);
    Task<Product?> GetByIdAsync(Guid id);
    Task<IEnumerable<Product>> GetAllAsync();
    Task UpdatePriceAsync(Guid id, decimal newPrice);
    Task DeleteAsync(Guid id);
}
