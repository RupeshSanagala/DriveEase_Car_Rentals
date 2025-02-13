using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car_Rental_Backend_Application.Migrations
{
    /// <inheritdoc />
    public partial class CategoryAndLocationAddedInCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Cars",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Cars",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Car_ID",
                keyValue: 1,
                columns: new[] { "Category", "Location" },
                values: new object[] { "Sedan", "Mumbai" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Car_ID",
                keyValue: 2,
                columns: new[] { "Category", "Location" },
                values: new object[] { "SUV", "Delhi" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Car_ID",
                keyValue: 3,
                columns: new[] { "Category", "Location" },
                values: new object[] { "Hatchback", "Bangalore" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Car_ID",
                keyValue: 4,
                columns: new[] { "Category", "Location" },
                values: new object[] { "SUV", "Chennai" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Car_ID",
                keyValue: 5,
                columns: new[] { "Category", "Location" },
                values: new object[] { "Hatchback", "Kolkata" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Car_ID",
                keyValue: 6,
                columns: new[] { "Category", "Location" },
                values: new object[] { "Hatchback", "Hyderabad" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Car_ID",
                keyValue: 7,
                columns: new[] { "Category", "Location" },
                values: new object[] { "Hatchback", "Pune" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Car_ID",
                keyValue: 8,
                columns: new[] { "Category", "Location" },
                values: new object[] { "Sedan", "Ahmedabad" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Car_ID",
                keyValue: 9,
                columns: new[] { "Category", "Location" },
                values: new object[] { "SUV", "Jaipur" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Car_ID",
                keyValue: 10,
                columns: new[] { "Category", "Location" },
                values: new object[] { "Luxury", "Surat" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Car_ID",
                keyValue: 11,
                columns: new[] { "Category", "Location" },
                values: new object[] { "MUV", "Lucknow" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Car_ID",
                keyValue: 12,
                columns: new[] { "Category", "Location" },
                values: new object[] { "Sedan", "Nagpur" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Car_ID",
                keyValue: 13,
                columns: new[] { "Category", "Location" },
                values: new object[] { "MUV", "Indore" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Car_ID",
                keyValue: 14,
                columns: new[] { "Category", "Location" },
                values: new object[] { "SUV", "Patna" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Car_ID",
                keyValue: 15,
                columns: new[] { "Category", "Location" },
                values: new object[] { "SUV", "Bhopal" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Car_ID",
                keyValue: 16,
                columns: new[] { "Category", "Location" },
                values: new object[] { "SUV", "Vadodara" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Car_ID",
                keyValue: 17,
                columns: new[] { "Category", "Location" },
                values: new object[] { "SUV", "Ludhiana" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Car_ID",
                keyValue: 18,
                columns: new[] { "Category", "Location" },
                values: new object[] { "SUV", "Agra" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Car_ID",
                keyValue: 19,
                columns: new[] { "Category", "Location" },
                values: new object[] { "SUV", "Nashik" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Car_ID",
                keyValue: 20,
                columns: new[] { "Category", "Location" },
                values: new object[] { "Sedan", "Meerut" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Car_ID",
                keyValue: 21,
                columns: new[] { "Category", "Location" },
                values: new object[] { "Sedan", "Rajkot" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Car_ID",
                keyValue: 22,
                columns: new[] { "Category", "Location" },
                values: new object[] { "Hatchback", "Jamshedpur" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Car_ID",
                keyValue: 23,
                columns: new[] { "Category", "Location" },
                values: new object[] { "SUV", "Amritsar" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Car_ID",
                keyValue: 24,
                columns: new[] { "Category", "Location" },
                values: new object[] { "Sedan", "Jodhpur" });

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Car_ID",
                keyValue: 25,
                columns: new[] { "Category", "Location" },
                values: new object[] { "Luxury", "Dehradun" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Cars");
        }
    }
}
