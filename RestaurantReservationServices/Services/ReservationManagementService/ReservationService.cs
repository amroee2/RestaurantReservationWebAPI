using AutoMapper;
using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Repositories.ReservationManagement;
using RestaurantReservationCore.Db.Views;
using RestaurantReservationServices.DTOs.ReservationDTOs;
using RestaurantReservationServices.Exceptions;

namespace RestaurantReservationServices.Services.ReservationManagementService
{
    public class ReservationService: IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;
        public ReservationService(IReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        public async Task<List<ReservationReadDTO>> GetAllReservationsAsync()
        {
            var reservations = await _reservationRepository.GetAllAsync();
            return _mapper.Map<List<ReservationReadDTO>>(reservations);
        }

        public async Task<ReservationReadDTO> GetReservationByIdAsync(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            if (reservation == null)
            {
                throw new EntityNotFoundException("Reservation not found");
            }
            return _mapper.Map<ReservationReadDTO>(reservation);
        }

        public async Task<int> AddReservationAsync(ReservationCreateDTO reservation)
        {
            var newReservation = _mapper.Map<Reservation>(reservation);
            await _reservationRepository.AddAsync(newReservation);
            return newReservation.ReservationId;
        }

        public async Task UpdateReservationAsync(int id, ReservationUpdateDTO reservation)
        {
            var reservationToUpdate = await _reservationRepository.GetByIdAsync(id);
            _mapper.Map(reservation, reservationToUpdate);
            await _reservationRepository.UpdateAsync(reservationToUpdate);
        }

        public async Task DeleteReservationAsync(int id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            await _reservationRepository.DeleteAsync(reservation);
        }

        public async Task GetReservationsByCustomerIdAsync(int customerId)
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

        public async Task GetCustomerReservationsByRestaurantsAsync()
        {
            List<CustomerReservationsByRestaurant> customerReservationsByRestaurants = await _reservationRepository.GetCustomerReservationsByRestaurants();
            if (!customerReservationsByRestaurants.Any())
            {
                Console.WriteLine("No reservations found");
                return;
            }
            foreach (var customerReservationByRestaurant in customerReservationsByRestaurants)
            {
                Console.WriteLine(customerReservationByRestaurant);
            }
        }
    }
}