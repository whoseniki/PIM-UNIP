using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DsiVendas.Migrations
{
    /// <inheritdoc />
    public partial class ATUALIZAÇÃOFORNECEDORES : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Fornecedores",
                newName: "RazaoSocial");

            migrationBuilder.RenameColumn(
                name: "Cidade",
                table: "Fornecedores",
                newName: "Endereco");

            migrationBuilder.AddColumn<string>(
                name: "CNPJ",
                table: "Fornecedores",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CNPJ",
                table: "Fornecedores");

            migrationBuilder.RenameColumn(
                name: "RazaoSocial",
                table: "Fornecedores",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "Endereco",
                table: "Fornecedores",
                newName: "Cidade");
        }
    }
}
