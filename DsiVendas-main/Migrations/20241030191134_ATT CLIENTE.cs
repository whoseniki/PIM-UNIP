using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DsiVendas.Migrations
{
    /// <inheritdoc />
    public partial class ATTCLIENTE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "Clientes",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CPF",
                table: "Clientes");
        }
    }
}
