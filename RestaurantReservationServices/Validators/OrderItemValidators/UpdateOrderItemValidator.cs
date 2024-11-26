using FluentValidation;
using RestaurantReservationServices.DTOs.OrderItemDTOs;

namespace RestaurantReservationServices.Validators.OrderItemValidators
{
    public class UpdateOrderItemValidator: AbstractValidator<OrderItemUpdateDTO>
    {
        public UpdateOrderItemValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty()
                .WithMessage("Order Id is required")
                .GreaterThan(0)
                .WithMessage("Order Id must be greater than 0");
            RuleFor(x => x.MenuItemId)
                .NotEmpty()
                .WithMessage("Menu Item Id is required")
                .GreaterThan(0)
                .WithMessage("Menu Item Id must be greater than 0");
            RuleFor(x => x.Quantity)
                .NotEmpty()
                .WithMessage("Quantity is required")
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0");
        }
    }
}
