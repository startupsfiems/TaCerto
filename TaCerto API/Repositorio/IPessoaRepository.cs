using System.Collections.Generic;
using System.Threading.Tasks;
using ApiTaCerto.Models;
using ApiTaCerto.Models.Usuario;

namespace ApiTaCerto.Repositorio
{
    public interface IPessoaRepository
    {
         Task Add(Pessoa pessoa);

         Task AddPessoaToken(PessoaToken pessoaToken);

        Task RemovePessoaToken(PessoaToken pessoaToken);

        IEnumerable<Pessoa> GetAll();

        Pessoa Find(long id);

        Pessoa Find(string email, int perfil);


        PessoaToken FindPessoaToken(long id);


        Task Remove(long id);

        Task Update(Pessoa pessoa);

        TurmaAluno FindTurmaAluno(int idPessoa);

        Turma FindTurma(int idTurma);

        Midia FindMidia(int idOrigem, string tabela);

        Task<string> SaveLogLogin(LogLogin logLogin);
    }
}