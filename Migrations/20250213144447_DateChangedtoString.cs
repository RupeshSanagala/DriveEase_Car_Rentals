using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Car_Rental_Backend_Application.Migrations
{
    /// <inheritdoc />
    public partial class DateChangedtoString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    Admin_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(type: "VARCHAR(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "VARCHAR(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "VARCHAR(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.Admin_ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Car_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Brand = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Model = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    PricePerDay = table.Column<int>(type: "int", nullable: false),
                    License_Plate = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Availability_Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Car_ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Role = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OTP = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ResetTokenExpiry = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    User_ID = table.Column<int>(type: "int", nullable: false),
                    Car_ID = table.Column<int>(type: "int", nullable: false),
                    BookingDate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PickupDate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReturnDate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TotalPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_Cars_Car_ID",
                        column: x => x.Car_ID,
                        principalTable: "Cars",
                        principalColumn: "Car_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cancellations",
                columns: table => new
                {
                    Cancellation_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Booking_ID = table.Column<int>(type: "int", nullable: false),
                    Cancellation_Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Reason = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cancellations", x => x.Cancellation_ID);
                    table.ForeignKey(
                        name: "FK_Cancellations_Bookings_Booking_ID",
                        column: x => x.Booking_ID,
                        principalTable: "Bookings",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "Admin_ID", "Email", "Password", "Username" },
                values: new object[,]
                {
                    { 1, "devaravinay698.com", "Vinay@123", "devaravinay698" },
                    { 2, "narasimhagorla45@gmail.com", "Narasimha@123", "narasimhagorla45" },
                    { 3, "rupeshsanagala523@gmail.com", "Rupesh@123", "rupeshsanagala523" },
                    { 4, "ajaythella0@gmail.com", "Ajay@123", "ajaythella0" }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Car_ID", "Availability_Status", "Brand", "License_Plate", "Model", "PricePerDay", "Year" },
                values: new object[,]
                {
                    { 1, "Available", "Honda", "ABC123", "City", 50, 2022 },
                    { 2, "Available", "Hyundai", "XYZ456", "Creta", 55, 2021 },
                    { 3, "Available", "Hyundai", "HYU789", "i20", 40, 2023 },
                    { 4, "Available", "Mahindra", "MH300T", "TUV 300", 60, 2020 },
                    { 5, "Available", "Tata", "TAT456", "Punch", 45, 2022 },
                    { 6, "Available", "Suzuki", "SUZ123", "Celerio", 35, 2021 },
                    { 7, "Available", "Tata", "TIG789", "Tiago", 38, 2022 },
                    { 8, "Available", "Toyota", "TOY999", "Corolla", 55, 2019 },
                    { 9, "Available", "Mahindra", "BOL345", "Bolero", 50, 2020 },
                    { 10, "Available", "Chevrolet", "CHEV789", "Malibu", 65, 2018 },
                    { 11, "Available", "Maruti Suzuki", "ERT123", "Ertiga", 50, 2022 },
                    { 12, "Available", "Honda", "CIV567", "Civic", 70, 2021 },
                    { 13, "Available", "Toyota", "INN999", "Innova", 80, 2023 },
                    { 14, "Available", "Jeep", "JEEP123", "Compass", 75, 2020 },
                    { 15, "Available", "Kia", "KIA456", "Seltos", 60, 2022 },
                    { 16, "Available", "Mahindra", "MOR123", "Morrozo", 70, 2021 },
                    { 17, "Available", "Mahindra", "XUV700", "XUV700", 85, 2022 },
                    { 18, "Available", "Mahindra", "XUV300", "XUV300", 55, 2020 },
                    { 19, "Available", "Mahindra", "THAR999", "Thar", 90, 2023 },
                    { 20, "Available", "Maruti Suzuki", "CIAZ567", "Ciaz", 50, 2019 },
                    { 21, "Available", "Nissan", "ALTIMA1", "Altima", 75, 2021 },
                    { 22, "Available", "Tata", "ALT123", "Altroz Dark Edition", 60, 2022 },
                    { 23, "Available", "Tata", "SAFARI1", "Safari", 85, 2023 },
                    { 24, "Available", "Hyundai", "VERNA88", "Verna", 55, 2022 },
                    { 25, "Available", "Volkswagen", "JET789", "Jetta", 70, 2019 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admin_Email",
                table: "Admin",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_Car_ID",
                table: "Bookings",
                column: "Car_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_User_ID",
                table: "Bookings",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cancellations_Booking_ID",
                table: "Cancellations",
                column: "Booking_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_License_Plate",
                table: "Cars",
                column: "License_Plate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users",
                column: "PhoneNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Cancellations");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
