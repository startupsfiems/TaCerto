using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiTaCerto.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "TaCerto");

            migrationBuilder.CreateTable(
                name: "Atividade",
                schema: "TaCerto",
                columns: table => new
                {
                    IdAtividade = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTurmaDisciplinaAutor = table.Column<int>(nullable: false),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: false),
                    NumeroTentativas = table.Column<int>(nullable: false),
                    IsAleatorio = table.Column<bool>(nullable: false),
                    IsProva = table.Column<bool>(nullable: false),
                    Titulo = table.Column<string>(nullable: true),
                    NumeroQuestoes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atividade", x => x.IdAtividade);
                });

            migrationBuilder.CreateTable(
                name: "AtividadeAluno",
                schema: "TaCerto",
                columns: table => new
                {
                    IdAtividadeAluno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroTentativas = table.Column<int>(nullable: false),
                    MaiorNota = table.Column<double>(nullable: false),
                    MenorTempo = table.Column<int>(nullable: false),
                    MaiorTempo = table.Column<int>(nullable: false),
                    IdPessoa = table.Column<int>(nullable: false),
                    IdAtividade = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtividadeAluno", x => x.IdAtividadeAluno);
                });

            migrationBuilder.CreateTable(
                name: "AtividadeRespostaAluno",
                schema: "TaCerto",
                columns: table => new
                {
                    IdAtividadeRespostaAluno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAtividade = table.Column<int>(nullable: false),
                    IdPessoa = table.Column<int>(nullable: false),
                    DataEnvio = table.Column<DateTime>(nullable: false),
                    Nota = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtividadeRespostaAluno", x => x.IdAtividadeRespostaAluno);
                });

            migrationBuilder.CreateTable(
                name: "Disciplina",
                schema: "TaCerto",
                columns: table => new
                {
                    IdDisciplina = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true),
                    IdMatriz = table.Column<int>(nullable: false),
                    CorR = table.Column<int>(nullable: true),
                    CorG = table.Column<int>(nullable: true),
                    CorB = table.Column<int>(nullable: true),
                    CorA = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplina", x => x.IdDisciplina);
                });

            migrationBuilder.CreateTable(
                name: "DisciplinaTurma",
                schema: "TaCerto",
                columns: table => new
                {
                    IdDisciplinaTurma = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDisciplina = table.Column<int>(nullable: false),
                    IdTurma = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplinaTurma", x => x.IdDisciplinaTurma);
                });

            migrationBuilder.CreateTable(
                name: "Instituicao",
                schema: "TaCerto",
                columns: table => new
                {
                    IdInstituicao = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CNPJ = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instituicao", x => x.IdInstituicao);
                });

            migrationBuilder.CreateTable(
                name: "LogLogin",
                schema: "TaCerto",
                columns: table => new
                {
                    IdLogLogin = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPessoa = table.Column<int>(nullable: false),
                    HoraAcesso = table.Column<DateTime>(nullable: false),
                    Plataforma = table.Column<string>(maxLength: 150, nullable: true),
                    DeviceId = table.Column<string>(maxLength: 150, nullable: true),
                    DeviceIp = table.Column<string>(maxLength: 150, nullable: true),
                    Origem = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogLogin", x => x.IdLogLogin);
                });

            migrationBuilder.CreateTable(
                name: "Midia",
                schema: "TaCerto",
                columns: table => new
                {
                    IdMidia = table.Column<Guid>(nullable: false),
                    IdOrigem = table.Column<int>(nullable: false),
                    Tabela = table.Column<string>(nullable: true),
                    Filename = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    Extensao = table.Column<string>(nullable: true),
                    Tipo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Midia", x => x.IdMidia);
                });

            migrationBuilder.CreateTable(
                name: "Pessoa",
                schema: "TaCerto",
                columns: table => new
                {
                    IdPessoa = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdInstituicao = table.Column<int>(nullable: false),
                    Perfil = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Cpf = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Senha = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoa", x => x.IdPessoa);
                });

            migrationBuilder.CreateTable(
                name: "PessoaToken",
                schema: "TaCerto",
                columns: table => new
                {
                    IdPessoaToken = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(maxLength: 50, nullable: true),
                    Message = table.Column<string>(maxLength: 50, nullable: true),
                    Autenticado = table.Column<bool>(nullable: false),
                    IdPessoa = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoaToken", x => x.IdPessoaToken);
                });

            migrationBuilder.CreateTable(
                name: "Questao",
                schema: "TaCerto",
                columns: table => new
                {
                    IdQuestao = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAtividade = table.Column<int>(nullable: false),
                    IdTipoQuestao = table.Column<int>(nullable: false),
                    Titulo = table.Column<string>(nullable: true),
                    Enunciado = table.Column<string>(nullable: true),
                    JsonQuestao = table.Column<string>(nullable: true),
                    PesoNota = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questao", x => x.IdQuestao);
                });

            migrationBuilder.CreateTable(
                name: "QuestaoRespostaAluno",
                schema: "TaCerto",
                columns: table => new
                {
                    IdQuestaoRespostaAluno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAtividadeRespostaAluno = table.Column<int>(nullable: false),
                    IdQuestao = table.Column<int>(nullable: false),
                    NumAcerto = table.Column<int>(nullable: false),
                    NumErro = table.Column<int>(nullable: false),
                    JsonReposta = table.Column<string>(nullable: true),
                    Nota = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestaoRespostaAluno", x => x.IdQuestaoRespostaAluno);
                });

            migrationBuilder.CreateTable(
                name: "Turma",
                schema: "TaCerto",
                columns: table => new
                {
                    IdTurma = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdInstituicao = table.Column<int>(nullable: false),
                    Serie = table.Column<string>(nullable: true),
                    Periodo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turma", x => x.IdTurma);
                });

            migrationBuilder.CreateTable(
                name: "TurmaAluno",
                schema: "TaCerto",
                columns: table => new
                {
                    IdTurmaAluno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTurma = table.Column<int>(nullable: false),
                    IdPessoa = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurmaAluno", x => x.IdTurmaAluno);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Atividade",
                schema: "TaCerto");

            migrationBuilder.DropTable(
                name: "AtividadeAluno",
                schema: "TaCerto");

            migrationBuilder.DropTable(
                name: "AtividadeRespostaAluno",
                schema: "TaCerto");

            migrationBuilder.DropTable(
                name: "Disciplina",
                schema: "TaCerto");

            migrationBuilder.DropTable(
                name: "DisciplinaTurma",
                schema: "TaCerto");

            migrationBuilder.DropTable(
                name: "Instituicao",
                schema: "TaCerto");

            migrationBuilder.DropTable(
                name: "LogLogin",
                schema: "TaCerto");

            migrationBuilder.DropTable(
                name: "Midia",
                schema: "TaCerto");

            migrationBuilder.DropTable(
                name: "Pessoa",
                schema: "TaCerto");

            migrationBuilder.DropTable(
                name: "PessoaToken",
                schema: "TaCerto");

            migrationBuilder.DropTable(
                name: "Questao",
                schema: "TaCerto");

            migrationBuilder.DropTable(
                name: "QuestaoRespostaAluno",
                schema: "TaCerto");

            migrationBuilder.DropTable(
                name: "Turma",
                schema: "TaCerto");

            migrationBuilder.DropTable(
                name: "TurmaAluno",
                schema: "TaCerto");
        }
    }
}
