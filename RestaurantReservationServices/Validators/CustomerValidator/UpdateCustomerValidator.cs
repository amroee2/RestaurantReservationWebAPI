using FluentValidation;
using RestaurantReservationServices.DTOs.CustomerDTOs;

namespace RestaurantReservationServices.Validators.CustomerValidator
{
    public class UpdateCustomerValidator: AbstractValidator<CustomerUpdateDTO>
    {
        public UpdateCustomerValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("First name is required")
                .MaximumLength(50)
                .WithMessage("First name can't be longer than 50 characters");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Last name is required")
                .MaximumLength(50)
                .WithMessage("Last name can't be longer than 50 characters");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Email is not valid");

            RuleFor(x => x.PhoneNumber)
                .MaximumLength(15)
                .WithMessage("Phone number can't be longer than 15 characters");
        }
    }
}
