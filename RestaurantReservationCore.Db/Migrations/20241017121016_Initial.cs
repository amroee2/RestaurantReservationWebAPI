using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestaurantReservationCore.Db.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    RestaurantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpeningHours = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.RestaurantId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RestaurantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "RestaurantId");
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    MenuItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RestaurantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.MenuItemId);
                    table.ForeignKey(
                        name: "FK_MenuItems_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "RestaurantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    TableId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    RestaurantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.TableId);
                    table.ForeignKey(
                        name: "FK_Tables_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "RestaurantId");
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PartySize = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    TableId = table.Column<int>(type: "int", nullable: false),
                    RestaurantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_Reservations_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "RestaurantId");
                    table.ForeignKey(
                        name: "FK_Reservations_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "TableId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<double>(type: "float", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ReservationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "ReservationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    MenuItemId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItems_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "MenuItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "john.doe@example.com", "John", "Doe", "123456789" },
                    { 2, "jane.smith@example.com", "Jane", "Smith", "987654321" },
                    { 3, "alice.johnson@example.com", "Alice", "Johnson", null },
                    { 4, "bob.brown@example.com", "Bob", "Brown", "456123789" },
                    { 5, "charlie.white@example.com", "Charlie", "White", null },
                    { 6, "emily.davis@example.com", "Emily", "Davis", "789456123" },
                    { 7, "david.wilson@example.com", "David", "Wilson", null },
                    { 8, "grace.taylor@example.com", "Grace", "Taylor", "321654987" },
                    { 9, "ethan.anderson@example.com", "Ethan", "Anderson", null },
                    { 10, "olivia.moore@example.com", "Olivia", "Moore", "159753258" }
                });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "RestaurantId", "Address", "Name", "OpeningHours", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "123 Main St", "Gourmet Paradise", "9 AM - 10 PM", "555-1234" },
                    { 2, "456 Elm St", "Food Haven", "10 AM - 11 PM", "555-5678" },
                    { 3, "789 Oak St", "Taste Buds", "11 AM - 12 AM", "555-9876" },
                    { 4, "789 Elm St", "The Great Restaurant", "11 AM - 12 AM", "555-9876" },
                    { 5, "789 Main St", "The Best Restaurant", "11 AM - 12 AM", "555-9876" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "FirstName", "LastName", "Position", "RestaurantId" },
                values: new object[,]
                {
                    { 1, "Tom", "Jones", "Waiter", 1 },
                    { 2, "Sarah", "Connor", "Chef", 1 },
                    { 3, "Michael", "Scott", "Manager", 2 },
                    { 4, "Dwight", "Schrute", "Waiter", 2 },
                    { 5, "Pam", "Beesly", "Receptionist", 1 },
                    { 6, "Jim", "Halpert", "Assistant Manager", 2 },
                    { 7, "Angela", "Martin", "Chef", 3 },
                    { 8, "Stanley", "Hudson", "Waiter", 3 },
                    { 9, "Ryan", "Howard", "Waiter", 1 },
                    { 10, "Kelly", "Kapoor", "Waiter", 3 }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "MenuItemId", "Description", "Name", "Price", "RestaurantId" },
                values: new object[,]
                {
                    { 1, "Delicious cheese pizza", "Pizza", 12.99, 1 },
                    { 2, "Juicy beef burger", "Burger", 9.9900000000000002, 1 },
                    { 3, "Fresh garden salad", "Salad", 6.9900000000000002, 2 },
                    { 4, "Creamy Alfredo pasta", "Pasta", 10.99, 2 },
                    { 5, "Grilled steak", "Steak", 19.989999999999998, 3 },
                    { 6, "Fresh sushi rolls", "Sushi", 15.99, 3 },
                    { 7, "Hot chicken soup", "Soup", 5.9900000000000002, 1 },
                    { 8, "Spicy beef tacos", "Tacos", 8.9900000000000002, 2 },
                    { 9, "Vanilla ice cream", "Ice Cream", 3.9900000000000002, 3 },
                    { 10, "Crispy French fries", "Fries", 2.9900000000000002, 1 }
                });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "TableId", "Capacity", "RestaurantId" },
                values: new object[,]
                {
                    { 1, 4, 1 },
                    { 2, 2, 1 },
                    { 3, 6, 2 },
                    { 4, 8, 2 },
                    { 5, 10, 3 },
                    { 6, 4, 3 },
                    { 7, 6, 1 },
                    { 8, 2, 2 },
                    { 9, 8, 3 },
                    { 10, 4, 1 }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "ReservationId", "CustomerId", "PartySize", "ReservationDate", "RestaurantId", "TableId" },
                values: new object[,]
                {
                    { 1, 1, 4, new DateTime(2024, 10, 18, 15, 10, 15, 745, DateTimeKind.Local).AddTicks(5927), 1, 1 },
                    { 2, 2, 2, new DateTime(2024, 10, 19, 15, 10, 15, 745, DateTimeKind.Local).AddTicks(6005), 1, 2 },
                    { 3, 3, 6, new DateTime(2024, 10, 20, 15, 10, 15, 745, DateTimeKind.Local).AddTicks(6013), 2, 3 },
                    { 4, 4, 8, new DateTime(2024, 10, 21, 15, 10, 15, 745, DateTimeKind.Local).AddTicks(6019), 2, 4 },
                    { 5, 5, 10, new DateTime(2024, 10, 22, 15, 10, 15, 745, DateTimeKind.Local).AddTicks(6025), 3, 5 },
                    { 6, 6, 4, new DateTime(2024, 10, 23, 15, 10, 15, 745, DateTimeKind.Local).AddTicks(6031), 3, 6 },
                    { 7, 7, 6, new DateTime(2024, 10, 24, 15, 10, 15, 745, DateTimeKind.Local).AddTicks(6037), 1, 7 },
                    { 8, 8, 2, new DateTime(2024, 10, 25, 15, 10, 15, 745, DateTimeKind.Local).AddTicks(6044), 2, 8 },
                    { 9, 9, 8, new DateTime(2024, 10, 26, 15, 10, 15, 745, DateTimeKind.Local).AddTicks(6050), 3, 9 },
                    { 10, 10, 4, new DateTime(2024, 10, 27, 15, 10, 15, 745, DateTimeKind.Local).AddTicks(6057), 1, 10 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "EmployeeId", "OrderDate", "ReservationId", "TotalAmount" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 16, 15, 10, 15, 745, DateTimeKind.Local).AddTicks(6234), 1, 0.0 },
                    { 2, 2, new DateTime(2024, 10, 15, 15, 10, 15, 745, DateTimeKind.Local).AddTicks(6244), 2, 0.0 },
                    { 3, 3, new DateTime(2024, 10, 14, 15, 10, 15, 745, DateTimeKind.Local).AddTicks(6250), 3, 0.0 },
                    { 4, 4, new DateTime(2024, 10, 13, 15, 10, 15, 745, DateTimeKind.Local).AddTicks(6256), 4, 0.0 },
                    { 5, 9, new DateTime(2024, 10, 12, 15, 10, 15, 745, DateTimeKind.Local).AddTicks(6261), 5, 0.0 },
                    { 6, 7, new DateTime(2024, 10, 11, 15, 10, 15, 745, DateTimeKind.Local).AddTicks(6266), 6, 0.0 },
                    { 7, 8, new DateTime(2024, 10, 10, 15, 10, 15, 745, DateTimeKind.Local).AddTicks(6272), 7, 0.0 },
                    { 8, 1, new DateTime(2024, 10, 9, 15, 10, 15, 745, DateTimeKind.Local).AddTicks(6277), 8, 0.0 },
                    { 9, 2, new DateTime(2024, 10, 8, 15, 10, 15, 745, DateTimeKind.Local).AddTicks(6283), 9, 0.0 },
                    { 10, 6, new DateTime(2024, 10, 7, 15, 10, 15, 745, DateTimeKind.Local).AddTicks(6289), 10, 0.0 }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "OrderItemId", "MenuItemId", "OrderId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1, 2 },
                    { 2, 2, 1, 1 },
                    { 3, 3, 2, 3 },
                    { 4, 4, 2, 1 },
                    { 5, 5, 3, 2 },
                    { 6, 6, 3, 1 },
                    { 7, 7, 4, 4 },
                    { 8, 8, 5, 3 },
                    { 9, 9, 6, 2 },
                    { 10, 10, 7, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RestaurantId",
                table: "Employees",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_RestaurantId",
                table: "MenuItems",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_MenuItemId",
                table: "OrderItems",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EmployeeId",
                table: "Orders",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ReservationId",
                table: "Orders",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CustomerId",
                table: "Reservations",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_RestaurantId",
                table: "Reservations",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_TableId",
                table: "Reservations",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_RestaurantId",
                table: "Tables",
                column: "RestaurantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropTable(
                name: "Restaurants");
        }
    }
}
