using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Salva as respostas da AtividadeAtual para enviar os dados e salvar no banco na conclusão da atividade
public class RespostaManager : MonoBehaviour
{
    public AtividadeAtual atividadeAtual;
    public MainPlayer mainPlayer;

    private int numQuestoes;
    private float notaSomada;
    private AtividadeRespostaAluno atividadeRespostaAluno;
    private List<QuestaoRespostaAluno> questoesRespostaAluno;
    public Button playAgainButton;

    public S_ModoCertoErrado_Cronometro cronometro;

    private void Start()
    {
        numQuestoes = atividadeAtual.numQuestoes;
        atividadeRespostaAluno = new AtividadeRespostaAluno();
        questoesRespostaAluno = new List<QuestaoRespostaAluno>();
    }

    public void SetNotaSomada(List<Questao> questoes)
    {
        foreach(Questao question in questoes)
        {
            notaSomada += question.pesoNota;
        }
    }

    public void SetAtividadeRespostaAluno()
    {
        
        atividadeAtual.precisaSalvarRespostas = false;

        atividadeRespostaAluno.idAtividade = atividadeAtual.idAtividade;
        atividadeRespostaAluno.idPessoa = mainPlayer.idPessoa;
        atividadeRespostaAluno.dataEnvio = System.DateTime.Now;
        atividadeRespostaAluno.nota = CalculaNota();

        string atividadeRespostaAlunoString = JsonConvert.SerializeObject(atividadeRespostaAluno);

        SetQueFezMaisUmaVez();
    }

    private void SetQueFezMaisUmaVez()
    {
        AtividadeAluno atividadeAluno = new AtividadeAluno();
        atividadeAluno.idAtividadeAluno = atividadeAtual.idAtividadeAluno;
        atividadeAluno.numeroTentativas = atividadeAtual.numeroTentativasAtuais;

        if (atividadeAtual.numeroTentativasAtuais >= atividadeAtual.numeroTentativas)
            playAgainButton.interactable = false;

        atividadeAluno.maiorTempo = atividadeAtual.maiorTempo;
        atividadeAluno.menorTempo = atividadeAtual.menorTempo;

        if (atividadeRespostaAluno.nota > atividadeAtual.maiorNota)
            atividadeAluno.maiorNota = atividadeRespostaAluno.nota;
        else
            atividadeAluno.maiorNota = atividadeAtual.maiorNota;

        int tempoAtualEmSegundos = cronometro.getSegundos();

        if (atividadeAluno.menorTempo == 0 && atividadeAluno.maiorTempo == 0)
        {
            atividadeAluno.menorTempo = tempoAtualEmSegundos;
            atividadeAluno.maiorTempo = tempoAtualEmSegundos;
        }
        else
        {
            if (tempoAtualEmSegundos > atividadeAluno.maiorTempo)
                atividadeAluno.maiorTempo = tempoAtualEmSegundos;
            else if (tempoAtualEmSegundos < atividadeAluno.menorTempo)
                atividadeAluno.menorTempo = tempoAtualEmSegundos;
        }

        atividadeAluno.idPessoa = mainPlayer.idPessoa;
        atividadeAluno.idAtividade = atividadeAtual.idAtividade;

        string atividadeAlunoJson = JsonConvert.SerializeObject(atividadeAluno);
        StartCoroutine(RestClient.Instance.Post(2, "addorupdateatividadealuno", atividadeAlunoJson, AddOrUpdateAtividadeAlunoCallBack));
    }

    //receber a quantidade de tentativas do banco e atualizar se for necessário
    private void AddOrUpdateAtividadeAlunoCallBack(RespostaPadrao resposta, int id, int id2, int id3, bool isResult)
    {
        if (resposta.GetOk())
        {
            AtividadeAluno atividadeAluno = JsonConvert.DeserializeObject<AtividadeAluno>(resposta.dado.ToString());
            atividadeAtual.idAtividadeAluno = atividadeAluno.idAtividadeAluno;
            atividadeAtual.maiorNota = atividadeAluno.maiorNota;
            atividadeAtual.menorTempo = atividadeAluno.menorTempo;
            atividadeAtual.maiorTempo = atividadeAluno.maiorTempo;
            atividadeAtual.numeroTentativasAtuais = atividadeAluno.numeroTentativas;

            if (atividadeAtual.numeroTentativasAtuais < atividadeAtual.numeroTentativas || atividadeAtual.numeroTentativas == 0)
                playAgainButton.interactable = true;
            string atividadeRespostaAlunoString = JsonConvert.SerializeObject(atividadeRespostaAluno);
            StartCoroutine(RestClient.Instance.Post(2, "saveatividaderesposta", atividadeRespostaAlunoString, GetIdAtividadeRespostaAndCallToSendQuestaoResposta));
        }
        else
        {
            Debug.LogError("Erro");
        }
    }

    void GetIdAtividadeRespostaAndCallToSendQuestaoResposta(RespostaPadrao resposta, int id, int id2, int id3, bool isResult)
    {
        if (resposta.GetOk())
        {
            int idAtividadeResposta = JsonConvert.DeserializeObject<int>(resposta.dado.ToString());
            SendAllQuestaoRespostaAluno(idAtividadeResposta);
        }
        else
        {
            Debug.LogError("Erro");
        }
    }

    private void SendAllQuestaoRespostaAluno(int idAtividadeResposta)
    {

        foreach (QuestaoRespostaAluno questaoResposta in questoesRespostaAluno)
            questaoResposta.idAtividadeRespostaAluno = idAtividadeResposta;

        var jsonQuestoesRespostaAluno = JsonConvert.SerializeObject(questoesRespostaAluno);
        StartCoroutine(RestClient.Instance.Post(2, "questaorespostaluno", jsonQuestoesRespostaAluno, GetSaveQuestaoRespostaResult));
    }

    void GetSaveQuestaoRespostaResult(RespostaPadrao resposta, int id, int id2, int id3, bool isResult)
    {
        if (!resposta.GetOk())
            Debug.LogError("erro");
    }

    private float CalculaNota()
    {
        float notaAtividade = 0;
        
        foreach(QuestaoRespostaAluno questaoResposta in questoesRespostaAluno)
        {
            notaAtividade += questaoResposta.nota;
        }

        return notaAtividade;
    }

    public void CalculaEAddQuestaoRespostaAluno(int idQuestao, int numAcerto, int numErro, string jsonResposta, float peso)
    {

        float nota = (10.0f * peso) / notaSomada;
        float respostasTotais = numAcerto + numErro + 0.0f;
        float porcentagemPerdida = numErro / respostasTotais;
        nota -= (nota * porcentagemPerdida);

        QuestaoRespostaAluno questaoAtual = new QuestaoRespostaAluno(idQuestao, numAcerto, numErro, jsonResposta, nota);
        questoesRespostaAluno.Add(questaoAtual);
    }
}
