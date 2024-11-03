using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservationCore.Db.Migrations
{
    /// <inheritdoc />
    public partial class addFunctionCalculateRestaurantRevenue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                    CREATE OR ALTER FUNCTION dbo.CalculateRestaurantRevenue
                    (
	                    @RestaurantId INT
                    )
                    RETURNS DECIMAL
                    AS
                    BEGIN
                    DECLARE @TotalRevenue INT
                    SELECT @TotalRevenue = SUM(Orders.TotalAmount) 
                    FROM Restaurants
                    JOIN Employees ON Restaurants.RestaurantId = Employees.RestaurantId
                    JOIN Orders ON Employees.EmployeeId = Orders.OrderId
                    WHERE Restaurants.RestaurantId = @RestaurantId 

                    RETURN @TotalRevenue
                    END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION dbo.CalculateRestaurantRevenue");
        }
    }
}
