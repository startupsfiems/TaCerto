﻿// <auto-generated />
using System;
using ApiTaCerto.Models.Usuario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApiTaCerto.Migrations
{
    [DbContext(typeof(MainDbContext))]
    partial class MainDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("TaCerto")
                .HasAnnotation("ProductVersion", "3.1.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApiTaCerto.Models.Atividade", b =>
                {
                    b.Property<int>("IdAtividade")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataFim")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdTurmaDisciplinaAutor")
                        .HasColumnType("int");

                    b.Property<bool>("IsAleatorio")
                        .HasColumnType("bit");

                    b.Property<bool>("IsProva")
                        .HasColumnType("bit");

                    b.Property<int>("NumeroQuestoes")
                        .HasColumnType("int");

                    b.Property<int>("NumeroTentativas")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TurmaDisciplinaAutorIdTurmaDisciplinaAutor")
                        .HasColumnType("int");

                    b.HasKey("IdAtividade");

                    b.HasIndex("TurmaDisciplinaAutorIdTurmaDisciplinaAutor");

                    b.ToTable("Atividade");
                });

            modelBuilder.Entity("ApiTaCerto.Models.AtividadeAluno", b =>
                {
                    b.Property<int>("IdAtividadeAluno")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AtividadeIdAtividade")
                        .HasColumnType("int");

                    b.Property<int>("IdAtividade")
                        .HasColumnType("int");

                    b.Property<int>("IdPessoa")
                        .HasColumnType("int");

                    b.Property<double>("MaiorNota")
                        .HasColumnType("float");

                    b.Property<int>("MaiorTempo")
                        .HasColumnType("int");

                    b.Property<int>("MenorTempo")
                        .HasColumnType("int");

                    b.Property<int>("NumeroTentativas")
                        .HasColumnType("int");

                    b.Property<int?>("PessoaIdPessoa")
                        .HasColumnType("int");

                    b.HasKey("IdAtividadeAluno");

                    b.HasIndex("AtividadeIdAtividade");

                    b.HasIndex("PessoaIdPessoa");

                    b.ToTable("AtividadeAluno");
                });

            modelBuilder.Entity("ApiTaCerto.Models.AtividadeDisciplina", b =>
                {
                    b.Property<int>("IdAtividade")
                        .HasColumnType("int");

                    b.Property<int>("IdTurmaDisciplinaAutor")
                        .HasColumnType("int");

                    b.Property<int>("IdDisciplina")
                        .HasColumnType("int");

                    b.HasKey("IdAtividade", "IdTurmaDisciplinaAutor", "IdDisciplina");

                    b.ToTable("AtividadeDisciplina");
                });

            modelBuilder.Entity("ApiTaCerto.Models.AtividadeRespostaAluno", b =>
                {
                    b.Property<int>("IdAtividadeRespostaAluno")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AtividadeIdAtividade")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataEnvio")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdAtividade")
                        .HasColumnType("int");

                    b.Property<int>("IdPessoa")
                        .HasColumnType("int");

                    b.Property<float>("Nota")
                        .HasColumnType("real");

                    b.Property<int?>("PessoaIdPessoa")
                        .HasColumnType("int");

                    b.HasKey("IdAtividadeRespostaAluno");

                    b.HasIndex("AtividadeIdAtividade");

                    b.HasIndex("PessoaIdPessoa");

                    b.ToTable("AtividadeRespostaAluno");
                });

            modelBuilder.Entity("ApiTaCerto.Models.Disciplina", b =>
                {
                    b.Property<int>("IdDisciplina")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<int>("IdMatriz")
                        .HasColumnType("int");

                    b.Property<int?>("MatrizIdInstituicao")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.HasKey("IdDisciplina");

                    b.HasIndex("MatrizIdInstituicao");

                    b.ToTable("Disciplina");
                });

            modelBuilder.Entity("ApiTaCerto.Models.DisciplinaTurma", b =>
                {
                    b.Property<int>("IdDisciplinaTurma")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DisciplinaIdDisciplina")
                        .HasColumnType("int");

                    b.Property<int>("IdDisciplina")
                        .HasColumnType("int");

                    b.Property<int>("IdTurma")
                        .HasColumnType("int");

                    b.Property<int?>("TurmaIdTurma")
                        .HasColumnType("int");

                    b.HasKey("IdDisciplinaTurma");

                    b.HasIndex("DisciplinaIdDisciplina");

                    b.HasIndex("TurmaIdTurma");

                    b.ToTable("DisciplinaTurma");
                });

            modelBuilder.Entity("ApiTaCerto.Models.Endereco", b =>
                {
                    b.Property<int>("IdEndereco")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bairro")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<string>("CEP")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Cidade")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<string>("Complemento")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<string>("Logradouro")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<string>("Pais")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<string>("UF")
                        .HasColumnType("nvarchar(2)")
                        .HasMaxLength(2);

                    b.HasKey("IdEndereco");

                    b.ToTable("Endereco");
                });

            modelBuilder.Entity("ApiTaCerto.Models.Instituicao", b =>
                {
                    b.Property<int>("IdInstituicao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CNPJ")
                        .HasColumnType("nvarchar(18)")
                        .HasMaxLength(18);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<int?>("EnderecoCobrancaIdEndereco")
                        .HasColumnType("int");

                    b.Property<int?>("EnderecoPrincipalIdEndereco")
                        .HasColumnType("int");

                    b.Property<int>("IdEnderecoCobranca")
                        .HasColumnType("int");

                    b.Property<int>("IdEnderecoPrincipal")
                        .HasColumnType("int");

                    b.Property<int?>("IdMatriz")
                        .HasColumnType("int");

                    b.Property<bool>("IsMatriz")
                        .HasColumnType("bit");

                    b.Property<int?>("MatrizIdInstituicao")
                        .HasColumnType("int");

                    b.Property<string>("NomeFantasia")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<string>("RazaoSocial")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<string>("Telefone")
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.HasKey("IdInstituicao");

                    b.HasIndex("EnderecoCobrancaIdEndereco");

                    b.HasIndex("EnderecoPrincipalIdEndereco");

                    b.HasIndex("MatrizIdInstituicao");

                    b.ToTable("Instituicao");
                });

            modelBuilder.Entity("ApiTaCerto.Models.LogLogin", b =>
                {
                    b.Property<int>("IdLogLogin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DeviceId")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<string>("DeviceIp")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<DateTime>("HoraAcesso")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdPessoa")
                        .HasColumnType("int");

                    b.Property<int>("Origem")
                        .HasColumnType("int");

                    b.Property<int?>("PessoaIdPessoa")
                        .HasColumnType("int");

                    b.Property<string>("Plataforma")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.HasKey("IdLogLogin");

                    b.HasIndex("PessoaIdPessoa");

                    b.ToTable("LogLogin");
                });

            modelBuilder.Entity("ApiTaCerto.Models.Questao", b =>
                {
                    b.Property<int>("IdQuestao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AtividadeIdAtividade")
                        .HasColumnType("int");

                    b.Property<string>("Enunciado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdAtividade")
                        .HasColumnType("int");

                    b.Property<int>("IdTipoQuestao")
                        .HasColumnType("int");

                    b.Property<string>("JsonQuestao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("PesoNota")
                        .HasColumnType("real");

                    b.Property<int?>("TipoQuestaoIdTipoQuestao")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdQuestao");

                    b.HasIndex("AtividadeIdAtividade");

                    b.HasIndex("TipoQuestaoIdTipoQuestao");

                    b.ToTable("Questao");
                });

            modelBuilder.Entity("ApiTaCerto.Models.QuestaoRespostaAluno", b =>
                {
                    b.Property<int>("IdQuestaoRespostaAluno")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AtividadeRespostaAlunoIdAtividadeRespostaAluno")
                        .HasColumnType("int");

                    b.Property<int>("IdAtividadeRespostaAluno")
                        .HasColumnType("int");

                    b.Property<int>("IdQuestao")
                        .HasColumnType("int");

                    b.Property<string>("JsonReposta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Nota")
                        .HasColumnType("real");

                    b.Property<int>("NumAcerto")
                        .HasColumnType("int");

                    b.Property<int>("NumErro")
                        .HasColumnType("int");

                    b.Property<int?>("QuestaoIdQuestao")
                        .HasColumnType("int");

                    b.HasKey("IdQuestaoRespostaAluno");

                    b.HasIndex("AtividadeRespostaAlunoIdAtividadeRespostaAluno");

                    b.HasIndex("QuestaoIdQuestao");

                    b.ToTable("QuestaoRespostaAluno");
                });

            modelBuilder.Entity("ApiTaCerto.Models.TipoQuestao", b =>
                {
                    b.Property<int>("IdTipoQuestao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.HasKey("IdTipoQuestao");

                    b.ToTable("TipoQuestao");
                });

            modelBuilder.Entity("ApiTaCerto.Models.Turma", b =>
                {
                    b.Property<int>("IdTurma")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdInstituicao")
                        .HasColumnType("int");

                    b.Property<int?>("InstituicaoIdInstituicao")
                        .HasColumnType("int");

                    b.Property<int>("Periodo")
                        .HasColumnType("int");

                    b.Property<string>("Serie")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdTurma");

                    b.HasIndex("InstituicaoIdInstituicao");

                    b.ToTable("Turma");
                });

            modelBuilder.Entity("ApiTaCerto.Models.TurmaAluno", b =>
                {
                    b.Property<int>("IdTurmaAluno")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdPessoa")
                        .HasColumnType("int");

                    b.Property<int>("IdTurma")
                        .HasColumnType("int");

                    b.Property<int?>("PessoaIdPessoa")
                        .HasColumnType("int");

                    b.Property<int?>("TurmaIdTurma")
                        .HasColumnType("int");

                    b.HasKey("IdTurmaAluno");

                    b.HasIndex("PessoaIdPessoa");

                    b.HasIndex("TurmaIdTurma");

                    b.ToTable("TurmaAluno");
                });

            modelBuilder.Entity("ApiTaCerto.Models.TurmaDisciplinaAutor", b =>
                {
                    b.Property<int>("IdTurmaDisciplinaAutor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AutorIdPessoa")
                        .HasColumnType("int");

                    b.Property<int?>("DisciplinaTurmaIdDisciplinaTurma")
                        .HasColumnType("int");

                    b.Property<int>("IdAutor")
                        .HasColumnType("int");

                    b.Property<int>("IdDisciplinaTurma")
                        .HasColumnType("int");

                    b.HasKey("IdTurmaDisciplinaAutor");

                    b.HasIndex("AutorIdPessoa");

                    b.HasIndex("DisciplinaTurmaIdDisciplinaTurma");

                    b.ToTable("TurmaDisciplinaAutor");
                });

            modelBuilder.Entity("ApiTaCerto.Models.Usuario.Midia", b =>
                {
                    b.Property<Guid>("IdMidia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Extensao")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<string>("Filename")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<int>("IdOrigem")
                        .HasColumnType("int");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<string>("Tabela")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("IdMidia");

                    b.ToTable("Midia");
                });

            modelBuilder.Entity("ApiTaCerto.Models.Usuario.Pessoa", b =>
                {
                    b.Property<int>("IdPessoa")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CPF")
                        .HasColumnType("nvarchar(14)")
                        .HasMaxLength(14);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<int>("IdInstituicao")
                        .HasColumnType("int");

                    b.Property<int?>("InstituicaoIdInstituicao")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<int>("Perfil")
                        .HasColumnType("int");

                    b.Property<string>("Senha")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("TokenDate")
                        .HasColumnType("datetime2");

                    b.HasKey("IdPessoa");

                    b.HasIndex("InstituicaoIdInstituicao");

                    b.ToTable("Pessoa");
                });

            modelBuilder.Entity("ApiTaCerto.Models.Usuario.PessoaToken", b =>
                {
                    b.Property<int>("IdPessoaToken")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Autenticado")
                        .HasColumnType("bit");

                    b.Property<int>("IdPessoa")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(512)")
                        .HasMaxLength(512);

                    b.HasKey("IdPessoaToken");

                    b.ToTable("PessoaToken");
                });

            modelBuilder.Entity("ApiTaCerto.Models.Atividade", b =>
                {
                    b.HasOne("ApiTaCerto.Models.TurmaDisciplinaAutor", "TurmaDisciplinaAutor")
                        .WithMany()
                        .HasForeignKey("TurmaDisciplinaAutorIdTurmaDisciplinaAutor");
                });

            modelBuilder.Entity("ApiTaCerto.Models.AtividadeAluno", b =>
                {
                    b.HasOne("ApiTaCerto.Models.Atividade", "Atividade")
                        .WithMany()
                        .HasForeignKey("AtividadeIdAtividade");

                    b.HasOne("ApiTaCerto.Models.Usuario.Pessoa", "Pessoa")
                        .WithMany()
                        .HasForeignKey("PessoaIdPessoa");
                });

            modelBuilder.Entity("ApiTaCerto.Models.AtividadeRespostaAluno", b =>
                {
                    b.HasOne("ApiTaCerto.Models.Atividade", "Atividade")
                        .WithMany()
                        .HasForeignKey("AtividadeIdAtividade");

                    b.HasOne("ApiTaCerto.Models.Usuario.Pessoa", "Pessoa")
                        .WithMany()
                        .HasForeignKey("PessoaIdPessoa");
                });

            modelBuilder.Entity("ApiTaCerto.Models.Disciplina", b =>
                {
                    b.HasOne("ApiTaCerto.Models.Instituicao", "Matriz")
                        .WithMany()
                        .HasForeignKey("MatrizIdInstituicao");
                });

            modelBuilder.Entity("ApiTaCerto.Models.DisciplinaTurma", b =>
                {
                    b.HasOne("ApiTaCerto.Models.Disciplina", "Disciplina")
                        .WithMany()
                        .HasForeignKey("DisciplinaIdDisciplina");

                    b.HasOne("ApiTaCerto.Models.Turma", "Turma")
                        .WithMany()
                        .HasForeignKey("TurmaIdTurma");
                });

            modelBuilder.Entity("ApiTaCerto.Models.Instituicao", b =>
                {
                    b.HasOne("ApiTaCerto.Models.Endereco", "EnderecoCobranca")
                        .WithMany()
                        .HasForeignKey("EnderecoCobrancaIdEndereco");

                    b.HasOne("ApiTaCerto.Models.Endereco", "EnderecoPrincipal")
                        .WithMany()
                        .HasForeignKey("EnderecoPrincipalIdEndereco");

                    b.HasOne("ApiTaCerto.Models.Instituicao", "Matriz")
                        .WithMany()
                        .HasForeignKey("MatrizIdInstituicao");
                });

            modelBuilder.Entity("ApiTaCerto.Models.LogLogin", b =>
                {
                    b.HasOne("ApiTaCerto.Models.Usuario.Pessoa", "Pessoa")
                        .WithMany()
                        .HasForeignKey("PessoaIdPessoa");
                });

            modelBuilder.Entity("ApiTaCerto.Models.Questao", b =>
                {
                    b.HasOne("ApiTaCerto.Models.Atividade", "Atividade")
                        .WithMany()
                        .HasForeignKey("AtividadeIdAtividade");

                    b.HasOne("ApiTaCerto.Models.TipoQuestao", "TipoQuestao")
                        .WithMany()
                        .HasForeignKey("TipoQuestaoIdTipoQuestao");
                });

            modelBuilder.Entity("ApiTaCerto.Models.QuestaoRespostaAluno", b =>
                {
                    b.HasOne("ApiTaCerto.Models.AtividadeRespostaAluno", "AtividadeRespostaAluno")
                        .WithMany()
                        .HasForeignKey("AtividadeRespostaAlunoIdAtividadeRespostaAluno");

                    b.HasOne("ApiTaCerto.Models.Questao", "Questao")
                        .WithMany()
                        .HasForeignKey("QuestaoIdQuestao");
                });

            modelBuilder.Entity("ApiTaCerto.Models.Turma", b =>
                {
                    b.HasOne("ApiTaCerto.Models.Instituicao", "Instituicao")
                        .WithMany()
                        .HasForeignKey("InstituicaoIdInstituicao");
                });

            modelBuilder.Entity("ApiTaCerto.Models.TurmaAluno", b =>
                {
                    b.HasOne("ApiTaCerto.Models.Usuario.Pessoa", "Pessoa")
                        .WithMany()
                        .HasForeignKey("PessoaIdPessoa");

                    b.HasOne("ApiTaCerto.Models.Turma", "Turma")
                        .WithMany()
                        .HasForeignKey("TurmaIdTurma");
                });

            modelBuilder.Entity("ApiTaCerto.Models.TurmaDisciplinaAutor", b =>
                {
                    b.HasOne("ApiTaCerto.Models.Usuario.Pessoa", "Autor")
                        .WithMany()
                        .HasForeignKey("AutorIdPessoa");

                    b.HasOne("ApiTaCerto.Models.DisciplinaTurma", "DisciplinaTurma")
                        .WithMany()
                        .HasForeignKey("DisciplinaTurmaIdDisciplinaTurma");
                });

            modelBuilder.Entity("ApiTaCerto.Models.Usuario.Pessoa", b =>
                {
                    b.HasOne("ApiTaCerto.Models.Instituicao", "Instituicao")
                        .WithMany()
                        .HasForeignKey("InstituicaoIdInstituicao");
                });
#pragma warning restore 612, 618
        }
    }
}
