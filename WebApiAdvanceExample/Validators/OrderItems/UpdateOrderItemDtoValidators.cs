using FluentValidation;
using WebApiAdvanceExample.Entities.DTOs.OrderItemDTOs;

namespace WebApiAdvanceExample.Validators.OrderItem
{
    public class UpdateOrderItemDtoValidators : AbstractValidator<UpdateOrderItemDto>
    {
        public UpdateOrderItemDtoValidators()
        {
            RuleFor(oi => oi.ProductId)
               .NotEmpty().WithMessage("Product seçilməlidir");

            RuleFor(oi => oi.OrderId)
                .NotEmpty().WithMessage("Order seçilməlidir");

            RuleFor(oi => oi.Description)
                .MinimumLength(10).WithMessage("Təsvir hissəsi ən azı 10 simvol olmalıdır")
                .MaximumLength(500).WithMessage("Təsvir hissəsi ən çoxu 500 simvol ola bilər");

            RuleFor(oi => oi.Quantity)
                .NotEmpty().WithMessage("Ədəd hissəsi boş ola bilməz")
                .NotNull().WithMessage("Ədəd hissəsi null(dəyərsiz) ola bilməz")
                .GreaterThan(0).WithMessage("Ədəd hissəsi 0-dan böyük olmalıdır");

            RuleFor(oi => oi.UnitPrice)
                .NotEmpty().WithMessage("Qiymət hissəsi boş ola bilməz")
                .NotNull().WithMessage("Qiymət hissəsi null(dəyərsiz) ola bilməz")
                .GreaterThan(0).WithMessage("Qiymət hissəsi 0-dan böyük olmalıdır");
        }
    }
}
