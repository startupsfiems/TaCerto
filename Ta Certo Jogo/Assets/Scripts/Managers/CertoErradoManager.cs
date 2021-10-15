using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CertoErradoManager : MonoBehaviour
{
    public AtividadeAtual atividadeAtual;
    public AudioManager audioManager;
    public RespostaManager respostaManager;
    private Questao questaoAtual;
    private int idQuestaoAtual;
    private float pesoAtual;
    public GameMenuManager gameMenuManager;
    private Color defaultColor = new Color(1, 1, 1, .2f);
    public Color writeColor = new Color(1, 1, 1, 1);
    public Color wrongColor = new Color(1, 1, 1, 1);
    public Sprite[] answerImages;

    [Header("Sem Foto")]
    public Animator certoErradoAnimator;
    public GameObject certoErradoSemFotoPrefab;
    public TextMeshProUGUI certoErradoSemFotoText;
    public Image semFotoCaixaDeTexto;
    public Image semFotoCertoImagem;

    [Header("Com Foto")]
    public Animator certoErradoComFotoAnimator;
    public GameObject certoErradoComFotoPrefab;
    public TextMeshProUGUI certoErradoComFotoText;
    public Image certoErradoImage;
    private bool certoErradoComFoto = false;
    public Image comFotoCaixaDeTexto;
    public Image comFotoCertoImagem;

    [Header("Botões")]
    public GameObject certoErradoBotoesPrefab;
    public Button botaoCerto;
    public Button botaoErrado;

    private bool currentAnswer;

    public void AtivaCertoErradoSemFoto(int _idQuestaoAtual)
    {
        semFotoCaixaDeTexto.color = defaultColor;
        idQuestaoAtual = _idQuestaoAtual;
        questaoAtual = atividadeAtual.questoes[idQuestaoAtual];
        pesoAtual = questaoAtual.pesoNota;
        ConteudoCertoErrado conteudo = (ConteudoCertoErrado)questaoAtual.conteudo;

        currentAnswer = conteudo.IsVerdadeiro;
        certoErradoSemFotoText.SetText(questaoAtual.enunciado);
        gameMenuManager.mostraUmaTelaEEscondeAsOutras("certoerradosemfoto");

        gameMenuManager.checaSePrecisaMostrarExplicao("Certo/Errado");

        certoErradoAnimator.SetTrigger("Entrar");
        certoErradoComFoto = false;
    }

    public void AtivaCertoErradoComFoto(int _idQuestaoAtual, Texture2D textura)
    {
        comFotoCaixaDeTexto.color = defaultColor;

        idQuestaoAtual = _idQuestaoAtual;
        questaoAtual = atividadeAtual.questoes[idQuestaoAtual];
        pesoAtual = questaoAtual.pesoNota;
        ConteudoCertoErrado conteudo = (ConteudoCertoErrado)questaoAtual.conteudo;

        currentAnswer = conteudo.IsVerdadeiro;
        certoErradoComFotoText.SetText(questaoAtual.enunciado);
        SetQuestaoImage(textura);
        gameMenuManager.checaSePrecisaMostrarExplicao("Certo/Errado");
        gameMenuManager.mostraUmaTelaEEscondeAsOutras("certoerradocomfoto");

        certoErradoComFotoAnimator.SetTrigger("Entrar");
        certoErradoComFoto = true;
    }

    private void SetQuestaoImage(Texture2D textura)
    {
        Sprite spriteImage = Sprite.Create(textura, new Rect(0, 0, textura.width, textura.height), new Vector2(0.5f, 0.0f), 1.0f);
        certoErradoImage.sprite = spriteImage;
    }

    public void RespondeCertoErrado(bool op)
    {
        certoErradoBotoesPrefab.SetActive(false);

        bool acertou = false;
        if(op == currentAnswer)
        {
            acertou = true;
            audioManager.PlaySound("Acerto");
        }
        else
        {
            audioManager.PlaySound("Erro");
        }

        PegaDadosEEnviaParaCalcular(acertou);

        gameMenuManager.setAcerto(acertou);
        ShowFeedBack(acertou, certoErradoComFoto);

        StartCoroutine(MoveToNext());

    }

    private void ShowFeedBack(bool acertou, bool comFoto)
    {
        if (acertou)
        {
            if (comFoto)
            {
                comFotoCertoImagem.sprite = answerImages[1];
                comFotoCertoImagem.gameObject.SetActive(true);
                comFotoCaixaDeTexto.color = writeColor;
            }
            else
            {
                semFotoCaixaDeTexto.color = writeColor;
                semFotoCertoImagem.sprite = answerImages[1];
                semFotoCertoImagem.gameObject.SetActive(true);
            }
        }
        else
        {
            if (comFoto)
            {
                comFotoCertoImagem.sprite = answerImages[0];
                comFotoCertoImagem.gameObject.SetActive(true);
                comFotoCaixaDeTexto.color = wrongColor;
            }
            else
            {
                semFotoCaixaDeTexto.color = wrongColor;
                semFotoCertoImagem.sprite = answerImages[0];
                semFotoCertoImagem.gameObject.SetActive(true);
            }
        }
    }

    IEnumerator MoveToNext()
    {
        yield return new WaitForSeconds(1.5f);

        if (certoErradoComFoto)
        {
            certoErradoComFotoAnimator.SetTrigger("Sair");
            comFotoCertoImagem.gameObject.SetActive(false);
        }
        else
        {
            certoErradoAnimator.SetTrigger("Sair");
            semFotoCertoImagem.gameObject.SetActive(false);
        }

        GameManager.Instance.NextQuestionOrFinish();
    }

    private void PegaDadosEEnviaParaCalcular(bool acertou)
    {
        int numAcerto = 0;
        int numErro = 0;
        if (acertou)
            numAcerto = 1;
        else
            numErro = 1;

        if(atividadeAtual.precisaSalvarRespostas)
            respostaManager.CalculaEAddQuestaoRespostaAluno(atividadeAtual.questoes[idQuestaoAtual].idQuestao, numAcerto, numErro, "", pesoAtual);
    }

    public void AcabouAtividade()
    {
        /*if (atividadeAtual.precisaSalvarRespostas)
            respostaManager.SetAtividadeRespostaAluno(); //Todo verificar código comentado aqui
        StartCoroutine(AcabouAtividadeCoroutine());*/
    }

    IEnumerator AcabouAtividadeCoroutine()
    {
        yield return new WaitForSeconds(.3f);

        certoErradoSemFotoPrefab.SetActive(false);
        certoErradoComFotoPrefab.SetActive(false);
    }

    public void DesativaCertoErrado()
    {
        certoErradoSemFotoPrefab.SetActive(false);
        certoErradoComFotoPrefab.SetActive(false);
        certoErradoBotoesPrefab.SetActive(false);
    }
}