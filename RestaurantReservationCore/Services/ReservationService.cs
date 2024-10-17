using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Repositories;

namespace RestaurantReservationCore.Services
{
    public class ReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task GetAllReservationsAsync()
        {
            List<Reservation> reservations =  await _reservationRepository.GetAllAsync();
            if (!reservations.Any())
            {
                Console.WriteLine("No reservations found");
                return;
            }
            foreach (var reservation in reservations)
            {
                Console.WriteLine(reservation);
            }
        }

        public async Task GetReservationByIdAsync(int id)
        {
            Reservation reservation =  await _reservationRepository.GetByIdAsync(id);
            if (reservation == null)
            {
                Console.WriteLine("Reservation not found");
                return;
            }
            Console.WriteLine(reservation);
        }

        public async Task AddReservationAsync(Reservation reservation)
        {
            var existingReservation = await _reservationRepository.GetByIdAsync(reservation.ReservationId);
            if (existingReservation != null)
            {
                Console.WriteLine("Reservation already exists");
                return;
            }
            await _reservationRepository.AddAsync(reservation);
        }

        public async Task UpdateReservationAsync(int id,Reservation reservation)
        {
            var updatedReservation = await _reservationRepository.GetByIdAsync(id);
            if (updatedReservation == null)
            {
                Console.WriteLine("Reservation doesn't exist");
                return;
            }
            updatedReservation.ReservationDate = reservation.ReservationDate;
            updatedReservation.PartySize = reservation.PartySize;
            updatedReservation.CustomerId = reservation.CustomerId;
            updatedReservation.TableId = reservation.TableId;
            updatedReservation.RestaurantId = reservation.RestaurantId;
            await _reservationRepository.UpdateAsync(updatedReservation);
        }

        public async Task DeleteReservationAsync(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            if (reservation == null)
            {
                Console.WriteLine("Reservation doesn't exist");
                return;
            }
            await _reservationRepository.DeleteAsync(reservation);
        }

        public async Task GetAllReservationsByCustomerIdAsync(int customerId)
        {
            List<Reservation> reservations = await _reservationRepository.GetReservationsByCustomerIdAsync(customerId);
            if (!reservations.Any())
            {
                Console.WriteLine("No reservations found");
                return;
            }
            foreach (var reservation in reservations)
            {
                if (reservation.CustomerId == customerId)
                {
                    Console.WriteLine(reservation);
                }
            }
        }
    }
}
