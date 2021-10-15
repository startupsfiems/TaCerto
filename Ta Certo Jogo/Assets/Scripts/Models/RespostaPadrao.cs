using System;

[Serializable]
public class RespostaPadrao
{
    public int codigo;
    public string resposta;
    public object dado;

    public bool GetOk()
    {
        if (codigo == 200)
            return true;

        return false;
    }
}
