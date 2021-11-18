using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiTaCerto.Migrations
{
    public partial class TableAtividadeRespostaAlunoRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AtividadeIdAtividade",
                schema: "TaCerto",
                table: "AtividadeRespostaAluno",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PessoaIdPessoa",
                schema: "TaCerto",
                table: "AtividadeRespostaAluno",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AtividadeRespostaAluno_AtividadeIdAtividade",
                schema: "TaCerto",
                table: "AtividadeRespostaAluno",
                column: "AtividadeIdAtividade");

            migrationBuilder.CreateIndex(
                name: "IX_AtividadeRespostaAluno_PessoaIdPessoa",
                schema: "TaCerto",
                table: "AtividadeRespostaAluno",
                column: "PessoaIdPessoa");

            migrationBuilder.AddForeignKey(
                name: "FK_AtividadeRespostaAluno_Atividade_AtividadeIdAtividade",
                schema: "TaCerto",
                table: "AtividadeRespostaAluno",
                column: "AtividadeIdAtividade",
                principalSchema: "TaCerto",
                principalTable: "Atividade",
                principalColumn: "IdAtividade",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AtividadeRespostaAluno_Pessoa_PessoaIdPessoa",
                schema: "TaCerto",
                table: "AtividadeRespostaAluno",
                column: "PessoaIdPessoa",
                principalSchema: "TaCerto",
                principalTable: "Pessoa",
                principalColumn: "IdPessoa",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AtividadeRespostaAluno_Atividade_AtividadeIdAtividade",
                schema: "TaCerto",
                table: "AtividadeRespostaAluno");

            migrationBuilder.DropForeignKey(
                name: "FK_AtividadeRespostaAluno_Pessoa_PessoaIdPessoa",
                schema: "TaCerto",
                table: "AtividadeRespostaAluno");

            migrationBuilder.DropIndex(
                name: "IX_AtividadeRespostaAluno_AtividadeIdAtividade",
                schema: "TaCerto",
                table: "AtividadeRespostaAluno");

            migrationBuilder.DropIndex(
                name: "IX_AtividadeRespostaAluno_PessoaIdPessoa",
                schema: "TaCerto",
                table: "AtividadeRespostaAluno");

            migrationBuilder.DropColumn(
                name: "AtividadeIdAtividade",
                schema: "TaCerto",
                table: "AtividadeRespostaAluno");

            migrationBuilder.DropColumn(
                name: "PessoaIdPessoa",
                schema: "TaCerto",
                table: "AtividadeRespostaAluno");
        }
    }
}
