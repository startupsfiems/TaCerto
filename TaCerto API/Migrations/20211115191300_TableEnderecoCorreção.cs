using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiTaCerto.Migrations
{
    public partial class TableEnderecoCorreção : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoa_Endereco_EnderecoCobrancaIdEndereco",
                schema: "TaCerto",
                table: "Pessoa");

            migrationBuilder.DropForeignKey(
                name: "FK_Pessoa_Endereco_EnderecoPrincipalIdEndereco",
                schema: "TaCerto",
                table: "Pessoa");

            migrationBuilder.DropForeignKey(
                name: "FK_Pessoa_Instituicao_MatrizIdInstituicao",
                schema: "TaCerto",
                table: "Pessoa");

            migrationBuilder.DropIndex(
                name: "IX_Pessoa_EnderecoCobrancaIdEndereco",
                schema: "TaCerto",
                table: "Pessoa");

            migrationBuilder.DropIndex(
                name: "IX_Pessoa_EnderecoPrincipalIdEndereco",
                schema: "TaCerto",
                table: "Pessoa");

            migrationBuilder.DropIndex(
                name: "IX_Pessoa_MatrizIdInstituicao",
                schema: "TaCerto",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "EnderecoCobrancaIdEndereco",
                schema: "TaCerto",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "EnderecoPrincipalIdEndereco",
                schema: "TaCerto",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "MatrizIdInstituicao",
                schema: "TaCerto",
                table: "Pessoa");

            migrationBuilder.AddColumn<int>(
                name: "InstituicaoIdInstituicao",
                schema: "TaCerto",
                table: "Pessoa",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EnderecoCobrancaIdEndereco",
                schema: "TaCerto",
                table: "Instituicao",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EnderecoPrincipalIdEndereco",
                schema: "TaCerto",
                table: "Instituicao",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MatrizIdInstituicao",
                schema: "TaCerto",
                table: "Instituicao",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_InstituicaoIdInstituicao",
                schema: "TaCerto",
                table: "Pessoa",
                column: "InstituicaoIdInstituicao");

            migrationBuilder.CreateIndex(
                name: "IX_Instituicao_EnderecoCobrancaIdEndereco",
                schema: "TaCerto",
                table: "Instituicao",
                column: "EnderecoCobrancaIdEndereco");

            migrationBuilder.CreateIndex(
                name: "IX_Instituicao_EnderecoPrincipalIdEndereco",
                schema: "TaCerto",
                table: "Instituicao",
                column: "EnderecoPrincipalIdEndereco");

            migrationBuilder.CreateIndex(
                name: "IX_Instituicao_MatrizIdInstituicao",
                schema: "TaCerto",
                table: "Instituicao",
                column: "MatrizIdInstituicao");

            migrationBuilder.AddForeignKey(
                name: "FK_Instituicao_Endereco_EnderecoCobrancaIdEndereco",
                schema: "TaCerto",
                table: "Instituicao",
                column: "EnderecoCobrancaIdEndereco",
                principalSchema: "TaCerto",
                principalTable: "Endereco",
                principalColumn: "IdEndereco",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Instituicao_Endereco_EnderecoPrincipalIdEndereco",
                schema: "TaCerto",
                table: "Instituicao",
                column: "EnderecoPrincipalIdEndereco",
                principalSchema: "TaCerto",
                principalTable: "Endereco",
                principalColumn: "IdEndereco",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Instituicao_Instituicao_MatrizIdInstituicao",
                schema: "TaCerto",
                table: "Instituicao",
                column: "MatrizIdInstituicao",
                principalSchema: "TaCerto",
                principalTable: "Instituicao",
                principalColumn: "IdInstituicao",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoa_Instituicao_InstituicaoIdInstituicao",
                schema: "TaCerto",
                table: "Pessoa",
                column: "InstituicaoIdInstituicao",
                principalSchema: "TaCerto",
                principalTable: "Instituicao",
                principalColumn: "IdInstituicao",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instituicao_Endereco_EnderecoCobrancaIdEndereco",
                schema: "TaCerto",
                table: "Instituicao");

            migrationBuilder.DropForeignKey(
                name: "FK_Instituicao_Endereco_EnderecoPrincipalIdEndereco",
                schema: "TaCerto",
                table: "Instituicao");

            migrationBuilder.DropForeignKey(
                name: "FK_Instituicao_Instituicao_MatrizIdInstituicao",
                schema: "TaCerto",
                table: "Instituicao");

            migrationBuilder.DropForeignKey(
                name: "FK_Pessoa_Instituicao_InstituicaoIdInstituicao",
                schema: "TaCerto",
                table: "Pessoa");

            migrationBuilder.DropIndex(
                name: "IX_Pessoa_InstituicaoIdInstituicao",
                schema: "TaCerto",
                table: "Pessoa");

            migrationBuilder.DropIndex(
                name: "IX_Instituicao_EnderecoCobrancaIdEndereco",
                schema: "TaCerto",
                table: "Instituicao");

            migrationBuilder.DropIndex(
                name: "IX_Instituicao_EnderecoPrincipalIdEndereco",
                schema: "TaCerto",
                table: "Instituicao");

            migrationBuilder.DropIndex(
                name: "IX_Instituicao_MatrizIdInstituicao",
                schema: "TaCerto",
                table: "Instituicao");

            migrationBuilder.DropColumn(
                name: "InstituicaoIdInstituicao",
                schema: "TaCerto",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "EnderecoCobrancaIdEndereco",
                schema: "TaCerto",
                table: "Instituicao");

            migrationBuilder.DropColumn(
                name: "EnderecoPrincipalIdEndereco",
                schema: "TaCerto",
                table: "Instituicao");

            migrationBuilder.DropColumn(
                name: "MatrizIdInstituicao",
                schema: "TaCerto",
                table: "Instituicao");

            migrationBuilder.AddColumn<int>(
                name: "EnderecoCobrancaIdEndereco",
                schema: "TaCerto",
                table: "Pessoa",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EnderecoPrincipalIdEndereco",
                schema: "TaCerto",
                table: "Pessoa",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MatrizIdInstituicao",
                schema: "TaCerto",
                table: "Pessoa",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_EnderecoCobrancaIdEndereco",
                schema: "TaCerto",
                table: "Pessoa",
                column: "EnderecoCobrancaIdEndereco");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_EnderecoPrincipalIdEndereco",
                schema: "TaCerto",
                table: "Pessoa",
                column: "EnderecoPrincipalIdEndereco");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_MatrizIdInstituicao",
                schema: "TaCerto",
                table: "Pessoa",
                column: "MatrizIdInstituicao");

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoa_Endereco_EnderecoCobrancaIdEndereco",
                schema: "TaCerto",
                table: "Pessoa",
                column: "EnderecoCobrancaIdEndereco",
                principalSchema: "TaCerto",
                principalTable: "Endereco",
                principalColumn: "IdEndereco",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoa_Endereco_EnderecoPrincipalIdEndereco",
                schema: "TaCerto",
                table: "Pessoa",
                column: "EnderecoPrincipalIdEndereco",
                principalSchema: "TaCerto",
                principalTable: "Endereco",
                principalColumn: "IdEndereco",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoa_Instituicao_MatrizIdInstituicao",
                schema: "TaCerto",
                table: "Pessoa",
                column: "MatrizIdInstituicao",
                principalSchema: "TaCerto",
                principalTable: "Instituicao",
                principalColumn: "IdInstituicao",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
