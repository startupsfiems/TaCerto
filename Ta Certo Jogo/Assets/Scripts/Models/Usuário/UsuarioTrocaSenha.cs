public class UsuarioTrocaSenha
{
    public int Id { get; set; }
    public string Senha { get; set; }
    public string NovaSenha { get; set; }
    public string ConfirmacaoNovaSenha { get; set; }

    public UsuarioTrocaSenha()
    {

    }

    public UsuarioTrocaSenha(int id, string senha, string novaSenha, string confirmacaoNovaSenha)
    {
        Id = id;
        Senha = senha;
        NovaSenha = novaSenha;
        ConfirmacaoNovaSenha = confirmacaoNovaSenha;
    }
}
