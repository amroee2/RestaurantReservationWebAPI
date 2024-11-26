using FluentValidation;
using RestaurantReservationServices.DTOs.EmployeeDTOs;

namespace RestaurantReservationServices.Validators.EmployeeValidators
{
    public class CreateEmployeeValidator: AbstractValidator<EmployeeCreateDTO>
    {
        public CreateEmployeeValidator()
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

            RuleFor(x => x.Position)
                .NotEmpty()
                .WithMessage("Position is required")
                .MaximumLength(50)
                .WithMessage("Position can't be longer than 50 characters");

            RuleFor(x => x.RestaurantId)
                .NotEmpty()
                .WithMessage("Restaurant id is required")
                .GreaterThan(0);
        }
    }
}
