using FluentValidation;
using WebApiAdvanceExample.Entities.DTOs.ProductDTOs;

namespace WebApiAdvanceExample.Validators.Products
{
    public class UpdateProductDtoValidators : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductDtoValidators()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Ad hissəsi boş ola bilməz")
                .NotNull().WithMessage("Ad hissəsi null(dəyərsiz) ola bilməz")
                .MinimumLength(2).WithMessage("Ad hissəsi ən azı 2 simvol olmalıdır")
                .MaximumLength(100).WithMessage("Ad hissəsi ən çoxu 100 simvol ola bilər");

            RuleFor(p => p.Description)
                .MinimumLength(10).WithMessage("Təsvir hissəsi ən azı 10 simvol olmalıdır")
                .MaximumLength(500).WithMessage("Təsvir hissəsi ən çoxu 500 simvol ola bilər");

            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("Qiymət hissəsi boş ola bilməz")
                .NotNull().WithMessage("Qiymət hissəsi null(dəyərsiz) ola bilməz")
                .GreaterThan(0).WithMessage("Qiymət hissəsi 0-dan böyük olmalıdır");

            RuleFor(p => p.Status)
                .IsInEnum()
                .WithMessage("Status dəyəri yanlışdır");

            RuleFor(p => p.DiscountPrice)
                 .GreaterThanOrEqualTo(0)
                 .When(p => p.DiscountPrice.HasValue)
                 .WithMessage("Endirimli qiymət 0-dan kiçik ola bilməz");

            RuleFor(p => p)
                .Must(p => !p.DiscountPrice.HasValue || p.DiscountPrice <= p.Price)
                .WithMessage("Endirimli qiymət əsas qiymətdən böyük ola bilməz");
        }
    }
}
