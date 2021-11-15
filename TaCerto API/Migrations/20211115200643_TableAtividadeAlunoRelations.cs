using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiTaCerto.Migrations
{
    public partial class TableAtividadeAlunoRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AtividadeIdAtividade",
                schema: "TaCerto",
                table: "AtividadeAluno",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PessoaIdPessoa",
                schema: "TaCerto",
                table: "AtividadeAluno",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AtividadeAluno_AtividadeIdAtividade",
                schema: "TaCerto",
                table: "AtividadeAluno",
                column: "AtividadeIdAtividade");

            migrationBuilder.CreateIndex(
                name: "IX_AtividadeAluno_PessoaIdPessoa",
                schema: "TaCerto",
                table: "AtividadeAluno",
                column: "PessoaIdPessoa");

            migrationBuilder.AddForeignKey(
                name: "FK_AtividadeAluno_Atividade_AtividadeIdAtividade",
                schema: "TaCerto",
                table: "AtividadeAluno",
                column: "AtividadeIdAtividade",
                principalSchema: "TaCerto",
                principalTable: "Atividade",
                principalColumn: "IdAtividade",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AtividadeAluno_Pessoa_PessoaIdPessoa",
                schema: "TaCerto",
                table: "AtividadeAluno",
                column: "PessoaIdPessoa",
                principalSchema: "TaCerto",
                principalTable: "Pessoa",
                principalColumn: "IdPessoa",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AtividadeAluno_Atividade_AtividadeIdAtividade",
                schema: "TaCerto",
                table: "AtividadeAluno");

            migrationBuilder.DropForeignKey(
                name: "FK_AtividadeAluno_Pessoa_PessoaIdPessoa",
                schema: "TaCerto",
                table: "AtividadeAluno");

            migrationBuilder.DropIndex(
                name: "IX_AtividadeAluno_AtividadeIdAtividade",
                schema: "TaCerto",
                table: "AtividadeAluno");

            migrationBuilder.DropIndex(
                name: "IX_AtividadeAluno_PessoaIdPessoa",
                schema: "TaCerto",
                table: "AtividadeAluno");

            migrationBuilder.DropColumn(
                name: "AtividadeIdAtividade",
                schema: "TaCerto",
                table: "AtividadeAluno");

            migrationBuilder.DropColumn(
                name: "PessoaIdPessoa",
                schema: "TaCerto",
                table: "AtividadeAluno");
        }
    }
}
