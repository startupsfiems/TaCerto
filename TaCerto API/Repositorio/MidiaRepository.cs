using System.Linq;
using ApiTaCerto.Models.Usuario;

namespace ApiTaCerto.Repositorio
{
    public class MidiaRepository : IMidiaRepository
    {
        private readonly MainDbContext _contexto;

        public MidiaRepository(MainDbContext ctx)
        {
            _contexto = ctx;    
        }

        public Midia FindMidia(int idOrigem, string tabela)
        {
            return _contexto.Midia.FirstOrDefault(midia => midia.IdOrigem == idOrigem && 
            midia.Tabela == tabela);
        }
    }
}