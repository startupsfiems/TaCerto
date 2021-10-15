using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTaCerto.Models;
using ApiTaCerto.Models.Usuario;

namespace ApiTaCerto.Repositorio
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly MainDbContext _contexto;

        public PessoaRepository(MainDbContext ctx)
        {
            _contexto = ctx;    
        }

        public async Task Add(Pessoa pessoa)
        {
            await _contexto.Pessoas.AddAsync(pessoa);
            await _contexto.SaveChangesAsync();
        }

        public async Task AddPessoaToken(PessoaToken pessoaToken){
            await _contexto.PessoaToken.AddAsync(pessoaToken);
            await _contexto.SaveChangesAsync();
        }

        public async Task RemovePessoaToken(PessoaToken pessoaToken){
            _contexto.PessoaToken.Remove(pessoaToken);
            await _contexto.SaveChangesAsync();
        }

        public Pessoa Find(long id)
        {
            Pessoa pessoa = _contexto.Pessoas.FirstOrDefault(p => p.IdPessoa == id);

            return pessoa;
        }

        public Pessoa Find(string email, int perfil)
        {
            return _contexto.Pessoas.FirstOrDefault(p => p.Email == email && p.Perfil == perfil);
        }

        public PessoaToken FindPessoaToken(long id){
            return _contexto.PessoaToken.FirstOrDefault(p => p.IdPessoaToken == id);
        }

        public IEnumerable<Pessoa> GetAll()
        {
            return _contexto.Pessoas.ToList();
        }

        public async Task Remove(long id)
        {
            var entity = _contexto.Pessoas.First(p => p.IdPessoa == id);
            _contexto.Pessoas.Remove(entity);
            await _contexto.SaveChangesAsync();
        }

        public async Task Update(Pessoa pessoa)
        {
            _contexto.Pessoas.Update(pessoa);
            await _contexto.SaveChangesAsync();
        }

        public TurmaAluno FindTurmaAluno(int idPessoa){
            return _contexto.TurmaAlunos.FirstOrDefault(ta => ta.IdPessoa == idPessoa);
        }

        public Turma FindTurma(int idTurma){
            return _contexto.Turmas.FirstOrDefault(tu => tu.IdTurma == idTurma);
        }

        public Midia FindMidia(int idOrigem, string tabela)
        {
            return _contexto.Midias.FirstOrDefault(midia => midia.IdOrigem == idOrigem && 
            midia.Tabela == tabela);
        }

        public async Task<string> SaveLogLogin(LogLogin logLogin)
        {
            try { 
                await _contexto.logLogins.AddAsync(logLogin);
                await _contexto.SaveChangesAsync();
            }
            catch
            {
                return "Erro ao salvar Log do Login";
            }

            return "Log do Login salvo com sucesso";
        }
    }
}