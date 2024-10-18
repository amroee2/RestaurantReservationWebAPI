
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

        public void DisplayOptions()
        {
            while (true)
            {
                Console.WriteLine("1. Add Restaurant");
                Console.WriteLine("2. Update Restaurant");
                Console.WriteLine("3. Delete Restaurant");
                Console.WriteLine("4. View All Restaurant");
                Console.WriteLine("5. View Restaurant By Id");
                Console.WriteLine("0. Go Back");
                try
                {
                    Enum.TryParse(Console.ReadLine(), out OperationOptions option);
                    if (option == OperationOptions.Exit)
                    {
                        return;
                    }
                    HandleRequest(option);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void HandleRequest(OperationOptions option)
        {
            switch (option)
            {
                case OperationOptions.Add:
                    AddReservation();
                    break;
                case OperationOptions.Update:
                    UpdateReservation();
                    break;
                case OperationOptions.Delete:
                    DeleteReservation();
                    break;
                case OperationOptions.View:
                    ViewAllReservations();
                    break;
                case OperationOptions.Search:
                    ViewReservationById();
                    break;
            }
        }

        private void ViewReservationById()
        {
            Console.WriteLine("Enter reservation id:");
            int id = Convert.ToInt32(Console.ReadLine());
            _restaurantService.GetRestaurantByIdAsync(id);
        }

        private void ViewAllReservations()
        {
            _restaurantService.GetAllRestaurantsAsync();
        }

        private void DeleteReservation()
        {
            Console.WriteLine("Enter restaurant id:");
            int id = Convert.ToInt32(Console.ReadLine());
            _restaurantService.DeleteRestaurantAsync(id);
        }

        private void UpdateReservation()
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
            _restaurantService.UpdateRestaurantAsync(id, restaurant);
        }

        private void AddReservation()
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
            _restaurantService.AddRestaurantAsync(restaurant);
        }
    }
}
