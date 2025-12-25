using FluentValidation;
using WebApiAdvanceExample.Entities.DTOs.OrderDTOs;

namespace WebApiAdvanceExample.Validators.Orders
{
    public class CreateOrderDtoValidators : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderDtoValidators()
        {
            RuleFor(o => o.TotalAmount)
                .NotEmpty().WithMessage("Qiymət hissəsi boş ola bilməz")
                .NotNull().WithMessage("Qiymət hissəsi null(dəyərsiz) ola bilməz")
                .GreaterThan(0).WithMessage("Qiymət hissəsi 0-dan böyük olmalıdır");
        }
    }
}
