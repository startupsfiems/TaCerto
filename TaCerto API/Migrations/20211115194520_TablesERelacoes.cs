using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiTaCerto.Migrations
{
    public partial class TablesERelacoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorA",
                schema: "TaCerto",
                table: "Disciplina");

            migrationBuilder.DropColumn(
                name: "CorB",
                schema: "TaCerto",
                table: "Disciplina");

            migrationBuilder.DropColumn(
                name: "CorG",
                schema: "TaCerto",
                table: "Disciplina");

            migrationBuilder.DropColumn(
                name: "CorR",
                schema: "TaCerto",
                table: "Disciplina");

            migrationBuilder.AddColumn<int>(
                name: "InstituicaoIdInstituicao",
                schema: "TaCerto",
                table: "Turma",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DisciplinaIdDisciplina",
                schema: "TaCerto",
                table: "DisciplinaTurma",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TurmaIdTurma",
                schema: "TaCerto",
                table: "DisciplinaTurma",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                schema: "TaCerto",
                table: "Disciplina",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                schema: "TaCerto",
                table: "Disciplina",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MatrizIdInstituicao",
                schema: "TaCerto",
                table: "Disciplina",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TurmaDisciplinaAutorIdTurmaDisciplinaAutor",
                schema: "TaCerto",
                table: "Atividade",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TurmaDisciplinaAutor",
                schema: "TaCerto",
                columns: table => new
                {
                    IdTurmaDisciplinaAutor = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAutor = table.Column<int>(nullable: false),
                    IdDisciplinaTurma = table.Column<int>(nullable: false),
                    AutorIdPessoa = table.Column<int>(nullable: true),
                    DisciplinaTurmaIdDisciplinaTurma = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurmaDisciplinaAutor", x => x.IdTurmaDisciplinaAutor);
                    table.ForeignKey(
                        name: "FK_TurmaDisciplinaAutor_Pessoa_AutorIdPessoa",
                        column: x => x.AutorIdPessoa,
                        principalSchema: "TaCerto",
                        principalTable: "Pessoa",
                        principalColumn: "IdPessoa",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TurmaDisciplinaAutor_DisciplinaTurma_DisciplinaTurmaIdDisciplinaTurma",
                        column: x => x.DisciplinaTurmaIdDisciplinaTurma,
                        principalSchema: "TaCerto",
                        principalTable: "DisciplinaTurma",
                        principalColumn: "IdDisciplinaTurma",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Turma_InstituicaoIdInstituicao",
                schema: "TaCerto",
                table: "Turma",
                column: "InstituicaoIdInstituicao");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinaTurma_DisciplinaIdDisciplina",
                schema: "TaCerto",
                table: "DisciplinaTurma",
                column: "DisciplinaIdDisciplina");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinaTurma_TurmaIdTurma",
                schema: "TaCerto",
                table: "DisciplinaTurma",
                column: "TurmaIdTurma");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplina_MatrizIdInstituicao",
                schema: "TaCerto",
                table: "Disciplina",
                column: "MatrizIdInstituicao");

            migrationBuilder.CreateIndex(
                name: "IX_Atividade_TurmaDisciplinaAutorIdTurmaDisciplinaAutor",
                schema: "TaCerto",
                table: "Atividade",
                column: "TurmaDisciplinaAutorIdTurmaDisciplinaAutor");

            migrationBuilder.CreateIndex(
                name: "IX_TurmaDisciplinaAutor_AutorIdPessoa",
                schema: "TaCerto",
                table: "TurmaDisciplinaAutor",
                column: "AutorIdPessoa");

            migrationBuilder.CreateIndex(
                name: "IX_TurmaDisciplinaAutor_DisciplinaTurmaIdDisciplinaTurma",
                schema: "TaCerto",
                table: "TurmaDisciplinaAutor",
                column: "DisciplinaTurmaIdDisciplinaTurma");

            migrationBuilder.AddForeignKey(
                name: "FK_Atividade_TurmaDisciplinaAutor_TurmaDisciplinaAutorIdTurmaDisciplinaAutor",
                schema: "TaCerto",
                table: "Atividade",
                column: "TurmaDisciplinaAutorIdTurmaDisciplinaAutor",
                principalSchema: "TaCerto",
                principalTable: "TurmaDisciplinaAutor",
                principalColumn: "IdTurmaDisciplinaAutor",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Disciplina_Instituicao_MatrizIdInstituicao",
                schema: "TaCerto",
                table: "Disciplina",
                column: "MatrizIdInstituicao",
                principalSchema: "TaCerto",
                principalTable: "Instituicao",
                principalColumn: "IdInstituicao",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DisciplinaTurma_Disciplina_DisciplinaIdDisciplina",
                schema: "TaCerto",
                table: "DisciplinaTurma",
                column: "DisciplinaIdDisciplina",
                principalSchema: "TaCerto",
                principalTable: "Disciplina",
                principalColumn: "IdDisciplina",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DisciplinaTurma_Turma_TurmaIdTurma",
                schema: "TaCerto",
                table: "DisciplinaTurma",
                column: "TurmaIdTurma",
                principalSchema: "TaCerto",
                principalTable: "Turma",
                principalColumn: "IdTurma",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Turma_Instituicao_InstituicaoIdInstituicao",
                schema: "TaCerto",
                table: "Turma",
                column: "InstituicaoIdInstituicao",
                principalSchema: "TaCerto",
                principalTable: "Instituicao",
                principalColumn: "IdInstituicao",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atividade_TurmaDisciplinaAutor_TurmaDisciplinaAutorIdTurmaDisciplinaAutor",
                schema: "TaCerto",
                table: "Atividade");

            migrationBuilder.DropForeignKey(
                name: "FK_Disciplina_Instituicao_MatrizIdInstituicao",
                schema: "TaCerto",
                table: "Disciplina");

            migrationBuilder.DropForeignKey(
                name: "FK_DisciplinaTurma_Disciplina_DisciplinaIdDisciplina",
                schema: "TaCerto",
                table: "DisciplinaTurma");

            migrationBuilder.DropForeignKey(
                name: "FK_DisciplinaTurma_Turma_TurmaIdTurma",
                schema: "TaCerto",
                table: "DisciplinaTurma");

            migrationBuilder.DropForeignKey(
                name: "FK_Turma_Instituicao_InstituicaoIdInstituicao",
                schema: "TaCerto",
                table: "Turma");

            migrationBuilder.DropTable(
                name: "TurmaDisciplinaAutor",
                schema: "TaCerto");

            migrationBuilder.DropIndex(
                name: "IX_Turma_InstituicaoIdInstituicao",
                schema: "TaCerto",
                table: "Turma");

            migrationBuilder.DropIndex(
                name: "IX_DisciplinaTurma_DisciplinaIdDisciplina",
                schema: "TaCerto",
                table: "DisciplinaTurma");

            migrationBuilder.DropIndex(
                name: "IX_DisciplinaTurma_TurmaIdTurma",
                schema: "TaCerto",
                table: "DisciplinaTurma");

            migrationBuilder.DropIndex(
                name: "IX_Disciplina_MatrizIdInstituicao",
                schema: "TaCerto",
                table: "Disciplina");

            migrationBuilder.DropIndex(
                name: "IX_Atividade_TurmaDisciplinaAutorIdTurmaDisciplinaAutor",
                schema: "TaCerto",
                table: "Atividade");

            migrationBuilder.DropColumn(
                name: "InstituicaoIdInstituicao",
                schema: "TaCerto",
                table: "Turma");

            migrationBuilder.DropColumn(
                name: "DisciplinaIdDisciplina",
                schema: "TaCerto",
                table: "DisciplinaTurma");

            migrationBuilder.DropColumn(
                name: "TurmaIdTurma",
                schema: "TaCerto",
                table: "DisciplinaTurma");

            migrationBuilder.DropColumn(
                name: "MatrizIdInstituicao",
                schema: "TaCerto",
                table: "Disciplina");

            migrationBuilder.DropColumn(
                name: "TurmaDisciplinaAutorIdTurmaDisciplinaAutor",
                schema: "TaCerto",
                table: "Atividade");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                schema: "TaCerto",
                table: "Disciplina",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                schema: "TaCerto",
                table: "Disciplina",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CorA",
                schema: "TaCerto",
                table: "Disciplina",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CorB",
                schema: "TaCerto",
                table: "Disciplina",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CorG",
                schema: "TaCerto",
                table: "Disciplina",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CorR",
                schema: "TaCerto",
                table: "Disciplina",
                type: "int",
                nullable: true);
        }
    }
}
