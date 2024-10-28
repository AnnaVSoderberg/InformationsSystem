using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Information_System_ASP.Net.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BeloppIn",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "BeloppUt",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "TotalBeloppIn",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "TotalBeloppUt",
                table: "Drivers");

            migrationBuilder.AddColumn<decimal>(
                name: "BeloppIn",
                table: "Events",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BeloppUt",
                table: "Events",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "DriverID",
                keyValue: 1,
                column: "NoteDescription",
                value: "General maintenance check");

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "DriverID",
                keyValue: 2,
                column: "NoteDescription",
                value: "Brake and tire replacement");

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "DriverID",
                keyValue: 3,
                column: "NoteDescription",
                value: "Air filter and tire pressure check");

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "DriverID",
                keyValue: 4,
                column: "NoteDescription",
                value: "Windshield wiper repair");

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "DriverID",
                keyValue: 5,
                column: "NoteDescription",
                value: "Headlight and interior cleaning");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1,
                columns: new[] { "BeloppIn", "BeloppUt" },
                values: new object[] { 500.00m, 200.00m });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 2,
                columns: new[] { "BeloppIn", "BeloppUt" },
                values: new object[] { 0.00m, 100.00m });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 3,
                columns: new[] { "BeloppIn", "BeloppUt" },
                values: new object[] { 600.00m, 150.00m });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 4,
                columns: new[] { "BeloppIn", "BeloppUt" },
                values: new object[] { 0.00m, 300.00m });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 5,
                columns: new[] { "BeloppIn", "BeloppUt" },
                values: new object[] { 550.00m, 180.00m });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 6,
                columns: new[] { "BeloppIn", "BeloppUt" },
                values: new object[] { 0.00m, 50.00m });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 7,
                columns: new[] { "BeloppIn", "BeloppUt" },
                values: new object[] { 750.00m, 220.00m });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 8,
                columns: new[] { "BeloppIn", "BeloppUt" },
                values: new object[] { 670.00m, 170.00m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BeloppIn",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "BeloppUt",
                table: "Events");

            migrationBuilder.AddColumn<decimal>(
                name: "BeloppIn",
                table: "Drivers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BeloppUt",
                table: "Drivers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalBeloppIn",
                table: "Drivers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalBeloppUt",
                table: "Drivers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "DriverID",
                keyValue: 1,
                columns: new[] { "BeloppIn", "BeloppUt", "NoteDescription", "TotalBeloppIn", "TotalBeloppUt" },
                values: new object[] { 500.00m, 200.00m, null, 500.00m, 200.00m });

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "DriverID",
                keyValue: 2,
                columns: new[] { "BeloppIn", "BeloppUt", "NoteDescription", "TotalBeloppIn", "TotalBeloppUt" },
                values: new object[] { 600.00m, 150.00m, null, 600.00m, 150.00m });

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "DriverID",
                keyValue: 3,
                columns: new[] { "BeloppIn", "BeloppUt", "NoteDescription", "TotalBeloppIn", "TotalBeloppUt" },
                values: new object[] { 550.00m, 180.00m, null, 550.00m, 180.00m });

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "DriverID",
                keyValue: 4,
                columns: new[] { "BeloppIn", "BeloppUt", "NoteDescription", "TotalBeloppIn", "TotalBeloppUt" },
                values: new object[] { 750.00m, 220.00m, null, 750.00m, 220.00m });

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "DriverID",
                keyValue: 5,
                columns: new[] { "BeloppIn", "BeloppUt", "NoteDescription", "TotalBeloppIn", "TotalBeloppUt" },
                values: new object[] { 670.00m, 170.00m, null, 670.00m, 170.00m });
        }
    }
}
