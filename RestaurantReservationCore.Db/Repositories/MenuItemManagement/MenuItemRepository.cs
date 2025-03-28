﻿using Microsoft.EntityFrameworkCore;
using RestaurantReservationCore.Db.DataModels;

namespace RestaurantReservationCore.Db.Repositories.MenuItemManagement
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly RestaurantReservationDbContext _context;

        public MenuItemRepository(RestaurantReservationDbContext context)
        {
            _context = context;
        }

        public async Task<List<MenuItem>> GetAllAsync()
        {
            return await _context.MenuItems.AsNoTracking().ToListAsync();
        }

        public async Task<MenuItem> GetByIdAsync(int id)
        {
            return await _context.MenuItems.AsNoTracking().FirstOrDefaultAsync(m => m.MenuItemId == id);
        }

        public async Task AddAsync(MenuItem menuItem)
        {
            await _context.MenuItems.AddAsync(menuItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MenuItem menuItem)
        {
            _context.MenuItems.Update(menuItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(MenuItem menuItem)
        {
            _context.MenuItems.Remove(menuItem);
            await _context.SaveChangesAsync();
        }

        public async Task<List<MenuItem>> GetMenuItemsByReservationIdAsync(int reservationId)
        {
            var menuItems = await _context.Orders.AsNoTracking().Where(o => o.ReservationId == reservationId)
                .Include(o => o.OrderItems)
                .ThenInclude(o => o.MenuItem)
                .SelectMany(order => order.OrderItems.Select(oi => oi.MenuItem))
                .ToListAsync();
            return menuItems;
        }
    }
}