using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiTaCerto.Migrations
{
    public partial class TableEndereco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoa_Instituicao_InstituicaoIdInstituicao",
                schema: "TaCerto",
                table: "Pessoa");

            migrationBuilder.DropIndex(
                name: "IX_Pessoa_InstituicaoIdInstituicao",
                schema: "TaCerto",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "InstituicaoIdInstituicao",
                schema: "TaCerto",
                table: "Pessoa");

            migrationBuilder.AddColumn<int>(
                name: "EnderecoCobrancaIdEndereco",
                schema: "TaCerto",
                table: "Pessoa",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EnderecoPrincipalIdEndereco",
                schema: "TaCerto",
                table: "Pessoa",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MatrizIdInstituicao",
                schema: "TaCerto",
                table: "Pessoa",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Endereco",
                schema: "TaCerto",
                columns: table => new
                {
                    IdEndereco = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pais = table.Column<string>(maxLength: 150, nullable: true),
                    UF = table.Column<string>(maxLength: 2, nullable: true),
                    Cidade = table.Column<string>(maxLength: 150, nullable: true),
                    Numero = table.Column<int>(nullable: false),
                    Complemento = table.Column<string>(maxLength: 150, nullable: true),
                    CEP = table.Column<string>(maxLength: 10, nullable: true),
                    Logradouro = table.Column<string>(maxLength: 150, nullable: true),
                    Bairro = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.IdEndereco);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropTable(
                name: "Endereco",
                schema: "TaCerto");

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
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_InstituicaoIdInstituicao",
                schema: "TaCerto",
                table: "Pessoa",
                column: "InstituicaoIdInstituicao");

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
    }
}
