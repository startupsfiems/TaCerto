using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuManager : MonoBehaviour
{
    public AtividadeAtual atividadeAtual;

    [Header("Game Top Menu")]
    public S_ModoCertoErrado_Cronometro scriptCronometro;
    public TextMeshProUGUI correctAnswerText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI wrongAnswerText;
    private int acertos;
    private int erros;

    [Header("Telas dos modos")]
    public GameObject telaCertoErradoComFoto;
    public GameObject telaCertoErradoSemFoto;
    public GameObject botoesCertoErrado;
    public GameObject telaLacuna;

    [Header("Tela de explicação de modo")]
    public TextMeshProUGUI tituloText;
    public TextMeshProUGUI explicacaoText;
    public GameObject telaExplicaoTipo;
    public Animator telaExplicacaoTipoAnimator;
    private string explicacaoAtual;

    [Header("Backgrounds Images")]
    public GameObject portuguesImage;
    public GameObject geografiaImage;
    public GameObject historiaImage;
    public GameObject cienciasImage;
    public GameObject artesImage;
    public GameObject geralImage;
    public GameObject gameImage;

    [Header("Menu Atividade Completa")]
    public GameObject menuAtividadeCompleta;
    public TextMeshProUGUI numAcertosText;
    public TextMeshProUGUI numErrosText;
    public TextMeshProUGUI numTempoText;
    public RespostaManager respostaManager;

    private void Start()
    {
        acertos = 0;
        erros = 0;
        GameManager.Instance.LoadGameSceneElements();
    }

    public void StartGame()
    {
        scriptCronometro.SwitchTimer(true);
    }

    public void mostraUmaTelaEEscondeAsOutras(string modo)
    {
        if (modo.Equals("certoerradosemfoto"))
        {
            telaCertoErradoSemFoto.SetActive(true);
            botoesCertoErrado.SetActive(true);
            telaCertoErradoComFoto.SetActive(false);
            telaLacuna.SetActive(false);
        }
        else if (modo.Equals("certoerradocomfoto"))
        {
            telaCertoErradoSemFoto.SetActive(false);
            botoesCertoErrado.SetActive(true);
            telaCertoErradoComFoto.SetActive(true);
            telaLacuna.SetActive(false);
        }
        else if (modo.Equals("lacuna"))
        {
            telaCertoErradoSemFoto.SetActive(false);
            botoesCertoErrado.SetActive(false);
            telaCertoErradoComFoto.SetActive(false);
            telaLacuna.SetActive(true);
        }
    }

    public void setBackgroundImage(string name)
    {
        if (name.Equals("Português"))
        {
            portuguesImage.SetActive(true);
        }
        else if (name.Equals("Geografia"))
        {
            geografiaImage.SetActive(true);
        }
        else if (name.Equals("História"))
        {
            historiaImage.SetActive(true);
        }
        else if (name.Equals("Ciências"))
        {
            cienciasImage.SetActive(true);
        }
        else if (name.Equals("Artes"))
        {
            artesImage.SetActive(true);
        }
        else if (name.Equals("Conhecimentos Gerais"))
        {
            geralImage.SetActive(true);
        }
        else
        {
            gameImage.SetActive(true);
        }
    }

    public void setAcerto(bool op)
    {
        if (op)
        {
            acertos++;
            correctAnswerText.SetText(acertos.ToString());
        }
        else
        {
            erros++;
            wrongAnswerText.SetText(erros.ToString());
        }
    }

    public void AcabouAtividade()
    {
        if (atividadeAtual.precisaSalvarRespostas)
            respostaManager.SetAtividadeRespostaAluno();
    }

    public void showMenuAtividadeCompleta()
    {

        StartCoroutine(FechaTudo());
        AcabouAtividade();
        //string numeroAcertos = correctAnswerText.text.ToString();
        string numTempo = scriptCronometro.getTimeAndStopCount();

        numAcertosText.SetText(acertos.ToString());
        numErrosText.SetText(erros.ToString());
        numTempoText.SetText(numTempo);

        menuAtividadeCompleta.SetActive(true);
    }

    IEnumerator FechaTudo()
    {
        yield return new WaitForSeconds(.25f);

        telaCertoErradoComFoto.SetActive(false);
        telaCertoErradoSemFoto.SetActive(false);
        botoesCertoErrado.SetActive(false);
        telaLacuna.SetActive(false);
    }

    public void returnToMenuScene()
    {
        SceneManager.LoadScene("Menus", LoadSceneMode.Single);
    }

    public void playAgainAction()
    {
        print("atividadeAtual.numeroTentativasAtuais = " + atividadeAtual.numeroTentativasAtuais);
        print("atividadeAtual.numeroTentativas = " + atividadeAtual.numeroTentativas);
        if (atividadeAtual.numeroTentativasAtuais < atividadeAtual.numeroTentativas || atividadeAtual.numeroTentativas == 0)
            GameManager.Instance.PlayAgainButton(atividadeAtual.numeroTentativasAtuais, atividadeAtual.idAtividadeAluno);
        else
            MostraMensagemQueNaoTemMaisTentativas();
    }

    private void MostraMensagemQueNaoTemMaisTentativas()
    {
        Debug.Log("Não tem mais tentativas");
    }

    public void checaSePrecisaMostrarExplicao(string tipo)
    {
        if (tipo == "Certo/Errado")
        {
            if (!PlayerPrefs.HasKey(GameConstants.USER_JA_SABE_CERTO_ERRADO))
            {
                mostraExplicacao(tipo, GameConstants.DESCRICAO_CERTO_ERRADO);
                explicacaoAtual = GameConstants.USER_JA_SABE_CERTO_ERRADO;
            }
        }
        else if (tipo == "Lacunas")
        {
            if (!PlayerPrefs.HasKey(GameConstants.USER_JA_SABE_LACUNAS))
            {
                mostraExplicacao(tipo, GameConstants.DESCRICAO_LACUNAS);
                explicacaoAtual = GameConstants.USER_JA_SABE_LACUNAS;
            }
        }
    }

    public void mostraExplicacao(string titulo, string explicacao)
    {
        tituloText.text = "Modo " + titulo;
        if (titulo.Equals("Lacunas"))
            explicacaoText.fontSize = 27;
        else
            explicacaoText.fontSize = 32;
        explicacaoText.text = explicacao;
        scriptCronometro.SwitchTimer(false);
        telaExplicaoTipo.SetActive(true);
    }


    public void setQueConheceTipo()
    {
        PlayerPrefs.SetInt(explicacaoAtual, 1);
        PlayerPrefs.Save();
        telaExplicacaoTipoAnimator.SetTrigger("Fechar");
        StartCoroutine(FechaTelaExplicaoTipo());
    }

    IEnumerator FechaTelaExplicaoTipo()
    {
        yield return new WaitForSeconds(.5f);
        telaExplicaoTipo.SetActive(false);
        scriptCronometro.SwitchTimer(true);
    }
}