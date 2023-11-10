using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryService.Migrations.Address
{
    /// <inheritdoc />
    public partial class Test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Houses",
                table: "Houses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hierarchies",
                table: "Hierarchies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.RenameTable(
                name: "Houses",
                newName: "as_houses");

            migrationBuilder.RenameTable(
                name: "Hierarchies",
                newName: "as_adm_hierarchy");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "as_addr_obj");

            migrationBuilder.AddPrimaryKey(
                name: "PK_as_houses",
                table: "as_houses",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_as_adm_hierarchy",
                table: "as_adm_hierarchy",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_as_addr_obj",
                table: "as_addr_obj",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_as_houses",
                table: "as_houses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_as_adm_hierarchy",
                table: "as_adm_hierarchy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_as_addr_obj",
                table: "as_addr_obj");

            migrationBuilder.RenameTable(
                name: "as_houses",
                newName: "Houses");

            migrationBuilder.RenameTable(
                name: "as_adm_hierarchy",
                newName: "Hierarchies");

            migrationBuilder.RenameTable(
                name: "as_addr_obj",
                newName: "Addresses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Houses",
                table: "Houses",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hierarchies",
                table: "Hierarchies",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "id");
        }
    }
}
