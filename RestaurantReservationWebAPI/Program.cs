using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using RestaurantReservationCore.Db;
using RestaurantReservationCore.Db.DataModels;
using RestaurantReservationCore.Db.Repositories;
using RestaurantReservationCore.Db.Repositories.CustomerManagement;
using RestaurantReservationCore.Db.Repositories.EmployeeManagement;
using RestaurantReservationCore.Db.Repositories.MenuItemManagement;
using RestaurantReservationCore.Db.Repositories.OrderManagement;
using RestaurantReservationCore.Db.Repositories.ReservationManagement;
using RestaurantReservationCore.Db.Repositories.RestaurantManagement;
using RestaurantReservationServices.Services.CustomerManagementService;
using RestaurantReservationServices.Services.EmployeeManagementService;
using RestaurantReservationServices.Services.MenuItemManagementService;
using RestaurantReservationServices.Services.OrderItemManagementService;
using RestaurantReservationServices.Services.OrderManagementService;
using RestaurantReservationServices.Services.ReservationManagementService;
using RestaurantReservationServices.Services.RestaurantManagementService;
using RestaurantReservationServices.Services.TableManagementService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
                .AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<RestaurantReservationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var externalAssembly = AppDomain.CurrentDomain.Load("RestaurantReservationServices");
builder.Services.AddFluentValidationAutoValidation()
                .AddValidatorsFromAssembly(externalAssembly);

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IMenuItemService, MenuItemService>();
builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
builder.Services.AddScoped<ITableService, TableService>();
builder.Services.AddScoped<IRepository<Table>, TableRepository>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<IRepository<OrderItem>, OrderItemRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
