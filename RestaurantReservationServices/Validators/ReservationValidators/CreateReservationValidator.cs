using FluentValidation;
using RestaurantReservationServices.DTOs.ReservationDTOs;

namespace RestaurantReservationServices.Validators.ReservationValidators
{
    public class CreateReservationValidator: AbstractValidator<ReservationCreateDTO>
    {
        public CreateReservationValidator()
        {
            RuleFor(r => r.ReservationDate)
                .NotEmpty()
                .WithMessage("Reservation Date is required")
                .GreaterThan(System.DateTime.Now)
                .WithMessage("Reservation Date must be in the future");
            RuleFor(r => r.PartySize)
                .NotEmpty()
                .WithMessage("Party Size is required")
                .GreaterThan(0)
                .WithMessage("Party Size must be greater than 0");
            RuleFor(r => r.CustomerId)
                .NotEmpty()
                .WithMessage("Customer Id is required")
                .GreaterThan(0)
                .WithMessage("Customer Id must be greater than 0");
            RuleFor(r => r.TableId)
                .NotEmpty()
                .WithMessage("Table Id is required")
                .GreaterThan(0)
                .WithMessage("Table Id must be greater than 0");
            RuleFor(r => r.RestaurantId)
                .NotEmpty()
                .WithMessage("Restaurant Id is required")
                .GreaterThan(0)
                .WithMessage("Restaurant Id must be greater than 0");
        }
    }
}
