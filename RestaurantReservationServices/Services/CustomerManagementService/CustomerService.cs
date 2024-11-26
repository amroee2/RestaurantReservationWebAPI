using AutoMapper;
using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Repositories.CustomerManagement;
using RestaurantReservationServices.DTOs.CustomerDTOs;
using RestaurantReservationServices.Exceptions;

namespace RestaurantReservationServices.Services.CustomerManagementService
{
    public class CustomerService: ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        private readonly IMapper _mapper;
        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<List<CustomerReadDTO>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return _mapper.Map<List<CustomerReadDTO>>(customers);
        }

        public async Task<CustomerReadDTO> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
            {
                throw new EntityNotFoundException("Customer not found");
            }
            return _mapper.Map<CustomerReadDTO>(customer);
        }

        public async Task<int> AddCustomerAsync(CustomerCreateDTO customer)
        {
            var newCustomer = _mapper.Map<Customer>(customer);
            await _customerRepository.AddAsync(newCustomer);
            return newCustomer.CustomerId;
        }

        public async Task UpdateCustomerAsync(int Id, CustomerUpdateDTO customer)
        {
            var customerToUpdate = await _customerRepository.GetByIdAsync(Id);
            if (customer == null)
            {
                throw new EntityNotFoundException("Customer not found");
            }
            _mapper.Map(customer, customerToUpdate);
            await _customerRepository.UpdateAsync(customerToUpdate);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
            {
                throw new EntityNotFoundException("Customer not found");
            }
            await _customerRepository.DeleteAsync(customer);
        }

        public async Task<List<CustomerReadDTO>> GetCustomersWithBigPartySizeAsync(int partySize)
        {
            var customers = await _customerRepository.GetCustomersWithBigPartySizeAsync(partySize);
            return _mapper.Map<List<CustomerReadDTO>>(customers);
        }
    }
}