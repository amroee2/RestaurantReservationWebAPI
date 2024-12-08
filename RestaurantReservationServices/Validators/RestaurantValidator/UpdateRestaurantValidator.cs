using FluentValidation;
using RestaurantReservationServices.DTOs.RestaurantDTOs;
using System.Data;

namespace RestaurantReservationServices.Validators.RestaurantValidator
{
    public class UpdateRestaurantValidator: AbstractValidator<RestaurantUpdateDTO>
    {

        public UpdateRestaurantValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(50).WithMessage("Name can't be longer than 50 characters");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required")
                .MaximumLength(100).WithMessage("Address can't be longer than 100 characters");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone number is required")
                .MaximumLength(15)
                .WithMessage("Phone number can't be longer than 15 characters");

            RuleFor(x => x.OpeningHours)
                .MaximumLength(50).WithMessage("Opening hours can't be longer than 50 characters");
        }
    }
}
