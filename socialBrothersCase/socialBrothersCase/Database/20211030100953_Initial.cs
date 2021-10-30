using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace socialBrothersCase.Database
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Street = table.Column<string>(type: "TEXT", maxLength: 250, nullable: true),
                    HouseNumber = table.Column<int>(type: "INTEGER", maxLength: 250, nullable: false),
                    PostalCode = table.Column<string>(type: "TEXT", maxLength: 250, nullable: true),
                    Location = table.Column<string>(type: "TEXT", maxLength: 250, nullable: true),
                    Country = table.Column<string>(type: "TEXT", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresses", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Adresses",
                columns: new[] { "Id", "Country", "HouseNumber", "Location", "PostalCode", "Street" },
                values: new object[] { new Guid("f744af43-a61f-4fe8-b4ce-d50762e8c905"), "The Netherlands", 100, "Utrecht", "3526 KS", "Europalaan" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adresses");
        }
    }
}
