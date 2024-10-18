using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservationCore.Db.Migrations
{
    /// <inheritdoc />
    public partial class addCustomerReservationsByRestaurantView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER VIEW CustomersReservationByRestaurant
	                            AS 
	                            SELECT Customers.FirstName + ' ' + Customers.LastName as CustomerName,
	                            Reservations.ReservationDate,
	                            Restaurants.Name
	                            FROM Customers
	                            JOIN Reservations ON Reservations.CustomerId = Customers.CustomerId
	                            JOIN Restaurants ON Reservations.RestaurantId = Restaurants.RestaurantId"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW CustomersReservationByRestaurant");
        }
    }
}
