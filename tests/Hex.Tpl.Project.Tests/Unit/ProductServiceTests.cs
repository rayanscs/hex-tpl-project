namespace Hex.Tpl.Project.Tests.Unit;

using Hex.Tpl.Project.Application.Services;
using Hex.Tpl.Project.Domain.Entities;
using Hex.Tpl.Project.Domain.Ports.Driven;
using Moq;

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _repositoryMock;
    private readonly ProductService _service;

    public ProductServiceTests()
    {
        _repositoryMock = new Mock<IProductRepository>();
        _service = new ProductService(_repositoryMock.Object);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnProduct_WhenValidInput()
    {
        // Arrange
        _repositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Product>()))
            .ReturnsAsync((Product p) => p);

        // Act
        var result = await _service.CreateAsync("Teclado Mecânico", 299.90m);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Teclado Mecânico", result.Name);
        Assert.Equal(299.90m, result.Price);
        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_ShouldThrow_WhenPriceIsZero()
    {
        // Act & Assert
        await Assert.ThrowsAsync<DomainException>(
            () => _service.CreateAsync("Produto Inválido", 0));
    }
}
