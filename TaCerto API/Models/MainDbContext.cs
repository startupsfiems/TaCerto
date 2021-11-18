using Microsoft.EntityFrameworkCore;

namespace ApiTaCerto.Models.Usuario
{
    public class MainDbContext : DbContext
    {
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<PessoaToken> PessoaToken { get; set; }
        public DbSet<Disciplina> Disciplina { get; set; }
        public DbSet<Instituicao> Instituicao { get; set; }
        public DbSet<AtividadeDisciplina> AtividadeDisciplinas { get; set; }
        public DbSet<Atividade> Atividade { get; set; }
        public DbSet<Questao> Questao { get; set; }
        public DbSet<TurmaAluno> TurmaAluno { get; set; }
        public DbSet<Turma> Turma { get; set; }
        public DbSet<DisciplinaTurma> DisciplinaTurma { get; set; }
        public DbSet<Midia> Midia { get; set; }
        public DbSet<LogLogin> LogLogin { get; set; }

        public DbSet<AtividadeRespostaAluno> AtividadeRespostaAluno { get; set; }
        public DbSet<QuestaoRespostaAluno> QuestaoRespostaAluno { get; set; }

        public DbSet<AtividadeAluno> AtividadeAluno { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options)
        : base(options)
        {   }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TaCerto");
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // equivalent of modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
                //entityType.Relational().TableName = entityType.DisplayName();
                entityType.SetTableName(entityType.DisplayName());
            }

            DefineChaves(modelBuilder);

