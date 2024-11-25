using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Repositories.RestaurantManagement;

namespace RestaurantReservationServices.Services.RestaurantManagementService
{
    public class RestaurantService: IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task GetAllRestaurantsAsync()
        {
            var restaurants = await _restaurantRepository.GetAllAsync();
            if (!restaurants.Any())
            {
                Console.WriteLine("No restaurants found");
                return;
            }
            foreach (var restaurant in restaurants)
            {
                Console.WriteLine(restaurant);
            }
        }

        public async Task GetRestaurantByIdAsync(int id)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(id);
            if (restaurant == null)
            {
                Console.WriteLine("Restaurant doesn't exist");
                return;
            }
            Console.WriteLine(restaurant);
        }

        public async Task AddRestaurantAsync(Restaurant restaurant)
        {
            var existingRestaurant = await _restaurantRepository.GetByIdAsync(restaurant.RestaurantId);
            if (existingRestaurant != null)
            {
                Console.WriteLine("Restaurant already exists");
                return;
            }
            await _restaurantRepository.AddAsync(restaurant);
        }

        public async Task UpdateRestaurantAsync(int id, Restaurant restaurant)
        {
            var updatedRestaurant = await _restaurantRepository.GetByIdAsync(id);
            if (updatedRestaurant == null)
            {
                Console.WriteLine("Restaurant doesn't exist");
                return;
            }
            updatedRestaurant.Name = restaurant.Name;
            updatedRestaurant.Address = restaurant.Address;
            updatedRestaurant.PhoneNumber = restaurant.PhoneNumber;
            updatedRestaurant.OpeningHours = restaurant.OpeningHours;
            await _restaurantRepository.UpdateAsync(updatedRestaurant);
        }

        public async Task DeleteRestaurantAsync(int id)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(id);
            if (restaurant == null)
            {
                Console.WriteLine("Restaurant doesn't exist");
                return;
            }
            await _restaurantRepository.DeleteAsync(restaurant);
        }

        public async Task CalculateRestaurantRevenueAsync(int restaurantId)
        {
            var totalRevenue = await _restaurantRepository.CalculateRestaurantRevenueAsync(restaurantId);
            Console.WriteLine($"Total revenue for restaurant {restaurantId}: {totalRevenue}");
        }
    }
}