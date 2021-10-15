using Newtonsoft.Json;
using System;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField emailInputField;
    public TextMeshProUGUI emailMessageText;
    public TMP_InputField passwordInputField;
    public TextMeshProUGUI passwordMessageText;

    public GameObject menuDeslogado;
    public GameObject menuLogado;
    public Animator menuLogadoAnimator;

    public LoadManager loadManager;

    public OptionsManager optionsManager;

    //variaveis utilizadas na correcao do input
    private TextMeshProUGUI placeHolderEmail;
    private TextMeshProUGUI placeHolderPassword;
    private bool keepOldTextInFieldEmail;
    private bool keepOldTextInFieldPassword;
    private string oldEditTextEmail;
    private string editTextEmail;
    private string oldEditTextPassword;
    private string editTextPassword;
    //fim

    private void Awake()
    {
        RestClient.Instance.IsAlive();
        GameManager.Instance.IsAlive();

        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.DeleteKey(GameConstants.USER_JA_SABE_CERTO_ERRADO);
        //PlayerPrefs.DeleteKey(GameConstants.USER_JA_SABE_LACUNAS);
        CheckIfHasToken();
    }

    private void Start()
    {
        #region Uso dos metodos de manutencao de valor nos inputs no uso do botao de voltar do celular
        placeHolderEmail = (TextMeshProUGUI)emailInputField.placeholder;
        emailInputField.onEndEdit.AddListener(EndEditEmail);
        emailInputField.onValueChanged.AddListener(EditingEmail);
        emailInputField.onTouchScreenKeyboardStatusChanged.AddListener(ReportChangeStatusEmail);

        placeHolderPassword = (TextMeshProUGUI)passwordInputField.placeholder;
        passwordInputField.onEndEdit.AddListener(EndEditPassword);
        passwordInputField.onValueChanged.AddListener(EditingPassword);
        passwordInputField.onTouchScreenKeyboardStatusChanged.AddListener(ReportChangeStatusPassword); 
        #endregion
    }


    public void CheckIfHasToken()
    {
        if (PlayerPrefs.HasKey(GameConstants.TOKEN_PATH) && PlayerPrefs.HasKey(GameConstants.ID_TOKEN_PATH))
        {
            loadManager.loadScreen.SetActive(true);

            int idtoken = PlayerPrefs.GetInt(GameConstants.ID_TOKEN_PATH);
            StartCoroutine(RestClient.Instance.Get(1, "token/"+ idtoken, GetPessoaToken, true));
        }
        else
        {
            menuDeslogado.SetActive(true);
            loadManager.loadScreen.SetActive(false);
        }
    }

    public void validarELogar(){
        string email = emailInputField.text.ToString();
        string password = passwordInputField.text.ToString();

        if (string.IsNullOrEmpty(email))
        {
            emailMessageText.color = Color.red;
            emailMessageText.text = "Por favor, informe o e-mail!";
            return;
        }else if (string.IsNullOrEmpty(password))
        {
            passwordMessageText.color = Color.red;
            passwordMessageText.text = "Por favor, informe a senha!";
            return;
        }

        PessoaLogin pessoaLogin = new PessoaLogin(email, password);
        string pessoaString = JsonConvert.SerializeObject(pessoaLogin);
        loadManager.loadScreen.SetActive(true);
        GameManager.Instance.setPrecisaEnviarLogLogin(true);
        StartCoroutine(RestClient.Instance.Post(1, "", pessoaString, GetPessoaToken));
    }

    public void ClearLoginMessageText()
    {
        emailMessageText.text = "";
        passwordMessageText.text = "";
    }

    public void Deslogar()
    {
        PessoaToken pessoaToken = GameManager.Instance.PessoaToken;
        StartCoroutine(RestClient.Instance.Post(1, "logout?IdPessoaToken="+pessoaToken.idPessoaToken, null, GetRemoveTokenResponse, needUpload: false));
        
        GameManager.Instance.PessoaToken = null;
        PlayerPrefs.DeleteKey(GameConstants.TOKEN_PATH);
        PlayerPrefs.DeleteKey(GameConstants.ID_TOKEN_PATH);
        PlayerPrefs.DeleteKey(GameConstants.HAS_USER_DATA);
        PlayerPrefs.Save();

        DeleteFile("/" + GameConstants.USER_PHOTO_PATH);
        DeleteFile("/" + GameConstants.USER_PATH);

        loadManager.scrollsManager.ClearActivities();

        menuDeslogado.SetActive(true);
        menuLogado.SetActive(false);
    }

    private void DeleteFile(string path)
    {
        string filePath = Application.persistentDataPath + path + ".sesi";
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            RefreshEditorProjectWindow();
        }
    }

    public void DeletePhotoAndSearchAgain(int idPessoa)
    {
        DeleteFile("/" + GameConstants.USER_PHOTO_PATH);
        PlayerPrefs.SetInt(GameConstants.HAS_USER_DATA, 1);

        StartCoroutine(RestClient.Instance.Get(3, "pessoaFoto/" + idPessoa, optionsManager.GetAndSetPessoaMidia, true));
    }

    private void RefreshEditorProjectWindow()
    {
        #if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
        #endif
    }

    void GetRemoveTokenResponse(RespostaPadrao resposta, int id, int id2, int id3, bool isResult)
    {
        print(resposta.resposta);
    }

    void GetPessoaToken(RespostaPadrao resposta, int id, int id2, int id3, bool isResult)
    {
        try
        {
            if (resposta.GetOk())
            {
                PessoaToken pessoaToken = JsonConvert.DeserializeObject<PessoaToken>(resposta.dado.ToString());
                if (pessoaToken.autenticado)
                {
                    GameManager.Instance.PessoaToken = pessoaToken;
                    bool needToOpen = false;
                    PlayerPrefs.SetString("token", pessoaToken.token);
                    PlayerPrefs.SetInt("idtoken", pessoaToken.idPessoaToken);
                    PlayerPrefs.Save();

                    if (pessoaToken.idTurma != -1)
                    {
                        StartCoroutine(RestClient.Instance.Get(1, "turma/" + pessoaToken.idTurma, GetTurma, false));
                    }
                    else
                    {
                        needToOpen = true;
                        loadManager.loadScreen.SetActive(false);
                        loadManager.scrollsManager.ShowHasNoSubjecties();
                    }

                    if (PlayerPrefs.HasKey(GameConstants.HAS_USER_DATA))
                    {
                        loadManager.LoadPessoa();
                    }
                    else
                    {
                        StartCoroutine(GetAfterSomeSeconds(pessoaToken.idPessoa));
                    }

                    menuDeslogado.SetActive(false);
                    menuLogado.SetActive(true);
                    if (needToOpen)
                        StartCoroutine(OpenMainMenu());
                }
                else
                {
                    MostraLogin();
                }
            }
            else
            {
                if (resposta != null && resposta.codigo != 200)
                {
                    passwordMessageText.color = Color.red;
                    passwordMessageText.text = resposta.resposta;
                }
                MostraLogin();
            }
        }
        catch
        {
            if (resposta != null && resposta.codigo != 200)
            {
                passwordMessageText.color = Color.red;
                passwordMessageText.text = resposta.resposta;
            }
            menuDeslogado.SetActive(true);
            loadManager.loadScreen.SetActive(false);
        }
    } 

    private void MostraLogin()
    {
        menuDeslogado.SetActive(true);
        menuLogado.SetActive(false);
        loadManager.loadScreen.SetActive(false);
    }

    IEnumerator OpenMainMenu()
    {
        yield return new WaitForSeconds(.2f);
        menuLogadoAnimator.SetTrigger("Abrir");
    }

    IEnumerator GetAfterSomeSeconds(int idPessoa)
    {
        yield return new WaitForSeconds(.5f);

        StartCoroutine(RestClient.Instance.Get(1, "" + idPessoa, optionsManager.GetPessoaInfo, true));
        loadManager.loadScreen.SetActive(false);
    }

    void GetTurma(RespostaPadrao resposta, int id, int id2, int id3, bool isResult)
    {
        try
        {
            if (resposta.GetOk())
            {
                Turma pessoaTurma = JsonConvert.DeserializeObject<Turma>(resposta.dado.ToString());
                GameManager.Instance.turma = pessoaTurma;

                //Pedir disciplinas da turma aqui
                StartCoroutine(RestClient.Instance.Get(2, "disciplinas/" + pessoaTurma.idTurma, GetDisciplinasDaTurma, false));
            }
            else
                Debug.Log("Erro ao recuperar informações de Turma");
        }
        catch (ArgumentException e)
        {
            UnityEngine.Debug.Log("json inválido - " + e);
        }
    }

    void GetDisciplinasDaTurma(RespostaPadrao resposta, int id, int id2, int id3, bool isDefault)
    {
        if (resposta.GetOk()) {
            loadManager.GetDisciplinas(resposta, id, id2, id3, isDefault);
            StartCoroutine(RestClient.Instance.Get(2, "peganumerodeatividadesfeitas/" + GameManager.Instance.PessoaToken.idPessoa, loadManager.GetAtividadesFeitas, false));
        }
    }

    #region Metodos para corrigir texto do input
    private void ReportChangeStatusEmail(TouchScreenKeyboard.Status newStatus)
    {
        if (newStatus == TouchScreenKeyboard.Status.Canceled)
            keepOldTextInFieldEmail = true;
    }

    private void ReportChangeStatusPassword(TouchScreenKeyboard.Status newStatus)
    {
        if (newStatus == TouchScreenKeyboard.Status.Canceled)
            keepOldTextInFieldPassword = true;
    }

    private void EditingEmail(string currentText)
    {
        oldEditTextEmail = editTextEmail;
        editTextEmail = currentText;
    }

    private void EditingPassword(string currentText)
    {
        oldEditTextPassword = editTextPassword;
        editTextPassword = currentText;
    }

    private void EndEditEmail(string currentText)
    {
        if (keepOldTextInFieldEmail)
        {
            //IMPORTANT ORDER
            editTextEmail = oldEditTextEmail;
            emailInputField.text = oldEditTextEmail;

            keepOldTextInFieldEmail = false;
        }
    }

    private void EndEditPassword(string currentText)
    {
        if (keepOldTextInFieldPassword)
        {
            //IMPORTANT ORDER
            editTextPassword = oldEditTextPassword;
            passwordInputField.text = oldEditTextPassword;

            keepOldTextInFieldPassword = false;
        }
    }

    #endregion Metodos para corrigir texto do input
}