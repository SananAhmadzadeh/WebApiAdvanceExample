using WebApiAdvanceExample.Entities.Enums;

namespace WebApiAdvanceExample.Entities.DTOs.CategoryDTOs
{
    public record GetCategoryDto
    (
        string Name,
        string Description,
        CategoryStatus Status
    );
}
