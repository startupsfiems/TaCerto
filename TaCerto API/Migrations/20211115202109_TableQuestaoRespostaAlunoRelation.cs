using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiTaCerto.Migrations
{
    public partial class TableQuestaoRespostaAlunoRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Nota",
                schema: "TaCerto",
                table: "QuestaoRespostaAluno",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "AtividadeRespostaAlunoIdAtividadeRespostaAluno",
                schema: "TaCerto",
                table: "QuestaoRespostaAluno",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuestaoIdQuestao",
                schema: "TaCerto",
                table: "QuestaoRespostaAluno",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestaoRespostaAluno_AtividadeRespostaAlunoIdAtividadeRespostaAluno",
                schema: "TaCerto",
                table: "QuestaoRespostaAluno",
                column: "AtividadeRespostaAlunoIdAtividadeRespostaAluno");

            migrationBuilder.CreateIndex(
                name: "IX_QuestaoRespostaAluno_QuestaoIdQuestao",
                schema: "TaCerto",
                table: "QuestaoRespostaAluno",
                column: "QuestaoIdQuestao");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestaoRespostaAluno_AtividadeRespostaAluno_AtividadeRespostaAlunoIdAtividadeRespostaAluno",
                schema: "TaCerto",
                table: "QuestaoRespostaAluno",
                column: "AtividadeRespostaAlunoIdAtividadeRespostaAluno",
                principalSchema: "TaCerto",
                principalTable: "AtividadeRespostaAluno",
                principalColumn: "IdAtividadeRespostaAluno",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestaoRespostaAluno_Questao_QuestaoIdQuestao",
                schema: "TaCerto",
                table: "QuestaoRespostaAluno",
                column: "QuestaoIdQuestao",
                principalSchema: "TaCerto",
                principalTable: "Questao",
                principalColumn: "IdQuestao",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestaoRespostaAluno_AtividadeRespostaAluno_AtividadeRespostaAlunoIdAtividadeRespostaAluno",
                schema: "TaCerto",
                table: "QuestaoRespostaAluno");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestaoRespostaAluno_Questao_QuestaoIdQuestao",
                schema: "TaCerto",
                table: "QuestaoRespostaAluno");

            migrationBuilder.DropIndex(
                name: "IX_QuestaoRespostaAluno_AtividadeRespostaAlunoIdAtividadeRespostaAluno",
                schema: "TaCerto",
                table: "QuestaoRespostaAluno");

            migrationBuilder.DropIndex(
                name: "IX_QuestaoRespostaAluno_QuestaoIdQuestao",
                schema: "TaCerto",
                table: "QuestaoRespostaAluno");

            migrationBuilder.DropColumn(
                name: "AtividadeRespostaAlunoIdAtividadeRespostaAluno",
                schema: "TaCerto",
                table: "QuestaoRespostaAluno");

            migrationBuilder.DropColumn(
                name: "QuestaoIdQuestao",
                schema: "TaCerto",
                table: "QuestaoRespostaAluno");

            migrationBuilder.AlterColumn<double>(
                name: "Nota",
                schema: "TaCerto",
                table: "QuestaoRespostaAluno",
                type: "float",
                nullable: false,
                oldClrType: typeof(float));
        }
    }
}
