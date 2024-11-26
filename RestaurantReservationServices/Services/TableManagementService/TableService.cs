using AutoMapper;
using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Repositories;
using RestaurantReservationServices.DTOs.TableDTOs;
using RestaurantReservationServices.Exceptions;

namespace RestaurantReservationServices.Services.TableManagementService
{
    public class TableService: ITableService
    {
        private readonly IRepository<Table> _tableRepository;
        private readonly IMapper _mapper;
        public TableService(IRepository<Table> tableRepository, IMapper mapper)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
        }

        public async Task<List<TableReadDTO>> GetAllTablesAsync()
        {
            var tables = await _tableRepository.GetAllAsync();
            return _mapper.Map<List<TableReadDTO>>(tables);
        }

        public async Task<TableReadDTO> GetTableByIdAsync(int id)
        {
            var table = await _tableRepository.GetByIdAsync(id);
            if (table == null)
            {
                throw new EntityNotFoundException("Table doesn't exist");
            }
            return _mapper.Map<TableReadDTO>(table);
        }

        public async Task<int> AddTableAsync(TableCreateDTO table)
        {
            var newTable = _mapper.Map<Table>(table);
            await _tableRepository.AddAsync(newTable);
            return newTable.TableId;
        }

        public async Task UpdateTableAsync(int id, TableUpdateDTO table)
        {
            var tableToUpdate = await _tableRepository.GetByIdAsync(id);
            _mapper.Map(table, tableToUpdate);
            await _tableRepository.UpdateAsync(tableToUpdate);
        }

        public async Task DeleteTableAsync(int id)
        {
            var table = await _tableRepository.GetByIdAsync(id);
            await _tableRepository.DeleteAsync(table);
        }
    }
}