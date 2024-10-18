using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservationCore.Db.Migrations
{
    /// <inheritdoc />

    public partial class addStoredProcedure : Migration
    {
        /// <inheritdoc />

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE OR ALTER PROCEDURE CustomersWithBigPartySize 
                    @PartySize INT 
                AS
                BEGIN
                    SELECT DISTINCT Customers.*
                    FROM Customers 
                    JOIN Reservations ON Reservations.CustomerId = Customers.CustomerId 
                    WHERE Reservations.PartySize > @PartySize;
                END
            ");
        }

        /// <inheritdoc />

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE CustomersWithBigPartySize");
        }
    }
}
