using ApiTaCerto.Models.Usuario;

namespace ApiTaCerto.Repositorio
{
    public interface IMidiaRepository
    {
         Midia FindMidia(int idOrigem, string tabela);
    }
}