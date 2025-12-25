using FluentValidation;
using WebApiAdvanceExample.Entities.DTOs.OrderDTOs;

namespace WebApiAdvanceExample.Validators.Orders
{
    public class UpdateOrderDtoValidators : AbstractValidator<UpdateOrderDto>
    {
        public UpdateOrderDtoValidators() 
        {
            RuleFor(o => o.TotalAmount)
                   .NotEmpty().WithMessage("Qiymət hissəsi boş ola bilməz")
                   .NotNull().WithMessage("Qiymət hissəsi null(dəyərsiz) ola bilməz")
                   .GreaterThan(0).WithMessage("Qiymət hissəsi 0-dan böyük olmalıdır");
        }
    }
}
