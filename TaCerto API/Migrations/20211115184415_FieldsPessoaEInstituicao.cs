using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiTaCerto.Migrations
{
    public partial class FieldsPessoaEInstituicao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cpf",
                schema: "TaCerto",
                table: "Pessoa",
                newName: "CPF");

            migrationBuilder.AlterColumn<string>(
                name: "Senha",
                schema: "TaCerto",
                table: "Pessoa",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                schema: "TaCerto",
                table: "Pessoa",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "TaCerto",
                table: "Pessoa",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                schema: "TaCerto",
                table: "Pessoa",
                maxLength: 14,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InstituicaoIdInstituicao",
                schema: "TaCerto",
                table: "Pessoa",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                schema: "TaCerto",
                table: "Pessoa",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenDate",
                schema: "TaCerto",
                table: "Pessoa",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CNPJ",
                schema: "TaCerto",
                table: "Instituicao",
                maxLength: 18,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "TaCerto",
                table: "Instituicao",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdEnderecoCobranca",
                schema: "TaCerto",
                table: "Instituicao",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdEnderecoPrincipal",
                schema: "TaCerto",
                table: "Instituicao",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdMatriz",
                schema: "TaCerto",
                table: "Instituicao",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsMatriz",
                schema: "TaCerto",
                table: "Instituicao",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "NomeFantasia",
                schema: "TaCerto",
                table: "Instituicao",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RazaoSocial",
                schema: "TaCerto",
                table: "Instituicao",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                schema: "TaCerto",
                table: "Instituicao",
                maxLength: 25,
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Token",
                schema: "TaCerto",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "TokenDate",
                schema: "TaCerto",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "TaCerto",
                table: "Instituicao");

            migrationBuilder.DropColumn(
                name: "IdEnderecoCobranca",
                schema: "TaCerto",
                table: "Instituicao");

            migrationBuilder.DropColumn(
                name: "IdEnderecoPrincipal",
                schema: "TaCerto",
                table: "Instituicao");

            migrationBuilder.DropColumn(
                name: "IdMatriz",
                schema: "TaCerto",
                table: "Instituicao");

            migrationBuilder.DropColumn(
                name: "IsMatriz",
                schema: "TaCerto",
                table: "Instituicao");

            migrationBuilder.DropColumn(
                name: "NomeFantasia",
                schema: "TaCerto",
                table: "Instituicao");

            migrationBuilder.DropColumn(
                name: "RazaoSocial",
                schema: "TaCerto",
                table: "Instituicao");

            migrationBuilder.DropColumn(
                name: "Telefone",
                schema: "TaCerto",
                table: "Instituicao");

            migrationBuilder.RenameColumn(
                name: "CPF",
                schema: "TaCerto",
                table: "Pessoa",
                newName: "Cpf");

            migrationBuilder.AlterColumn<string>(
                name: "Senha",
                schema: "TaCerto",
                table: "Pessoa",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                schema: "TaCerto",
                table: "Pessoa",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "TaCerto",
                table: "Pessoa",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                schema: "TaCerto",
                table: "Pessoa",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 14,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CNPJ",
                schema: "TaCerto",
                table: "Instituicao",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 18,
                oldNullable: true);
        }
    }
}
