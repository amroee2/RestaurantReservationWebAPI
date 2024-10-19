using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Enums;
using RestaurantReservationCore.Services;

namespace RestaurantReservationCore.UI
{
    public class ReservationUI
    {
        private readonly ReservationService _reservationService;

        public ReservationUI(ReservationService reservationRepository)
        {
            _reservationService = reservationRepository;
        }

        public async Task DisplayOptionsAsync()
        {
            while (true)
            {
                const int getReservationsByCustomerIdOption = 6;
                const int getCustomerReservationsByRestaurantOption = 7;
                Console.WriteLine("1. Add Reservation");
                Console.WriteLine("2. Update Reservation");
                Console.WriteLine("3. Delete Reservation");
                Console.WriteLine("4. View All Reservations");
                Console.WriteLine("5. View Reservation By Id");
                Console.WriteLine("6. View Reservations By Customer Id");
                Console.WriteLine("7. View Customer Reservations With Restaurant Name");
                Console.WriteLine("0. Go Back");

                string input = Console.ReadLine();
                if (Convert.ToInt32(input) == getReservationsByCustomerIdOption)
                {
                    await GetReservationsByCustomerIdAsync();
                    continue;
                }
                else if (Convert.ToInt32(input) == getCustomerReservationsByRestaurantOption)
                {
                    await GetCustomerReservationsByRestaurantAsync();
                    continue;
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
                    await AddReservationAsync();
                    break;

                case OperationOptions.Update:
                    await UpdateReservationAsync();
                    break;

                case OperationOptions.Delete:
                    await DeleteReservationAsync();
                    break;

                case OperationOptions.View:
                    await ViewAllReservationsAsync();
                    break;

                case OperationOptions.Search:
                    await ViewReservationByIdAsync();
                    break;
            }
        }

        private async Task ViewReservationByIdAsync()
        {
            Console.WriteLine("Enter reservation id:");
            int id = Convert.ToInt32(Console.ReadLine());
            await _reservationService.GetReservationByIdAsync(id);
        }

        private async Task ViewAllReservationsAsync()
        {
            await _reservationService.GetAllReservationsAsync();
        }

        private async Task DeleteReservationAsync()
        {
            Console.WriteLine("Enter reservation id:");
            int id = Convert.ToInt32(Console.ReadLine());
            await _reservationService.DeleteReservationAsync(id);
        }

        private async Task UpdateReservationAsync()
        {
            Console.WriteLine("Enter reservation id:");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter reservation date");
            DateTime reservationDate = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Enter number of guests");
            int partySize = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter customer id");
            int customerId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter table id");
            int tableId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter restaurant id");
            int restaurantId = Convert.ToInt32(Console.ReadLine());
            Reservation reservation = new Reservation
            {
                ReservationDate = reservationDate,
                PartySize = partySize,
                CustomerId = customerId,
                TableId = tableId,
                RestaurantId = restaurantId
            };
            await _reservationService.UpdateReservationAsync(id, reservation);
        }

        private async Task AddReservationAsync()
        {
            Console.WriteLine("Enter reservation date");
            DateTime reservationDate = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Enter number of guests");
            int partySize = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter customer id");
            int customerId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter table id");
            int tableId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter restaurant id");
            int restaurantId = Convert.ToInt32(Console.ReadLine());
            Reservation reservation = new Reservation
            {
                ReservationDate = reservationDate,
                PartySize = partySize,
                CustomerId = customerId,
                TableId = tableId,
                RestaurantId = restaurantId
            };
            await _reservationService.AddReservationAsync(reservation);
        }

        private async Task GetReservationsByCustomerIdAsync()
        {
            Console.WriteLine("Enter Customer Id");
            int customerId = Convert.ToInt32(Console.ReadLine());
            await _reservationService.GetReservationsByCustomerIdAsync(customerId);
        }

        private async Task GetCustomerReservationsByRestaurantAsync()
        {
            await _reservationService.GetCustomerReservationsByRestaurantsAsync();
        }
    }
}