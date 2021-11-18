using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiTaCerto.Migrations
{
    public partial class TableTurmaAlunoRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PessoaIdPessoa",
                schema: "TaCerto",
                table: "TurmaAluno",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TurmaIdTurma",
                schema: "TaCerto",
                table: "TurmaAluno",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TurmaAluno_PessoaIdPessoa",
                schema: "TaCerto",
                table: "TurmaAluno",
                column: "PessoaIdPessoa");

            migrationBuilder.CreateIndex(
                name: "IX_TurmaAluno_TurmaIdTurma",
                schema: "TaCerto",
                table: "TurmaAluno",
                column: "TurmaIdTurma");

            migrationBuilder.AddForeignKey(
                name: "FK_TurmaAluno_Pessoa_PessoaIdPessoa",
                schema: "TaCerto",
                table: "TurmaAluno",
                column: "PessoaIdPessoa",
                principalSchema: "TaCerto",
                principalTable: "Pessoa",
                principalColumn: "IdPessoa",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TurmaAluno_Turma_TurmaIdTurma",
                schema: "TaCerto",
                table: "TurmaAluno",
                column: "TurmaIdTurma",
                principalSchema: "TaCerto",
                principalTable: "Turma",
                principalColumn: "IdTurma",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TurmaAluno_Pessoa_PessoaIdPessoa",
                schema: "TaCerto",
                table: "TurmaAluno");

            migrationBuilder.DropForeignKey(
                name: "FK_TurmaAluno_Turma_TurmaIdTurma",
                schema: "TaCerto",
                table: "TurmaAluno");

            migrationBuilder.DropIndex(
                name: "IX_TurmaAluno_PessoaIdPessoa",
                schema: "TaCerto",
                table: "TurmaAluno");

            migrationBuilder.DropIndex(
                name: "IX_TurmaAluno_TurmaIdTurma",
                schema: "TaCerto",
                table: "TurmaAluno");

            migrationBuilder.DropColumn(
                name: "PessoaIdPessoa",
                schema: "TaCerto",
                table: "TurmaAluno");

            migrationBuilder.DropColumn(
                name: "TurmaIdTurma",
                schema: "TaCerto",
                table: "TurmaAluno");
        }
    }
}
