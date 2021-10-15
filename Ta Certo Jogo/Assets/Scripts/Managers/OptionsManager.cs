using Assets.Scripts.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    [Header("Config Elements")]
    public GameObject telaDeConfiguracoes;
    private bool telaDeConfiguracoesAparecendo;
    public TextMeshProUGUI userNameText;
    public TextMeshProUGUI userEmailText;
    public Image userProfileImage;
    public Image userImage;
    private Sprite userSpriteImage;

    public SaveManager saveManager;
    public LoginManager loginManager;

    [Header("Password Elements")]
    public GameObject passwordChangePanel;
    public GameObject passwordChangedPanel;
    public TMP_InputField passwordInputText;
    public TextMeshProUGUI passwordMessageText;
    public TMP_InputField newPasswordInputText;
    public TextMeshProUGUI newPasswordMessageText;
    public TMP_InputField newPasswordConfirmInputText;
    public TextMeshProUGUI newPasswordConfirmMessageText;
    private bool telaDeTrocarSenhaAparecendo;
    private bool telaDeSenhaTrocadaAparecendo;
    private string novaSenha = "3";

    [Header("Tela de Atividades")]
    private bool activitiesScreenAparecendo;
    public GameObject activitiesScreen;
    private bool listaDeAtividadesAvaliativasAparecendo;
    public GameObject listaDeAtividadesAvaliativas;
    private bool listaDeAtividadesTarefasAparecendo;
    public GameObject listaDeAtividadesTarefas;
    private bool telaDeInformacoesDeAtividadesAparecendo;
    private bool arrowAtividadeAvaliativaMostrando;
    private bool arrowAtividadeTarefaMostrando;

    [Header("Tela de Informações de Atividade")]
    public ActivityButton activityButtonDaAtividade;
    public GameObject telaDeInformacoesDeAtividade;
    private ActivityButton activityButtonAtual;
    public TextMeshProUGUI informacaoHeaderText;
    public TextMeshProUGUI informacaoDescriptionText;
    public TextMeshProUGUI informacaoQuestoesText;
    public TextMeshProUGUI informacaoAttemptsText;
    public TextMeshProUGUI informacaoShortestTimeText;
    public TextMeshProUGUI informacaoGreatestTimeText;
    private Atividade atividadeAtual;
    private bool temCertezaPanelAparecendo;
    public GameObject temCertezaPanel;
    public Button iniciarButton;

    [Header("Animator das Telas")]
    public Animator animatorTelaDeAtividade;
    public Animator animatorTelaDeInformacoesDeAtividade;
    public Animator animatorTelaTemCerteza;
    public Animator animatorTelaDeConfiguracoes;
    public Animator animatorTelaDeTrocarSenha;
    public Animator animatorTelaDeSenhaTrocada;
    public Animator animatorArrowAtividadeAvaliativa;
    public Animator animatorListaAtividadeAvaliativa;
    public Animator animatorArrowAtividadeTarefa;
    public Animator animatorListaAtividadeTarefa;

    private void Awake()
    {
        listaDeAtividadesAvaliativasAparecendo = false;
        listaDeAtividadesTarefasAparecendo = false;
    }

    public void GetPessoaInfo(RespostaPadrao resposta, int id, int id2, int id3, bool isDefault)
    {
        if(resposta != null && resposta.GetOk())
        {
            Pessoa pessoa = JsonConvert.DeserializeObject<Pessoa>(resposta.dado.ToString());
            GameManager.Instance.saveManager.SavePessoa(pessoa);
            GameManager.Instance.SetPessoa(pessoa);

            SetPessoaInfo(pessoa.nome, pessoa.email);
            StartCoroutine(RestClient.Instance.Get(3, "pessoaFoto/" + pessoa.idPessoa, GetAndSetPessoaMidia, true));
        }
    }

    public void SetPessoaInfo(string nome, string email)
    {
        userNameText.text = nome;
        userEmailText.text = email;
    }

    public void GetAndSetPessoaMidia(RespostaPadrao resposta, int id, int id2, int id3, bool isDefault)
    {
        try
        {
            if(resposta.GetOk())
            {
                Midia midia = JsonConvert.DeserializeObject<Midia>(resposta.dado.ToString());
                if (midia != null)
                {
                    string url = GameConstants.UPLOAD_URL + "/Pessoa/" + midia.idMidia + midia.extensao;
                    saveManager.SaveUserProfileLink(url);
                    CallGetImage(url);
                }
            }
        }
        catch(ArgumentException ae)
        {
            Debug.LogWarning(ae);
        }
    }

    public void CallGetImage(string url)
    {
        StartCoroutine(GetImage(url));
    }

    IEnumerator GetImage(string url)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                print(www.error + "\n"+url);
                int idPessoa = GameManager.Instance.Pessoa.idPessoa;
                loginManager.DeletePhotoAndSearchAgain(idPessoa);
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(www);
                byte[] textureByte = DownloadHandlerTexture.GetContent(www).GetRawTextureData();

                SetPessoaMidia(texture);
            }        
        }
    }

    public void SetPessoaMidia(Texture2D texture)
    {
        userSpriteImage = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.0f), 1.0f);
        userImage.sprite = userSpriteImage;
        userProfileImage.sprite = userSpriteImage;
    }

    public void TrocarSenha()
    {
        string currentPassword = passwordInputText.text;
        string newPassword = newPasswordInputText.text;
        string newPasswordConfirm = newPasswordConfirmInputText.text;

        if (!string.IsNullOrEmpty(currentPassword))
        {
            if (!string.IsNullOrEmpty(newPassword))
            {
                if (!string.IsNullOrEmpty(newPasswordConfirm))
                {
                    if (newPassword.Equals(newPasswordConfirm))
                    {
                        Pessoa pessoa = GameManager.Instance.Pessoa;

                        UsuarioTrocaSenha trocaSenha = new UsuarioTrocaSenha();
                        trocaSenha.Id = pessoa.idPessoa;
                        trocaSenha.Senha = currentPassword;
                        trocaSenha.NovaSenha = newPassword;
                        trocaSenha.ConfirmacaoNovaSenha = newPasswordConfirm;
                        string trocaString = JsonConvert.SerializeObject(trocaSenha);
                        novaSenha = newPassword;

                        StartCoroutine(RestClient.Instance.Post(1, "trocasenha", trocaString, CheckIfSenhaWasChanged));
     
                    }
                    else
                    {
                        newPasswordConfirmMessageText.color = Color.red;
                        newPasswordConfirmMessageText.SetText("A nova senha e a confirmação estão diferentes!");
                    }
                }
                else
                {
                    newPasswordConfirmMessageText.color = Color.red;
                    newPasswordConfirmMessageText.SetText("Por favor, informe a confirmação da nova senha!");
                }
            }
            else
            {
                newPasswordMessageText.color = Color.red;
                newPasswordMessageText.SetText("Por favor, informe uma nova senha!");
            }
        }
        else
        {
            passwordMessageText.color = Color.red;
            passwordMessageText.SetText("Por favor, informe sua senha atual!");
        }
    }

    public void ClearMessageTexts()
    {
        passwordMessageText.SetText("");
        newPasswordMessageText.SetText("");
        newPasswordConfirmMessageText.SetText("");
    }

    private void CheckIfSenhaWasChanged(RespostaPadrao resposta, int id, int id2, int id3, bool isDefault)
    {
        if (resposta.GetOk())
        {
            ShowPasswordChangedSuccess();
        }
        else
        {
            newPasswordConfirmMessageText.color = Color.red;
            newPasswordConfirmMessageText.SetText(resposta.resposta);
        }
    }

    private void ShowPasswordChangedSuccess()
    {
        passwordInputText.text = "";
        newPasswordInputText.text = "";
        newPasswordConfirmInputText.text = "";

        TriggerChangedPasswordScreen();
        TriggerChangePasswordScreen();
    }

    public void TriggerConfigurationsScreen()
    {
        telaDeConfiguracoesAparecendo = !telaDeConfiguracoesAparecendo;
        if (telaDeConfiguracoesAparecendo)
            telaDeConfiguracoes.SetActive(true);
        else
        {
            animatorTelaDeConfiguracoes.SetTrigger("Fechar");
            StartCoroutine(SwitchTelaDeConfiguracoes());
        }
    }

    IEnumerator SwitchTelaDeConfiguracoes()
    {
        yield return new WaitForSeconds(.5f);
        telaDeConfiguracoes.SetActive(false);
    }

    public void TriggerChangePasswordScreen()
    {
        telaDeTrocarSenhaAparecendo = !telaDeTrocarSenhaAparecendo;
        if (telaDeTrocarSenhaAparecendo)
            passwordChangePanel.SetActive(true);
        else
        {
            animatorTelaDeTrocarSenha.SetTrigger("Fechar");
            StartCoroutine(CloseChangePasswordScreen());
        }
    }

    IEnumerator CloseChangePasswordScreen()
    {
        yield return new WaitForSeconds(.5f);
        passwordChangePanel.SetActive(false);
    }

    public void TriggerChangedPasswordScreen()
    {
        telaDeSenhaTrocadaAparecendo = !telaDeSenhaTrocadaAparecendo;
        if (telaDeSenhaTrocadaAparecendo)
            passwordChangedPanel.SetActive(true);
        else
        {
            animatorTelaDeSenhaTrocada.SetTrigger("Fechar");
            StartCoroutine(FechaTelaDeSenhaTrocada());
        }
    }

    IEnumerator FechaTelaDeSenhaTrocada()
    {
        yield return new WaitForSeconds(.5f);
        passwordChangedPanel.SetActive(false);
    }

    public void TriggerActivitiesScreen()
    {
        if (listaDeAtividadesAvaliativasAparecendo)
            TriggerListaAtividadesAvaliativas();
        if (listaDeAtividadesTarefasAparecendo)
            TriggerListaAtividadesTarefas();
        
        activitiesScreenAparecendo = !activitiesScreenAparecendo;
        if (activitiesScreenAparecendo)
            activitiesScreen.SetActive(activitiesScreenAparecendo);
        else
        {
            animatorTelaDeAtividade.SetTrigger("Fechar");
            StartCoroutine(SwitchActivitiesScreen());
        }
    }

    IEnumerator SwitchActivitiesScreen()
    {
        yield return new WaitForSeconds(.5f);

        activitiesScreen.SetActive(activitiesScreenAparecendo);
    }

    public void TriggerListaAtividadesAvaliativas()
    {
        listaDeAtividadesAvaliativasAparecendo = !listaDeAtividadesAvaliativasAparecendo;
        if (listaDeAtividadesAvaliativasAparecendo)
        {
            animatorArrowAtividadeAvaliativa.SetTrigger("Abrir");
            listaDeAtividadesAvaliativas.SetActive(true);
        }
        else
        {
            animatorListaAtividadeAvaliativa.SetTrigger("Fechar");
            StartCoroutine(FecharAtividadeAvaliativa());
            animatorArrowAtividadeAvaliativa.SetTrigger("Fechar");
        }
    }

    IEnumerator FecharAtividadeAvaliativa()
    {
        yield return new WaitForSeconds(.5f);
        listaDeAtividadesAvaliativas.SetActive(false);
    }

    public void TriggerListaAtividadesTarefas()
    {
        listaDeAtividadesTarefasAparecendo = !listaDeAtividadesTarefasAparecendo;
        if (listaDeAtividadesTarefasAparecendo)
        {
            animatorArrowAtividadeTarefa.SetTrigger("Abrir");
            listaDeAtividadesTarefas.SetActive(true);
        }
        else
        {
            animatorListaAtividadeTarefa.SetTrigger("Fechar");
            StartCoroutine(FecharAtividadeTarefa());
            animatorArrowAtividadeTarefa.SetTrigger("Fechar");
        }
    }

    IEnumerator FecharAtividadeTarefa()
    {
        yield return new WaitForSeconds(.5f);
        listaDeAtividadesTarefas.SetActive(false);
    }

    public void TriggerTelaDeInformacoesDeAtividade(GameObject activityButton = null)
    {
        if (activityButton != null)
        {
            activityButtonAtual = activityButton.GetComponent<ActivityButton>();
            atividadeAtual = activityButtonAtual.activity;
            activityButtonDaAtividade.setActivity(atividadeAtual);
            if (atividadeAtual.numeroTentativasAtuais < atividadeAtual.numeroTentativas || atividadeAtual.numeroTentativas == 0)
                iniciarButton.interactable = true;
            else
                iniciarButton.interactable = false;
            
            SetInformacoesDoCorpo();
        }

        telaDeInformacoesDeAtividadesAparecendo = !telaDeInformacoesDeAtividadesAparecendo;
        telaDeInformacoesDeAtividade.SetActive(telaDeInformacoesDeAtividadesAparecendo);
    }

    private void SetInformacoesDoCorpo()
    {
        informacaoQuestoesText.SetText(atividadeAtual.questoes.Count.ToString());
        string attempts = atividadeAtual.numeroTentativasAtuais + "/";
        if (atividadeAtual.numeroTentativas == 0)
            attempts += "∞";
        else
            attempts += atividadeAtual.numeroTentativas;

        informacaoAttemptsText.SetText(attempts);
        informacaoShortestTimeText.SetText(SecondsToMinutes(atividadeAtual.menorTempo));
        informacaoQuestoesText.SetText(atividadeAtual.numeroQuestoes.ToString());

        informacaoGreatestTimeText.SetText(SecondsToMinutes(atividadeAtual.maiorTempo));
    }

    private string SecondsToMinutes(int numSegundos)
    {
        int segundos = numSegundos % 60;
        int minutos = numSegundos / 60;
        string segundosText = segundos < 10f ? "0" + segundos.ToString() : segundos.ToString();
        string minutosText = minutos < 10f ? "0" + minutos.ToString() : minutos.ToString();

        return minutosText + ":" + segundosText;
    }

    public void TriggerTelaDeInformacoesDeAtividade()
    {
        activityButtonAtual = null;

        telaDeInformacoesDeAtividadesAparecendo = !telaDeInformacoesDeAtividadesAparecendo;
        animatorTelaDeInformacoesDeAtividade.SetTrigger("Fechar");
        StartCoroutine(SwitchInformacoesActivitiesScreen());
    }

    IEnumerator SwitchInformacoesActivitiesScreen()
    {
        yield return new WaitForSeconds(.5f);

        telaDeInformacoesDeAtividade.SetActive(telaDeInformacoesDeAtividadesAparecendo);
    }

    public void TriggerTemCertezaPanel()
    {
        temCertezaPanelAparecendo = !temCertezaPanelAparecendo;

        if (temCertezaPanelAparecendo)
            temCertezaPanel.SetActive(true);
        else
        {
            animatorTelaTemCerteza.SetTrigger("Nao");
            StartCoroutine(SwitchTemCertezaScreen());
        }
    }

    IEnumerator SwitchTemCertezaScreen()
    {
        yield return new WaitForSeconds(.5f);

        temCertezaPanel.SetActive(false);
    }

    public void TenhoCerteza()
    {
        animatorTelaTemCerteza.SetTrigger("Sim");
        StartCoroutine(IniciaJogo());
    }

    IEnumerator IniciaJogo()
    {
        yield return new WaitForSeconds(.5f);
        activityButtonAtual.loadActivityContent();
    }

    public static string Encrypt(string textToEncrypt)
    {
        try
        {
            string ToReturn = "";
            string publickey = "santhosh";
            string secretkey = "engineer";
            byte[] secretkeyByte = { };
            secretkeyByte = System.Text.Encoding.UTF8.GetBytes(secretkey);
            byte[] publickeybyte = { };
            publickeybyte = System.Text.Encoding.UTF8.GetBytes(publickey);
            MemoryStream ms = null;
            CryptoStream cs = null;
            byte[] inputbyteArray = System.Text.Encoding.UTF8.GetBytes(textToEncrypt);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                ms = new MemoryStream();
                cs = new CryptoStream(ms, des.CreateEncryptor(publickeybyte, secretkeyByte), CryptoStreamMode.Write);
                cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                cs.FlushFinalBlock();
                ToReturn = Convert.ToBase64String(ms.ToArray());
            }
            return ToReturn;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex.InnerException);
        }
    }
}
