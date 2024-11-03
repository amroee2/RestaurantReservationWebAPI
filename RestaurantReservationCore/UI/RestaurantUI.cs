using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Enums;
using RestaurantReservationCore.Services;

namespace RestaurantReservationCore.UI
{
    public class RestaurantUI
    {
        private readonly RestaurantService _restaurantService;

        public RestaurantUI(RestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        public async Task DisplayOptionsAsync()
        {
            while (true)
            {
                const int calculateRestaurantRevenue = 6;
                Console.WriteLine("1. Add Restaurant");
                Console.WriteLine("2. Update Restaurant");
                Console.WriteLine("3. Delete Restaurant");
                Console.WriteLine("4. View All Restaurant");
                Console.WriteLine("5. View Restaurant By Id");
                Console.WriteLine("6. Calculate Restaurant Revenue");
                Console.WriteLine("0. Go Back");

                string input = Console.ReadLine();
                if (Convert.ToInt32(input) == calculateRestaurantRevenue)
                {
                    await CalculateRestaurantRevenueAsync();
                }
                try
                {
                    Enum.TryParse(input, out OperationOptions option);
                    if (option == OperationOptions.Exit)
                    {
                        return;
                    }
                    await HandleRequestAsync(option);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public async Task HandleRequestAsync(OperationOptions option)
        {
            switch (option)
            {
                case OperationOptions.Add:
                    await AddRestaurantAsync();
                    break;

                case OperationOptions.Update:
                    await UpdateResetaurantAsync();
                    break;

                case OperationOptions.Delete:
                    await DeleteRestaurantAsync();
                    break;

                case OperationOptions.View:
                    await ViewAllRestaurantAsync();
                    break;

                case OperationOptions.Search:
                    await ViewRestaurantByIdAsync();
                    break;
            }
        }

        private async Task ViewRestaurantByIdAsync()
        {
            Console.WriteLine("Enter restaurant id:");
            int id = Convert.ToInt32(Console.ReadLine());
            await _restaurantService.GetRestaurantByIdAsync(id);
        }

        private async Task ViewAllRestaurantAsync()
        {
            await _restaurantService.GetAllRestaurantsAsync();
        }

        private async Task DeleteRestaurantAsync()
        {
            Console.WriteLine("Enter restaurant id:");
            int id = Convert.ToInt32(Console.ReadLine());
            await _restaurantService.DeleteRestaurantAsync(id);
        }

        private async Task UpdateResetaurantAsync()
        {
            Console.WriteLine("Enter restaurant id");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter restaurant name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter restaurant address");
            string address = Console.ReadLine();
            Console.WriteLine("Enter restaurant phone number");
            string phoneNumber = Console.ReadLine();
            Console.WriteLine("Enter restaurant opening hours");
            string openingHours = Console.ReadLine();
            Restaurant restaurant = new Restaurant
            {
                Name = name,
                Address = address,
                PhoneNumber = phoneNumber,
                OpeningHours = openingHours
            };
            await _restaurantService.UpdateRestaurantAsync(id, restaurant);
        }

        private async Task AddRestaurantAsync()
        {
            Console.WriteLine("Enter restaurant name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter restaurant address");
            string address = Console.ReadLine();
            Console.WriteLine("Enter restaurant phone number");
            string phoneNumber = Console.ReadLine();
            Console.WriteLine("Enter restaurant opening hours");
            string openingHours = Console.ReadLine();
            Restaurant restaurant = new Restaurant
            {
                Name = name,
                Address = address,
                PhoneNumber = phoneNumber,
                OpeningHours = openingHours
            };
            await _restaurantService.AddRestaurantAsync(restaurant);
        }

        private async Task CalculateRestaurantRevenueAsync()
        {
            Console.WriteLine("Enter restaurant id:");
            int id = Convert.ToInt32(Console.ReadLine());
            await _restaurantService.CalculateRestaurantRevenueAsync(id);
        }
    }
}