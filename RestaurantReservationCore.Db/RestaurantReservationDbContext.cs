using Microsoft.EntityFrameworkCore;
using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Views;

namespace RestaurantReservationCore.Db
{
    public class RestaurantReservationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=RestaurantReservationSystem;Integrated Security=True");
        }

        public DbSet<CustomerReservationsByRestaurant> CustomerReservationsByRestaurants { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerReservationsByRestaurant>().HasNoKey().ToView("CustomersReservationByRestaurant");
            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", PhoneNumber = "123456789" },
                new Customer { CustomerId = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", PhoneNumber = "987654321" },
                new Customer { CustomerId = 3, FirstName = "Alice", LastName = "Johnson", Email = "alice.johnson@example.com" },
                new Customer { CustomerId = 4, FirstName = "Bob", LastName = "Brown", Email = "bob.brown@example.com", PhoneNumber = "456123789" },
                new Customer { CustomerId = 5, FirstName = "Charlie", LastName = "White", Email = "charlie.white@example.com" },
                new Customer { CustomerId = 6, FirstName = "Emily", LastName = "Davis", Email = "emily.davis@example.com", PhoneNumber = "789456123" },
                new Customer { CustomerId = 7, FirstName = "David", LastName = "Wilson", Email = "david.wilson@example.com" },
                new Customer { CustomerId = 8, FirstName = "Grace", LastName = "Taylor", Email = "grace.taylor@example.com", PhoneNumber = "321654987" },
                new Customer { CustomerId = 9, FirstName = "Ethan", LastName = "Anderson", Email = "ethan.anderson@example.com" },
                new Customer { CustomerId = 10, FirstName = "Olivia", LastName = "Moore", Email = "olivia.moore@example.com", PhoneNumber = "159753258" }
            );

            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeId = 1, FirstName = "Tom", LastName = "Jones", Position = "Waiter", RestaurantId = 1 },
                new Employee { EmployeeId = 2, FirstName = "Sarah", LastName = "Connor", Position = "Chef", RestaurantId = 1 },
                new Employee { EmployeeId = 3, FirstName = "Michael", LastName = "Scott", Position = "Manager", RestaurantId = 2 },
                new Employee { EmployeeId = 4, FirstName = "Dwight", LastName = "Schrute", Position = "Waiter", RestaurantId = 2 },
                new Employee { EmployeeId = 5, FirstName = "Pam", LastName = "Beesly", Position = "Receptionist", RestaurantId = 1 },
                new Employee { EmployeeId = 6, FirstName = "Jim", LastName = "Halpert", Position = "Assistant Manager", RestaurantId = 2 },
                new Employee { EmployeeId = 7, FirstName = "Angela", LastName = "Martin", Position = "Chef", RestaurantId = 3 },
                new Employee { EmployeeId = 8, FirstName = "Stanley", LastName = "Hudson", Position = "Waiter", RestaurantId = 3 },
                new Employee { EmployeeId = 9, FirstName = "Ryan", LastName = "Howard", Position = "Waiter", RestaurantId = 1 },
                new Employee { EmployeeId = 10, FirstName = "Kelly", LastName = "Kapoor", Position = "Waiter", RestaurantId = 3 }
            );

            modelBuilder.Entity<MenuItem>().HasData(
                new MenuItem { MenuItemId = 1, Name = "Pizza", Price = 12.99, Description = "Delicious cheese pizza", RestaurantId = 1 },
                new MenuItem { MenuItemId = 2, Name = "Burger", Price = 9.99, Description = "Juicy beef burger", RestaurantId = 1 },
                new MenuItem { MenuItemId = 3, Name = "Salad", Price = 6.99, Description = "Fresh garden salad", RestaurantId = 2 },
                new MenuItem { MenuItemId = 4, Name = "Pasta", Price = 10.99, Description = "Creamy Alfredo pasta", RestaurantId = 2 },
                new MenuItem { MenuItemId = 5, Name = "Steak", Price = 19.99, Description = "Grilled steak", RestaurantId = 3 },
                new MenuItem { MenuItemId = 6, Name = "Sushi", Price = 15.99, Description = "Fresh sushi rolls", RestaurantId = 3 },
                new MenuItem { MenuItemId = 7, Name = "Soup", Price = 5.99, Description = "Hot chicken soup", RestaurantId = 1 },
                new MenuItem { MenuItemId = 8, Name = "Tacos", Price = 8.99, Description = "Spicy beef tacos", RestaurantId = 2 },
                new MenuItem { MenuItemId = 9, Name = "Ice Cream", Price = 3.99, Description = "Vanilla ice cream", RestaurantId = 3 },
                new MenuItem { MenuItemId = 10, Name = "Fries", Price = 2.99, Description = "Crispy French fries", RestaurantId = 1 }
            );

            modelBuilder.Entity<Restaurant>().HasData(
                new Restaurant { RestaurantId = 1, Name = "Gourmet Paradise", Address = "123 Main St", PhoneNumber = "555-1234", OpeningHours = "9 AM - 10 PM" },
                new Restaurant { RestaurantId = 2, Name = "Food Haven", Address = "456 Elm St", PhoneNumber = "555-5678", OpeningHours = "10 AM - 11 PM" },
                new Restaurant { RestaurantId = 3, Name = "Taste Buds", Address = "789 Oak St", PhoneNumber = "555-9876", OpeningHours = "11 AM - 12 AM" },
                new Restaurant { RestaurantId = 4, Name = "The Great Restaurant", Address = "789 Elm St", PhoneNumber = "555-9876", OpeningHours = "11 AM - 12 AM" },
                new Restaurant { RestaurantId = 5, Name = "The Best Restaurant", Address = "789 Main St", PhoneNumber = "555-9876", OpeningHours = "11 AM - 12 AM" }
            );

            modelBuilder.Entity<Table>().HasData(
                new Table { TableId = 1, Capacity = 4, RestaurantId = 1 },
                new Table { TableId = 2, Capacity = 2, RestaurantId = 1 },
                new Table { TableId = 3, Capacity = 6, RestaurantId = 2 },
                new Table { TableId = 4, Capacity = 8, RestaurantId = 2 },
                new Table { TableId = 5, Capacity = 10, RestaurantId = 3 },
                new Table { TableId = 6, Capacity = 4, RestaurantId = 3 },
                new Table { TableId = 7, Capacity = 6, RestaurantId = 1 },
                new Table { TableId = 8, Capacity = 2, RestaurantId = 2 },
                new Table { TableId = 9, Capacity = 8, RestaurantId = 3 },
                new Table { TableId = 10, Capacity = 4, RestaurantId = 1 }
            );

            modelBuilder.Entity<Reservation>().HasData(
                new Reservation { ReservationId = 1, ReservationDate = DateTime.Now.AddDays(1), PartySize = 4, CustomerId = 1, TableId = 1, RestaurantId = 1 },
                new Reservation { ReservationId = 2, ReservationDate = DateTime.Now.AddDays(2), PartySize = 2, CustomerId = 2, TableId = 2, RestaurantId = 1 },
                new Reservation { ReservationId = 3, ReservationDate = DateTime.Now.AddDays(3), PartySize = 6, CustomerId = 3, TableId = 3, RestaurantId = 2 },
                new Reservation { ReservationId = 4, ReservationDate = DateTime.Now.AddDays(4), PartySize = 8, CustomerId = 4, TableId = 4, RestaurantId = 2 },
                new Reservation { ReservationId = 5, ReservationDate = DateTime.Now.AddDays(5), PartySize = 10, CustomerId = 5, TableId = 5, RestaurantId = 3 },
                new Reservation { ReservationId = 6, ReservationDate = DateTime.Now.AddDays(6), PartySize = 4, CustomerId = 6, TableId = 6, RestaurantId = 3 },
                new Reservation { ReservationId = 7, ReservationDate = DateTime.Now.AddDays(7), PartySize = 6, CustomerId = 7, TableId = 7, RestaurantId = 1 },
                new Reservation { ReservationId = 8, ReservationDate = DateTime.Now.AddDays(8), PartySize = 2, CustomerId = 8, TableId = 8, RestaurantId = 2 },
                new Reservation { ReservationId = 9, ReservationDate = DateTime.Now.AddDays(9), PartySize = 8, CustomerId = 9, TableId = 9, RestaurantId = 3 },
                new Reservation { ReservationId = 10, ReservationDate = DateTime.Now.AddDays(10), PartySize = 4, CustomerId = 10, TableId = 10, RestaurantId = 1 }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order { OrderId = 1, OrderDate = DateTime.Now.AddDays(-1), EmployeeId = 1, ReservationId = 1 },
                new Order { OrderId = 2, OrderDate = DateTime.Now.AddDays(-2), EmployeeId = 2, ReservationId = 2 },
                new Order { OrderId = 3, OrderDate = DateTime.Now.AddDays(-3), EmployeeId = 3, ReservationId = 3 },
                new Order { OrderId = 4, OrderDate = DateTime.Now.AddDays(-4), EmployeeId = 4, ReservationId = 4 },
                new Order { OrderId = 5, OrderDate = DateTime.Now.AddDays(-5), EmployeeId = 9, ReservationId = 5 },
                new Order { OrderId = 6, OrderDate = DateTime.Now.AddDays(-6), EmployeeId = 7, ReservationId = 6 },
                new Order { OrderId = 7, OrderDate = DateTime.Now.AddDays(-7), EmployeeId = 8, ReservationId = 7 },
                new Order { OrderId = 8, OrderDate = DateTime.Now.AddDays(-8), EmployeeId = 1, ReservationId = 8 },
                new Order { OrderId = 9, OrderDate = DateTime.Now.AddDays(-9), EmployeeId = 2, ReservationId = 9 },
                new Order { OrderId = 10, OrderDate = DateTime.Now.AddDays(-10), EmployeeId = 6, ReservationId = 10 }
            );

            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { OrderItemId = 1, Quantity = 2, OrderId = 1, MenuItemId = 1 },
                new OrderItem { OrderItemId = 2, Quantity = 1, OrderId = 1, MenuItemId = 2 },
                new OrderItem { OrderItemId = 3, Quantity = 3, OrderId = 2, MenuItemId = 3 },
                new OrderItem { OrderItemId = 4, Quantity = 1, OrderId = 2, MenuItemId = 4 },
                new OrderItem { OrderItemId = 5, Quantity = 2, OrderId = 3, MenuItemId = 5 },
                new OrderItem { OrderItemId = 6, Quantity = 1, OrderId = 3, MenuItemId = 6 },
                new OrderItem { OrderItemId = 7, Quantity = 4, OrderId = 4, MenuItemId = 7 },
                new OrderItem { OrderItemId = 8, Quantity = 3, OrderId = 5, MenuItemId = 8 },
                new OrderItem { OrderItemId = 9, Quantity = 2, OrderId = 6, MenuItemId = 9 },
                new OrderItem { OrderItemId = 10, Quantity = 5, OrderId = 7, MenuItemId = 10 }
            );

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Reservations)
                .WithOne(r => r.Customer)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Reservation>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Reservation)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Table>()
                .HasMany(t => t.Reservations)
                .WithOne(r => r.Table)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Restaurant>()
                .HasMany(r => r.Employees)
                .WithOne(e => e.Restaurant)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Restaurant>()
                .HasMany(r => r.Tables)
                .WithOne(t => t.Restaurant)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Restaurant>()
                .HasMany(r => r.MenuItems)
                .WithOne(m => m.Restaurant)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Restaurant>()
                .HasMany(r => r.Reservations)
                .WithOne(r => r.Restaurant)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Orders)
                .WithOne(o => o.Employee)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<MenuItem>()
                .HasMany(m => m.OrderItems)
                .WithOne(oi => oi.MenuItem)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}