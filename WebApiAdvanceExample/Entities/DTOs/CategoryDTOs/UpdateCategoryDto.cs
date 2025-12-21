using WebApiAdvanceExample.Entities.Enums;

namespace WebApiAdvanceExample.Entities.DTOs.CategoryDTOs
{
    public record UpdateCategoryDto(
        string Name, 
        string? Description = null, 
        CategoryStatus? Status = null
    );
}
