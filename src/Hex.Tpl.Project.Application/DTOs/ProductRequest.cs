namespace Hex.Tpl.Project.Application.DTOs;

public record ProductRequest(string Name, decimal Price);
public record UpdatePriceRequest(decimal NewPrice);