            DefineRelacoes(modelBuilder);
        }


        // Define as chaves de algumas tabelas específicas
        private void DefineChaves(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<AtividadeDisciplina>()
            .HasKey(ad => new { ad.IdAtividade, ad.IdTurmaDisciplinaAutor, ad.IdDisciplina });
            /* // ProdutoCidade
             modelBuilder.Entity<ProdutoCidade>()
                 .HasKey(pc => new { pc.ProdutoId, pc.CidadeId });

             // ProdutoVenda
             modelBuilder.Entity<ProdutoVenda>()
                 .HasKey(pv => new { pv.ProdutoId, pv.VendaId });

             // CursoArea
             modelBuilder.Entity<CursoArea>()
                 .HasKey(ca => new { ca.CursoId, ca.AreaDeDesenvolvimentoId });

             // ProdutoCupom
             modelBuilder.Entity<ProdutoCupom>()
                 .HasKey(pc => new { pc.ProdutoId, pc.CupomId});

             // RespondenteExternoAtividadeResposta
             modelBuilder.Entity<RespondenteExternoAtividadeResposta>()
                 .HasKey(rear => new { rear.AtividadeRespostaId, rear.RespondenteExternoId });

             // LoginInstituicao
             modelBuilder.Entity<LoginInstituicao>()
                 .HasKey(ui => new { ui.LoginId, ui.InstituicaoId });

             // UsuarioTurma
             modelBuilder.Entity<UsuarioTurma>()
                 .HasKey(ut => new { ut.LoginId, ut.TurmaId });

             // UsuarioEvento
             modelBuilder.Entity<UsuarioEvento>()
                 .HasKey(uv => new { uv.LoginId, uv.EventoId });

             // QuestaoTag
             modelBuilder.Entity<QuestaoTag>()
                 .HasKey(qt => new { qt.QuestaoId, qt.TagId });

             // TagArea
             modelBuilder.Entity<TagArea>()
                 .HasKey(ta => new { ta.TagId, ta.AreaId });

             // DiagnosticoRespostas
             modelBuilder.Entity<DiagnosticoResposta>()
                 .HasKey(dr => new { dr.EtapaDiagnosticoId, dr.AtividadeRespostaId });

             // InstituicaoOrigem
             modelBuilder.Entity<InstituicaoOrigem>()
                 .HasKey(ir => new { ir.InstituicaoId, ir.OrigemId });*/
        }


        // Define as relações entre as tabelas
        private void DefineRelacoes(ModelBuilder modelBuilder)
        {
            // UUUP


            /*
            // Um Usuário tem vários Login
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Logins)
                .WithOne(l => l.Usuario)
                .HasForeignKey(l => l.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Um Login tem um Contato
            modelBuilder.Entity<Login>()
                .HasOne(u => u.Contato)
                .WithOne(c => c.Login)
                .HasForeignKey<Contato>(c => c.LoginFk)
                .OnDelete(DeleteBehavior.Cascade);

            // Um Login tem vários Dispositivos
            modelBuilder.Entity<Login>()
                .HasMany(l => l.Dispositivos)
                .WithOne(d => d.Login)
                .HasForeignKey(l => l.LoginId)
                .OnDelete(DeleteBehavior.Cascade);

            // Um Usuário tem vários Endereços
            modelBuilder.Entity<Endereco>()
                .HasOne(e => e.Login)
                .WithMany(l => l.Enderecos)
                .HasForeignKey(e => e.LoginId)
                .OnDelete(DeleteBehavior.Cascade);

            // Um UsuárioApi tem um Contato
            modelBuilder.Entity<UsuarioApi>()
                .HasOne(u => u.Contato)
                .WithOne(c => c.UsuarioApi)
                .HasForeignKey<Contato>(c => c.UsuarioApiFk)
                .OnDelete(DeleteBehavior.Cascade);

            // Um TipoLogin tem vários Login
            modelBuilder.Entity<TipoLogin>()
                .HasMany(t => t.Logins)
                .WithOne(l => l.TipoLogin)
                .HasForeignKey(l => l.TipoLoginId);

            // Uma Origem tem vários Login
            modelBuilder.Entity<Origem>()
                .HasMany(o => o.Logins)
                .WithOne(l => l.Origem)
                .HasForeignKey(l => l.OrigemId);

            // Uma Origem tem várias Venda
            modelBuilder.Entity<Origem>()
                .HasMany(o => o.Vendas)
                .WithOne(v => v.Origem)
                .HasForeignKey(v => v.OrigemId)
                .OnDelete(DeleteBehavior.NoAction);

            // Uma Origem pode ter várias Atividades
            modelBuilder.Entity<Origem>()
                .HasMany(o => o.Atividades)
                .WithOne(a => a.Origem)
                .HasForeignKey(a => a.OrigemId)
                .OnDelete(DeleteBehavior.NoAction);

            // Uma Permissao tem vários Login
            modelBuilder.Entity<Permissao>()
                .HasMany(p => p.Logins)
                .WithOne(l => l.Permissao)
                .HasForeignKey(l => l.PermissaoId)
                .OnDelete(DeleteBehavior.NoAction);

            // Uma Permissao tem vários UsuarioApi
            modelBuilder.Entity<Permissao>()
               .HasMany(p => p.UsuariosApi)
               .WithOne(u => u.Permissao)
               .HasForeignKey(u => u.PermissaoId)
               .OnDelete(DeleteBehavior.NoAction);

            // Uma FormaPagamento tem várias Venda
            modelBuilder.Entity<FormaPagamento>()
                .HasMany(f => f.Vendas)
                .WithOne(v => v.FormaPagamento)
                .HasForeignKey(v => v.FormaPagamentoId)
                .OnDelete(DeleteBehavior.NoAction);

            // Uma Venda tem vários ZeusVenda
            modelBuilder.Entity<Venda>()
                .HasMany(v => v.ZeusVendas)
                .WithOne(z => z.Venda)
                .HasForeignKey(z => z.VendaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Uma Venda tem um PagamentoErede
            modelBuilder.Entity<Venda>()
                .HasOne(v => v.PagamentoErede)
                .WithOne(p => p.Venda)
                .HasForeignKey<PagamentoErede>(p => p.VendaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Uma Venda tem um PagamentoBoletoBB
            modelBuilder.Entity<Venda>()
                .HasOne(v => v.PagamentoBoletoBB)
                .WithOne(p => p.Venda)
                .HasForeignKey<PagamentoBoletoBB>(p => p.VendaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Um StatusPagamento tem vários PagamentoErede
            modelBuilder.Entity<StatusPagamento>()
                .HasMany(s => s.PagamentosErede)
                .WithOne(p => p.Status)
                .HasForeignKey(p => p.StatusId)
                .OnDelete(DeleteBehavior.NoAction);

            // Um StatusPagamento tem vários PagamentoBoletoBB
            modelBuilder.Entity<StatusPagamento>()
                .HasMany(s => s.PagamentosBoletoBB)
                .WithOne(p => p.Status)
                .HasForeignKey(p => p.StatusId)
                .OnDelete(DeleteBehavior.Cascade);

            // Uma Cidade tem várias CidadeProxima
            modelBuilder.Entity<Cidade>()
                .HasMany(c => c.CidadesProximas)
                .WithOne(cp => cp.Cidade)
                .HasForeignKey(cp => cp.CidadeFk)
                .OnDelete(DeleteBehavior.Cascade);

            // Muitos ProdutoVenda possuem um Produto
            modelBuilder.Entity<ProdutoVenda>()
                .HasOne(pv => pv.Produto)
                .WithMany(p => p.ProdutoVendas)
                .HasForeignKey(pv => pv.ProdutoId)
                .OnDelete(DeleteBehavior.NoAction);

            // Muitos ProdutoVenda possuem uma Venda
            modelBuilder.Entity<ProdutoVenda>()
                .HasOne(pv => pv.Venda)
                .WithMany(v => v.ProdutoVendas)
                .HasForeignKey(pv => pv.VendaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Muitos UsuarioTurma possuem um Login
            modelBuilder.Entity<UsuarioTurma>()
                .HasOne(ut => ut.Login)
                .WithMany(l => l.UsuarioTurmas)
                .HasForeignKey(ut => ut.LoginId)
                .OnDelete(DeleteBehavior.Cascade);

            // Muitos UsuarioTurma possuem uma Turma
            modelBuilder.Entity<UsuarioTurma>()
                .HasOne(ut => ut.Turma)
                .WithMany(t => t.UsuarioTurmas)
                .HasForeignKey(ut => ut.TurmaId)
                .OnDelete(DeleteBehavior.NoAction);

            // Muitos UsuarioEvento possuem um Login
            modelBuilder.Entity<UsuarioEvento>()
                .HasOne(uv => uv.Login)
                .WithMany(l => l.UsuarioEventos)
                .HasForeignKey(uv => uv.LoginId)
                .OnDelete(DeleteBehavior.Cascade);

            // Muitos UsuarioEvento possuem um Evento
            modelBuilder.Entity<UsuarioEvento>()
                .HasOne(uv => uv.Evento)
                .WithMany(e => e.UsuarioEventos)
                .HasForeignKey(uv => uv.EventoId)
                .OnDelete(DeleteBehavior.NoAction);

            // Muitos ProdutoCupom possuem um Produto
            modelBuilder.Entity<ProdutoCupom>()
                .HasOne(pc => pc.Produto)
                .WithMany(p => p.ProdutoCupoms)
                .HasForeignKey(pc => pc.ProdutoId)
                .OnDelete(DeleteBehavior.NoAction);

            // Muitos ProdutoCupom possuem uma Venda
            modelBuilder.Entity<ProdutoCupom>()
                .HasOne(pc => pc.CupomDesconto)
                .WithMany(v => v.ProdutoCupoms)
                .HasForeignKey(pc => pc.CupomId)
                .OnDelete(DeleteBehavior.Cascade);

            // Muitos ProdutoCidade possuem um Produto
            modelBuilder.Entity<ProdutoCidade>()
                .HasOne(pc => pc.Produto)
                .WithMany(p => p.ProdutoCidades)
                .HasForeignKey(pc => pc.ProdutoId)
                .OnDelete(DeleteBehavior.NoAction);

            // Muitos ProdutoCidade possuem uma Cidade
            modelBuilder.Entity<ProdutoCidade>()
                .HasOne(pc => pc.Cidade)
                .WithMany(v => v.ProdutoCidades)
                .HasForeignKey(pc => pc.CidadeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Um TipoProduto tem vários Produtos
            modelBuilder.Entity<TipoProduto>()
                .HasMany(p => p.Produtos)
                .WithOne(pr => pr.TipoProduto)
                .HasForeignKey(pr => pr.TipoProdutoId)
                .OnDelete(DeleteBehavior.NoAction);

            // Um Eixo tem vários Cursos
            modelBuilder.Entity<Eixo>()
                .HasMany(e => e.Cursos)
                .WithOne(c => c.Eixo)
                .HasForeignKey(c => c.EixoId)
                .OnDelete(DeleteBehavior.NoAction);

            // Uma LinhaEducativa tem vários Cursos
            modelBuilder.Entity<LinhaEducativa>()
                .HasMany(e => e.Cursos)
                .WithOne(c => c.LinhaEducativa)
                .HasForeignKey(c => c.LinhaEducativaId)
                .OnDelete(DeleteBehavior.NoAction);

            // Uma Casa tem vários Produtos
            modelBuilder.Entity<Casa>()
                .HasMany(c => c.Produtos)
                .WithOne(p => p.Casa)
                .HasForeignKey(p => p.CasaId)
                .OnDelete(DeleteBehavior.NoAction);

            // Uma Casa tem vários Logins
            modelBuilder.Entity<Casa>()
                .HasMany(c => c.Logins)
                .WithOne(l => l.Casa)
                .HasForeignKey(l => l.CasaId)
                .OnDelete(DeleteBehavior.SetNull);

            // Uma Casa tem várias ConfiguracoesApis
            modelBuilder.Entity<Casa>()
                .HasMany(c => c.ConfiguracoesApis)
                .WithOne(ca => ca.Casa)
                .HasForeignKey(ca => ca.CasaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Uma Casa tem várias ConfiguracoesBoleto
            modelBuilder.Entity<Casa>()
                .HasMany(c => c.ConfiguracoesBoletos)
                .WithOne(cb => cb.Casa)
                .HasForeignKey(cb => cb.CasaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Uma Casa tem várias ConfiguracoesZeus
            modelBuilder.Entity<Casa>()
                .HasMany(c => c.ConfiguracoesZeus)
                .WithOne(cz => cz.Casa)
                .HasForeignKey(cz => cz.CasaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Muitos CursoArea possuem uma AreaDeDesenvolvimento
            modelBuilder.Entity<CursoArea>()
                .HasOne(ca => ca.AreaDeDesenvolvimento)
                .WithMany(a => a.CursoAreas)
                .HasForeignKey(ca => ca.AreaDeDesenvolvimentoId)
                .OnDelete(DeleteBehavior.NoAction);

            // Muitos CursoArea possuem um Curso
            modelBuilder.Entity<CursoArea>()
                .HasOne(ca => ca.Curso)
                .WithMany(a => a.CursoAreas)
                .HasForeignKey(ca => ca.CursoId)
                .OnDelete(DeleteBehavior.NoAction);

            // Uma TipoOferta tem vários Cursos
            modelBuilder.Entity<TipoOferta>()
                .HasMany(t => t.Produtos)
                .WithOne(p => p.TipoOferta)
                .HasForeignKey(p => p.TipoOfertaId)
                .OnDelete(DeleteBehavior.NoAction);

            // Uma Modalidade tem vários Cursos
            modelBuilder.Entity<Modalidade>()
                .HasMany(m => m.Cursos)
                .WithOne(c => c.Modalidade)
                .HasForeignKey(c => c.ModalidadeId)
                .OnDelete(DeleteBehavior.NoAction);

            // Um TipoDeAssunto tem vários Cursos
            modelBuilder.Entity<TipoDeAssunto>()
                .HasMany(t => t.Cursos)
                .WithOne(c => c.TipoDeAssunto)
                .HasForeignKey(c => c.TipoDeAssuntoId)
                .OnDelete(DeleteBehavior.NoAction);

            // Um TipoAtividade tem várias Atividades
            modelBuilder.Entity<TipoAtividade>()
                .HasMany(ta => ta.Atividades)
                .WithOne(a => a.TipoAtividade)
                .HasForeignKey(a => a.TipoAtividadeId)
                .OnDelete(DeleteBehavior.NoAction);

            // Uma Atividade tem várias Questões
            modelBuilder.Entity<Atividade>()
                .HasMany(a => a.Questoes)
                .WithOne(q => q.Atividade)
                .HasForeignKey(q => q.AtividadeId)
                .OnDelete(DeleteBehavior.NoAction);

            // Um ModoQuestão tem várias Questões
            modelBuilder.Entity<ModoQuestao>()
                .HasMany(mq => mq.Questoes)
                .WithOne(q => q.ModoQuestao)
                .HasForeignKey(q => q.ModoQuestaoId)
                .OnDelete(DeleteBehavior.NoAction);

            // Uma Atividade tem várias AtividadeResposta
            modelBuilder.Entity<Atividade>()
                .HasMany(a => a.AtividadeRespostas)
                .WithOne(ar => ar.Atividade)
                .HasForeignKey(ar => ar.AtividadeId)
                .OnDelete(DeleteBehavior.NoAction);

            // Um Login tem várias AtividadeResposta
            modelBuilder.Entity<Login>()
                .HasMany(l => l.AtividadeRespostas)
                .WithOne(ar => ar.Respondente)
                .HasForeignKey(ar => ar.RespondenteId)
                .OnDelete(DeleteBehavior.NoAction);

            // Uma Questão tem várias QuestaoResposta
            modelBuilder.Entity<Questao>()
                .HasMany(q => q.QuestaoRespostas)
                .WithOne(qr => qr.Questao)
                .HasForeignKey(qr => qr.QuestaoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Muitos QuestaoTag possuem uma Questão
            modelBuilder.Entity<QuestaoTag>()
                .HasOne(qt => qt.Questao)
                .WithMany(q => q.QuestaoTags)
                .HasForeignKey(qt => qt.QuestaoId)
                .OnDelete(DeleteBehavior.NoAction);

            // Muitos QuestaoTag possuem uma Tag
            modelBuilder.Entity<QuestaoTag>()
                .HasOne(qt => qt.Tag)
                .WithMany(t => t.QuestaoTags)
                .HasForeignKey(qt => qt.TagId)
                .OnDelete(DeleteBehavior.Cascade);

            // Muitas TagArea possuem uma Tag
            modelBuilder.Entity<TagArea>()
                .HasOne(ta => ta.Tag)
                .WithMany(t => t.TagAreas)
                .HasForeignKey(ta => ta.TagId)
                .OnDelete(DeleteBehavior.NoAction);

            // Muitas TagArea possuem uma Area
            modelBuilder.Entity<TagArea>()
                .HasOne(ta => ta.Area)
                .WithMany(a => a.TagAreas)
                .HasForeignKey(ta => ta.AreaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Uma AtividadeResposta tem várias QuestaoResposta
            modelBuilder.Entity<AtividadeResposta>()
                .HasMany(ar => ar.QuestaoRespostas)
                .WithOne(qr => qr.AtividadeResposta)
                .HasForeignKey(qr => qr.AtividadeRespostaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Muitos RespondenteExternoAtividadeResposta possuem um AtividadeResposta
            modelBuilder.Entity<RespondenteExternoAtividadeResposta>()
                .HasOne(rear => rear.AtividadeResposta)
                .WithMany(ar => ar.RespondentesExternos)
                .HasForeignKey(rear => rear.AtividadeRespostaId)
                .OnDelete(DeleteBehavior.NoAction);

            // Muitos RespondenteExternoAtividadeResposta possuem um RespondenteExterno
            modelBuilder.Entity<RespondenteExternoAtividadeResposta>()
                .HasOne(rear => rear.RespondenteExterno)
                .WithMany(re => re.AtividadeRespostas)
                .HasForeignKey(rear => rear.RespondenteExternoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Uma Instituição tem um Endereco
            modelBuilder.Entity<Instituicao>()
                .HasOne(i => i.Endereco)
                .WithOne(e => e.Instituicao)
                .HasForeignKey<Endereco>(e => e.InstituicaoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Uma Instituição pode ter muitas Turmas
            modelBuilder.Entity<Instituicao>()
                .HasMany(i => i.Turmas)
                .WithOne(c => c.Instituicao)
                .HasForeignKey(c => c.InstituicaoId);

            // Uma Instituição pode ter muitos Representantes
            modelBuilder.Entity<Instituicao>()
                .HasMany(i => i.Representantes)
                .WithOne(r => r.Instituicao)
                .HasForeignKey(r => r.InstituicaoId);

            // Uma Instituição pode ter muitas EtapaDiagnostico
            modelBuilder.Entity<Instituicao>()
                .HasMany(i => i.EtapaDiagnostico)
                .WithOne(e => e.Instituicao)
                .HasForeignKey(e => e.InstituicaoId);

            // Um Representante pode ter muitas InformacoesEmpresa
            modelBuilder.Entity<Representante>()
                .HasMany(i => i.InformacoesEmpresa)
                .WithOne(ie => ie.Representante)
                .HasForeignKey(ie => ie.RepresentanteId)
                .OnDelete(DeleteBehavior.NoAction);

            // Uma EtapaDiagnostico tem uma InformacoesEmpresa
            modelBuilder.Entity<EtapaDiagnostico>()
                .HasOne(ed => ed.InformacoesEmpresa)
                .WithOne(ie => ie.EtapaDiagnostico)
                .HasForeignKey<InformacoesEmpresa>(ie => ie.EtapaDiagnosticoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Uma EtapaDiagnostico tem um Objetivos
            modelBuilder.Entity<EtapaDiagnostico>()
                .HasOne(ed => ed.Objetivos)
                .WithOne(o => o.EtapaDiagnostico)
                .HasForeignKey<Objetivos>(ed => ed.EtapaDiagnosticoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Muitos DiagnosticoRespostas possuem um EtapaDiagnostico
            modelBuilder.Entity<DiagnosticoResposta>()
                .HasOne(dr => dr.EtapaDiagnostico)
                .WithMany(ed => ed.DiagnosticoRespostas)
                .HasForeignKey(dr => dr.EtapaDiagnosticoId)
                .OnDelete(DeleteBehavior.NoAction);

            // Muitos DiagnosticoRespostas possuem uma AtividadeResposta
            modelBuilder.Entity<DiagnosticoResposta>()
                .HasOne(dr => dr.AtividadeResposta)
                .WithMany(ar => ar.DiagnosticoRespostas)
                .HasForeignKey(dr => dr.AtividadeRespostaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Um Login pode ser Criador de muitas Turmas
            modelBuilder.Entity<Login>()
                .HasMany(l => l.Turmas)
                .WithOne(t => t.Criador)
                .HasForeignKey(t => t.CriadorId);

            // Uma Turma pode ter muitas Disciplinas
            modelBuilder.Entity<Turma>()
                .HasMany(t => t.Disciplinas)
                .WithOne(d => d.Turma)
                .HasForeignKey(d => d.TurmaId)
                .OnDelete(DeleteBehavior.NoAction);

            // Um Login pode ser Criador de muitas Disciplinas
            modelBuilder.Entity<Login>()
                .HasMany(l => l.Disciplinas)
                .WithOne(t => t.Criador)
                .HasForeignKey(t => t.CriadorId);

            // Uma Instituição pode ter muitos Eventos
            modelBuilder.Entity<Instituicao>()
                .HasMany(i => i.Eventos)
                .WithOne(c => c.Instituicao)
                .HasForeignKey(c => c.InstituicaoId);

            // Um Login pode ser Criador de muitos Eventos
            modelBuilder.Entity<Login>()
                .HasMany(l => l.Eventos)
                .WithOne(t => t.Criador)
                .HasForeignKey(t => t.CriadorId);

            // Um Evento pode ter muitos CursosEvento
            modelBuilder.Entity<Evento>()
                .HasMany(e => e.CursosEvento)
                .WithOne(ce => ce.Evento)
                .HasForeignKey(ce => ce.EventoId)
                .OnDelete(DeleteBehavior.NoAction);

            // Um Login pode ser Criador de muitos CursosEvento
            modelBuilder.Entity<Login>()
                .HasMany(l => l.CursosEvento)
                .WithOne(t => t.Criador)
                .HasForeignKey(t => t.CriadorId);


            // Um TipoInstituicao tem várias Instituicoes
            modelBuilder.Entity<TipoInstituicao>()
                .HasMany(t => t.Instituicoes)
                .WithOne(i => i.TipoInstituicao)
                .HasForeignKey(i => i.TipoInstituicaoId)
                .OnDelete(DeleteBehavior.NoAction);

            // Um Setor tem várias Instituicoes
            modelBuilder.Entity<Setor>()
                .HasMany(t => t.Instituicoes)
                .WithOne(i => i.Setor)
                .HasForeignKey(i => i.SetorId)
                .OnDelete(DeleteBehavior.NoAction);

            // Um Segmento tem várias Instituicoes
            modelBuilder.Entity<Segmento>()
                .HasMany(t => t.Instituicoes)
                .WithOne(i => i.Segmento)
                .HasForeignKey(i => i.SegmentoId)
                .OnDelete(DeleteBehavior.NoAction);

            // Muitos LoginInstituicao possuem um Login
            modelBuilder.Entity<LoginInstituicao>()
                .HasOne(li => li.Login)
                .WithMany(l => l.LoginInstitucoes)
                .HasForeignKey(li => li.LoginId)
                .OnDelete(DeleteBehavior.NoAction);

            // Muitos LoginInstituicao possuem uma Instituicao
            modelBuilder.Entity<LoginInstituicao>()
                .HasOne(li => li.Instituicao)
                .WithMany(i => i.LoginInstituicao)
                .HasForeignKey(li => li.InstituicaoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Uma Atividade pode ter muitas CodigoAvaliacaoExterna
            modelBuilder.Entity<Atividade>()
                .HasMany(a => a.CodigosExterno)
                .WithOne(c => c.Atividade)
                .HasForeignKey(c => c.AtividadeId);

            // Uma Atividade pode ter muitas CodigoAvaliacaoExterna
            modelBuilder.Entity<Atividade>()
                .HasMany(a => a.CodigosExterno)
                .WithOne(c => c.Atividade)
                .HasForeignKey(c => c.AtividadeId);

            // Uma Disciplina pode ter muitas Atividades
            modelBuilder.Entity<Disciplina>()
                .HasMany(d => d.Atividades)
                .WithOne(a => a.Disciplina)
                .HasForeignKey(a => a.DisciplinaId);

            // Um CursoEvento pode ter muitas Atividades
            modelBuilder.Entity<CursoEvento>()
                .HasMany(c => c.Atividades)
                .WithOne(a => a.CursoEvento)
                .HasForeignKey(a => a.CursoEventoId);

            // Um CursoEvento pode ter muitos BalancoDeCurso
            modelBuilder.Entity<CursoEvento>()
                .HasMany(c => c.Balancos)
                .WithOne(a => a.CursoEvento)
                .HasForeignKey(a => a.CursoEventoId);

            modelBuilder.Entity<TipoTarefa>()
                .HasMany(x => x.AcompanhamentoTarefas)
                .WithOne(x => x.TipoTarefa)
                .HasForeignKey(x => x.Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AcompanhamentoTarefa>()
                .HasOne(x => x.TipoTarefa)
                .WithMany(x => x.AcompanhamentoTarefas)
                .HasForeignKey(x => x.TipoTarefaId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Acao>()
                .HasMany(x => x.AcompanhamentosAcao)
                .WithOne(x => x.Acao)
                .HasForeignKey(x => x.Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AcompanhamentoAcao>()
                .HasOne(x => x.Acao)
                .WithMany(x => x.AcompanhamentosAcao)
                .HasForeignKey(x => x.AcaoId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<InstituicaoOrigem>()
                .HasOne(x => x.Instituicao)
                .WithMany(x => x.InstituicaoOrigem)
                .HasForeignKey(x => x.InstituicaoId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<InstituicaoOrigem>()
                .HasOne(x => x.Origem)
                .WithMany(x => x.InstituicaoOrigem)
                .HasForeignKey(x => x.OrigemId)
                .OnDelete(DeleteBehavior.NoAction);*/

        }
    }
}