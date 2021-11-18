using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiTaCerto.Migrations
{
    public partial class TableQuestaoRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AtividadeIdAtividade",
                schema: "TaCerto",
                table: "Questao",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoQuestaoIdTipoQuestao",
                schema: "TaCerto",
                table: "Questao",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TipoQuestao",
                schema: "TaCerto",
                columns: table => new
                {
                    IdTipoQuestao = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 150, nullable: true),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoQuestao", x => x.IdTipoQuestao);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questao_AtividadeIdAtividade",
                schema: "TaCerto",
                table: "Questao",
                column: "AtividadeIdAtividade");

            migrationBuilder.CreateIndex(
                name: "IX_Questao_TipoQuestaoIdTipoQuestao",
                schema: "TaCerto",
                table: "Questao",
                column: "TipoQuestaoIdTipoQuestao");

            migrationBuilder.AddForeignKey(
                name: "FK_Questao_Atividade_AtividadeIdAtividade",
                schema: "TaCerto",
                table: "Questao",
                column: "AtividadeIdAtividade",
                principalSchema: "TaCerto",
                principalTable: "Atividade",
                principalColumn: "IdAtividade",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questao_TipoQuestao_TipoQuestaoIdTipoQuestao",
                schema: "TaCerto",
                table: "Questao",
                column: "TipoQuestaoIdTipoQuestao",
                principalSchema: "TaCerto",
                principalTable: "TipoQuestao",
                principalColumn: "IdTipoQuestao",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questao_Atividade_AtividadeIdAtividade",
                schema: "TaCerto",
                table: "Questao");

            migrationBuilder.DropForeignKey(
                name: "FK_Questao_TipoQuestao_TipoQuestaoIdTipoQuestao",
                schema: "TaCerto",
                table: "Questao");

            migrationBuilder.DropTable(
                name: "TipoQuestao",
                schema: "TaCerto");

            migrationBuilder.DropIndex(
                name: "IX_Questao_AtividadeIdAtividade",
                schema: "TaCerto",
                table: "Questao");

            migrationBuilder.DropIndex(
                name: "IX_Questao_TipoQuestaoIdTipoQuestao",
                schema: "TaCerto",
                table: "Questao");

            migrationBuilder.DropColumn(
                name: "AtividadeIdAtividade",
                schema: "TaCerto",
                table: "Questao");

            migrationBuilder.DropColumn(
                name: "TipoQuestaoIdTipoQuestao",
                schema: "TaCerto",
                table: "Questao");
        }
    }
}
