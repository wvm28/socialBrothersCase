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
                    Street = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    HouseNumber = table.Column<int>(type: "INTEGER", maxLength: 250, nullable: false),
                    PostalCode = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    Location = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    Country = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresses", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Adresses",
                columns: new[] { "Id", "Country", "HouseNumber", "Location", "PostalCode", "Street" },
                values: new object[] { new Guid("3884daa0-01e9-4cb8-9ded-08025419db4f"), "The Netherlands", 100, "Utrecht", "3526 KS", "Europalaan" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adresses");
        }
    }
}
