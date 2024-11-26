using FluentValidation;
using RestaurantReservationServices.DTOs.OrderDTOs;

namespace RestaurantReservationServices.Validators.OrderValidators
{
    public class UpdateOrderValidator: AbstractValidator<OrderUpdateDTO>
    {
        public UpdateOrderValidator()
        {
            RuleFor(x => x.OrderDate)
                .NotEmpty()
                .WithMessage("Order Date is required")
                .GreaterThan(System.DateTime.Now)
                .WithMessage("Order Date must be greater than current date");
            RuleFor(x => x.TotalAmount)
                .NotEmpty()
                .WithMessage("Total Amount is required")
                .GreaterThan(0)
                .WithMessage("Total Amount must be greater than 0");
            RuleFor(x => x.EmployeeId)
                .NotEmpty()
                .WithMessage("Employee Id is required")
                .GreaterThan(0)
                .WithMessage("Employee Id must be greater than 0");
            RuleFor(x => x.ReservationId)
                .NotEmpty()
                .WithMessage("Reservation Id is required")
                .GreaterThan(0)
                .WithMessage("Reservation Id must be greater than 0");
        }
    }
}
