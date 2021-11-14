using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiTaCerto.Migrations
{
    public partial class CriaTabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AtividadeAlunos",
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
                    table.PrimaryKey("PK_AtividadeAlunos", x => x.IdAtividadeAluno);
                });

            migrationBuilder.CreateTable(
                name: "AtividadeRespostaAlunos",
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
                    table.PrimaryKey("PK_AtividadeRespostaAlunos", x => x.IdAtividadeRespostaAluno);
                });

            migrationBuilder.CreateTable(
                name: "Atividades",
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
                    table.PrimaryKey("PK_Atividades", x => x.IdAtividade);
                });

            migrationBuilder.CreateTable(
                name: "Disciplinas",
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
                    table.PrimaryKey("PK_Disciplinas", x => x.IdDisciplina);
                });

            migrationBuilder.CreateTable(
                name: "DisciplinaTurmas",
                columns: table => new
                {
                    IdDisciplinaTurma = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDisciplina = table.Column<int>(nullable: false),
                    IdTurma = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplinaTurmas", x => x.IdDisciplinaTurma);
                });

            migrationBuilder.CreateTable(
                name: "Instituicoes",
                columns: table => new
                {
                    IdInstituicao = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CNPJ = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instituicoes", x => x.IdInstituicao);
                });

            migrationBuilder.CreateTable(
                name: "logLogins",
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
                    table.PrimaryKey("PK_logLogins", x => x.IdLogLogin);
                });

            migrationBuilder.CreateTable(
                name: "Midias",
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
                    table.PrimaryKey("PK_Midias", x => x.IdMidia);
                });

            migrationBuilder.CreateTable(
                name: "Pessoas",
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
                    table.PrimaryKey("PK_Pessoas", x => x.IdPessoa);
                });

            migrationBuilder.CreateTable(
                name: "PessoaToken",
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
                name: "QuestaoRespostaAlunos",
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
                    table.PrimaryKey("PK_QuestaoRespostaAlunos", x => x.IdQuestaoRespostaAluno);
                });

            migrationBuilder.CreateTable(
                name: "Questoes",
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
                    table.PrimaryKey("PK_Questoes", x => x.IdQuestao);
                });

            migrationBuilder.CreateTable(
                name: "TurmaAlunos",
                columns: table => new
                {
                    IdTurmaAluno = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTurma = table.Column<int>(nullable: false),
                    IdPessoa = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurmaAlunos", x => x.IdTurmaAluno);
                });

            migrationBuilder.CreateTable(
                name: "Turmas",
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
                    table.PrimaryKey("PK_Turmas", x => x.IdTurma);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AtividadeAlunos");

            migrationBuilder.DropTable(
                name: "AtividadeRespostaAlunos");

            migrationBuilder.DropTable(
                name: "Atividades");

            migrationBuilder.DropTable(
                name: "Disciplinas");

            migrationBuilder.DropTable(
                name: "DisciplinaTurmas");

            migrationBuilder.DropTable(
                name: "Instituicoes");

            migrationBuilder.DropTable(
                name: "logLogins");

            migrationBuilder.DropTable(
                name: "Midias");

            migrationBuilder.DropTable(
                name: "Pessoas");

            migrationBuilder.DropTable(
                name: "PessoaToken");

            migrationBuilder.DropTable(
                name: "QuestaoRespostaAlunos");

            migrationBuilder.DropTable(
                name: "Questoes");

            migrationBuilder.DropTable(
                name: "TurmaAlunos");

            migrationBuilder.DropTable(
                name: "Turmas");
        }
    }
}
