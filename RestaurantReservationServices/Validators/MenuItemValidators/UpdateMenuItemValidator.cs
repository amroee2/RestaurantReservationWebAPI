using FluentValidation;
using RestaurantReservationServices.DTOs.MenuItemDTOs;

namespace RestaurantReservationServices.Validators.MenuItemValidators
{
    public class UpdateMenuItemValidator: AbstractValidator<MenuItemUpdateDTO>
    {
        public UpdateMenuItemValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Price)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.RestaurantId)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
