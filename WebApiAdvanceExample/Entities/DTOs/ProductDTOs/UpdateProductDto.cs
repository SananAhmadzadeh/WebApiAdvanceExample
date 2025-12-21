using WebApiAdvanceExample.Entities.Enums;

namespace WebApiAdvanceExample.Entities.DTOs.ProductDTOs
{
    public record UpdateProductDto(
        string Name,
        string? Description,
        decimal Price,
        decimal? DiscountPrice,
        ProductStatus? Status
    );
}
