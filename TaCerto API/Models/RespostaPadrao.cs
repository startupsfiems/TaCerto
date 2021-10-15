namespace ApiTaCerto.Models
{
    public class RespostaPadrao
    {
        public int Codigo { get; set; }
        public string Resposta { get; set; }
        public object Dado { get; set; }

        public RespostaPadrao()
        {
            Codigo = 200;
            Resposta = "Ok";
            Dado = null;
        }

        public RespostaPadrao(string resposta)
        {
            Codigo = 200;
            Resposta = resposta;
            Dado = null;
        }

        public RespostaPadrao(int cod, string res)
        {
            Codigo = cod;
            Resposta = res;
            Dado = null;
        }

        public RespostaPadrao(int cod, string res, object obj)
        {
            Codigo = cod;
            Resposta = res;
            Dado = obj;
        }

        public void SetCodigo(int codigo)
        {
            Codigo = codigo;
        }

        public void SetMensagem(string mensagem)
        {
            Resposta = mensagem;
        }

        public void SetMensagem(string mensagem, object data)
        {
            Resposta = mensagem;
            Dado = data;
        }

        public void SetSemAcesso()
        {
            Codigo = 403;
            Resposta = "Você não possui direito de acesso ao conteúdo";
        }

        public void SetNaoEncontrado(string mensagem)
        {
            Codigo = 404;
            Resposta = mensagem;
        }

        public void SetCampoVazio(string campo)
        {
            Codigo = 616;
            Resposta = "Você deve informar o campo " + campo;
        }

        public void SetCampoJaExiste(string mensagem)
        {
            Codigo = 626;
            Resposta = mensagem;
        }

        public void SetCampoIncorreto(string mensagem)
        {
            Codigo = 628;
            Resposta = mensagem;
        }

        public void SetCampoInvalido(string campo, string explicacao = null)
        {
            Codigo = 636;
            Resposta = "O campo " + campo + " informado não é válido";
            if (explicacao != null)
            {
                Resposta += ". " + explicacao;
            }
        }

        public void SetLimitesExcedidos(string mensagem)
        {
            Codigo = 656;
            Resposta = mensagem;
        }

        public void SetErroInterno()
        {
            Codigo = 666;
            Resposta = "Erro interno durante o processamento";
        }

        public void SetErroInterno(string erro)
        {
            Codigo = 666;
            Resposta = "Erro interno durante o processamento. " + erro;
        }



    }
}
