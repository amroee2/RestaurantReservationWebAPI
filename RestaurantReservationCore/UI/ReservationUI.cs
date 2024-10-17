
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

        public void DisplayOptions()
        {
            while (true)
            {
                const int getReservationsByCustomerIdOption = 6;
                Console.WriteLine("1. Add Reservation");
                Console.WriteLine("2. Update Reservation");
                Console.WriteLine("3. Delete Reservation");
                Console.WriteLine("4. View All Reservations");
                Console.WriteLine("5. View Reservation By Id");
                Console.WriteLine("6. View Reservations By Customer Id");
                Console.WriteLine("0. Go Back");

                string input = Console.ReadLine();
                if (Convert.ToInt32(input) == getReservationsByCustomerIdOption)
                {
                    GetReservationsByCustomerId();
                    continue;
                }
                try
                {
                    Enum.TryParse(input, out OperationOptions option);
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
            _reservationService.GetReservationByIdAsync(id);
        }

        private void ViewAllReservations()
        {
            _reservationService.GetAllReservationsAsync();
        }

        private void DeleteReservation()
        {
            Console.WriteLine("Enter reservation id:");
            int id = Convert.ToInt32(Console.ReadLine());
            _reservationService.DeleteReservationAsync(id);
        }

        private void UpdateReservation()
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
            _reservationService.UpdateReservationAsync(id, reservation);
        }

        private void AddReservation()
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
            _reservationService.AddReservationAsync(reservation);
        }

        private void GetReservationsByCustomerId()
        {
            Console.WriteLine("Enter Customer Id");
            int customerId = Convert.ToInt32(Console.ReadLine());
            _reservationService.GetAllReservationsByCustomerIdAsync(customerId);
        }
    }
}
