using Microsoft.EntityFrameworkCore;

namespace ApiTaCerto.Models.Usuario
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options)
        : base(options)
        {   }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<PessoaToken> PessoaToken { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Instituicao> Instituicoes { get; set; }
        //public DbSet<AtividadeDisciplina> AtividadeDisciplinas { get; set; }
        public DbSet<Atividade> Atividades { get; set; }
        public DbSet<Questao> Questoes { get; set; }
        public DbSet<TurmaAluno> TurmaAlunos { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<DisciplinaTurma> DisciplinaTurmas { get; set; }
        public DbSet<Midia> Midias { get; set; }
        public DbSet<LogLogin> logLogins { get; set; }

        public DbSet<AtividadeRespostaAluno> AtividadeRespostaAlunos { get; set; }
        public DbSet<QuestaoRespostaAluno> QuestaoRespostaAlunos { get; set; }

        public DbSet<AtividadeAluno> AtividadeAlunos { get; set; }
    }
}