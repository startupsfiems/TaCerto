using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiTaCerto.Migrations
{
    public partial class TableLogLoginRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PessoaIdPessoa",
                schema: "TaCerto",
                table: "LogLogin",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LogLogin_PessoaIdPessoa",
                schema: "TaCerto",
                table: "LogLogin",
                column: "PessoaIdPessoa");

            migrationBuilder.AddForeignKey(
                name: "FK_LogLogin_Pessoa_PessoaIdPessoa",
                schema: "TaCerto",
                table: "LogLogin",
                column: "PessoaIdPessoa",
                principalSchema: "TaCerto",
                principalTable: "Pessoa",
                principalColumn: "IdPessoa",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogLogin_Pessoa_PessoaIdPessoa",
                schema: "TaCerto",
                table: "LogLogin");

            migrationBuilder.DropIndex(
                name: "IX_LogLogin_PessoaIdPessoa",
                schema: "TaCerto",
                table: "LogLogin");

            migrationBuilder.DropColumn(
                name: "PessoaIdPessoa",
                schema: "TaCerto",
                table: "LogLogin");
        }
    }
}
