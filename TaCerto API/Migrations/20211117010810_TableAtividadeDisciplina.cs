using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiTaCerto.Migrations
{
    public partial class TableAtividadeDisciplina : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AtividadeDisciplina",
                schema: "TaCerto",
                columns: table => new
                {
                    IdAtividade = table.Column<int>(nullable: false),
                    IdTurmaDisciplinaAutor = table.Column<int>(nullable: false),
                    IdDisciplina = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtividadeDisciplina", x => new { x.IdAtividade, x.IdTurmaDisciplinaAutor, x.IdDisciplina });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AtividadeDisciplina",
                schema: "TaCerto");
        }
    }
}
