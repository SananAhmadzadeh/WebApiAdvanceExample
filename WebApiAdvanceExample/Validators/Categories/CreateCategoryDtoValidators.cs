using FluentValidation;
using WebApiAdvanceExample.Entities.DTOs.CategoryDTOs;

namespace WebApiAdvanceExample.Validators.Categories
{
    public class CreateCategoryDtoValidators : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidators()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Ad hissəsi boş ola bilməz")
                .NotNull().WithMessage("Ad hissəsi null(dəyərsiz) ola bilməz")
                .MinimumLength(2).WithMessage("Ad hissəsi ən azı 2 simvol olmalıdır")
                .MaximumLength(100).WithMessage("Ad hissəsi ən çoxu 100 simvol ola bilər");


            RuleFor(p => p.Description)
                .MinimumLength(5).WithMessage("Təsvir hissəsi ən azı 5 simvol olmalıdır")
                .MaximumLength(500).WithMessage("Təsvir hissəsi ən çoxu 500 simvol ola bilər");

            RuleFor(p => p.Status)
                .IsInEnum().WithMessage("Status hissəsi düzgün dəyər olmalıdır");
        }
    }
}
