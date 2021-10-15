using ApiTaCerto.Models;
using ApiTaCerto.Models.Usuario;
using ApiTaCerto.Repositorio;

namespace ApiTaCerto.Logic
{
    public class MidiaLogic
    {
        private readonly IMidiaRepository _midiaRepositorio;

        public MidiaLogic(IMidiaRepository midiaRepo)
        {
            _midiaRepositorio = midiaRepo;
        }

        public RespostaPadrao GetMidia(int idOrigem, string tabela){
            RespostaPadrao resposta = new RespostaPadrao();

            Midia midia = _midiaRepositorio.FindMidia(idOrigem, tabela);
            if (midia == null)
                resposta.SetCodigo(204);

            resposta.Dado = midia;
            return resposta;
        }
    }
}