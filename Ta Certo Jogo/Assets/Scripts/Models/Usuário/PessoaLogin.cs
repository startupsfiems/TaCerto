public class PessoaLogin
{
    public string Email { get; set; }
    public string Senha { get; set; }

    public PessoaLogin(string _email, string _senha)
    {
        Email = _email;
        Senha = _senha;
    }
}
