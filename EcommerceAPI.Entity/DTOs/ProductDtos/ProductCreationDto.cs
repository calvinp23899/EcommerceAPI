
namespace EcommerceAPI.Entity.DTOs.ProductDtos
{
    public record ProductCreationDto(string ProductName, decimal Price, int Quantity, string Description, bool IsActive, bool IsHot);
}
