using WebApiAdvanceExample.Entities.Enums;

namespace WebApiAdvanceExample.Entities.DTOs.CategoryDTOs
{
    public record CreateCategoryDto(
        string Name,
        string? Description = null,
        CategoryStatus? Status = null
    );
}
