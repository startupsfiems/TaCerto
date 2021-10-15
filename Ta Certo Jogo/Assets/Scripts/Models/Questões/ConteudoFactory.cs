
public class ConteudoFactory
{
    public Conteudo GetConteudo(Questao questao)
    {
        switch (questao.idTipoQuestao)
        {
            case 1:
                return new ConteudoCertoErrado(questao);
            case 2:
                return new ConteudoLacunas(questao);
            case 3:
                return new ConteudoColunas(questao);
            case 4:
                return new ConteudoAssociacao(questao);
            default:
                return null;
        }
    }
}