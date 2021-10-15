
public class QuestaoRespostaAluno
{
    public int idQuestaoRespostaAluno;
    public int idAtividadeRespostaAluno;
    public int idQuestao;
    public int numAcerto;
    public int numErro;
    public string jsonReposta;
    public float nota;

    public QuestaoRespostaAluno(int _idQuestao, int _numAcerto, int _numErro, string _jsonResposta, float _nota)
    {
        idQuestao = _idQuestao;
        numAcerto = _numAcerto;
        numErro = _numErro;
        jsonReposta = _jsonResposta;
        nota = _nota;
    }
}
