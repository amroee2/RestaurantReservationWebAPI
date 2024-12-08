using FluentValidation;
using RestaurantReservationServices.DTOs.TableDTOs;

namespace RestaurantReservationServices.Validators.TableValidators
{
    public class CreateTableValidator: AbstractValidator<TableCreateDTO>
    {
        public CreateTableValidator()
        {
            RuleFor(x => x.Capacity)
                .NotEmpty()
                .WithMessage("Capacity is required")
                .GreaterThan(0).WithMessage("Capacity must be greater than 0")
                .LessThan(20).WithMessage("Capacity must be less than 20");

            RuleFor(x => x.RestaurantId)
                .NotEmpty()
                .WithMessage("Restaurant Id is required")
                .GreaterThan(0)
                .WithMessage("Restaurant Id must be greater than 0");
        }
    }
}
