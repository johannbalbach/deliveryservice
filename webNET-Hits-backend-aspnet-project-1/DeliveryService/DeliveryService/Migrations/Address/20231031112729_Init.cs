using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryService.Migrations.Address
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "fias");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "as_addr_obj",
                schema: "fias");

            migrationBuilder.DropTable(
                name: "as_adm_hierarchy",
                schema: "fias");

            migrationBuilder.DropTable(
                name: "as_houses",
                schema: "fias");
        }
    }
}
