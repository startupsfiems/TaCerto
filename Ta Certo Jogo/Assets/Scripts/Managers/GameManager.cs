using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Randomm = System.Random;


public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public MainPlayer mainPlayer;

    private PessoaToken pessoaToken = new PessoaToken();
    public PessoaToken PessoaToken { get => pessoaToken; set => pessoaToken = value; }
    public Pessoa Pessoa { get; set; }
    public Turma turma { get; set; }
    private bool precisaEnviarLogLogin = false;

  
    [Header("Managers")]
    public LoadManager loadManager;
    public SaveManager saveManager;
    public ScrollsManager scrollsManager;

    [Header("Screens")]
    public GameObject activityScreen;
    public TextMeshProUGUI activitiesScreenTitleText;

    /*[Header("Config Modal")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI emailText;
    public Image profilePictureImage;*/

    [Header("Current Activity")]
    public AtividadeAtual atividadeAtualSO;
    private Atividade atividadeAtual;
    private int numeroDeQuestoes = 0;
    private int idQuestaoAtual = 0;
    private List<Questao> questoes;
    private int[] ordemDosIndex;

    [Header("Game Scene Managers")]
    private GameMenuManager gameMenuManager;
    private CertoErradoManager certoErradoManager;
    private LacunaManager lacunaManager;
    private RespostaManager respostaManager;

    [Header("GameObject for Open Activity Scene")]
    public Button classButton;
    public Button demoButton;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = typeof(GameManager).Name;
                    _instance = go.AddComponent<GameManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (PlayerPrefs.HasKey(GameConstants.TOKEN_PATH) && PlayerPrefs.HasKey(GameConstants.ID_TOKEN_PATH))
            setPrecisaEnviarLogLogin(true);

        LoadMyThings();
        Pessoa = new Pessoa();
    }

    public bool getPrecisaEnviarLogLogin()
    {
        return precisaEnviarLogLogin;
    }

    public void setPrecisaEnviarLogLogin(bool value)
    {
        precisaEnviarLogLogin = value;
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnLoadCallback;
    }

    private void OnLoadCallback(Scene scene, LoadSceneMode sceneMode)
    {
        if(scene.buildIndex == 0) {
            LoadMyThings();
            /*if (PlayerPrefs.HasKey("whereToReturn"))
            {
                atividadeAtualSO.whereToReturn = PlayerPrefs.GetString("whereToReturn");
                PlayerPrefs.DeleteKey("whereToReturn");
            }

            if (atividadeAtualSO.whereToReturn.Equals("Login"))
            {
                BackToLoggedScreen();
            }
            else if (atividadeAtualSO.whereToReturn.Equals("Atividade"))
            {
                BackToActivityScreen();
            }else if (atividadeAtualSO.whereToReturn.Equals("disciplinasCurso"))
            {
                BackToClassScreen();
            }
            atividadeAtualSO.whereToReturn = "";*/
        }   
    }

    private void BackToLoggedScreen()
    {
        print("game manager backtologgerscreen");
    }

    private void BackToActivityScreen()
    {
        demoButton.onClick.Invoke();
        SetDisciplina(atividadeAtualSO.disciplina);
        SetActivitiesTitleText(atividadeAtualSO.disciplina);
        loadManager.loadActivitiesOfDefaultSubject(atividadeAtualSO.idDisciplina);
    }

    private void BackToClassScreen()
    {
        classButton.onClick.Invoke();
    }

    public void IsAlive()
    {
        print(name + " is alive");
    }

    public void SendLogLogin()
    {
        if (getPrecisaEnviarLogLogin())
        {
            setPrecisaEnviarLogLogin(false);
            LogLogin logLogin = new LogLogin();
            logLogin.idPessoa = Pessoa.idPessoa;
            logLogin.horaAcesso = DateTime.Now.ToString();
            logLogin.plataforma = Application.platform.ToString();
            logLogin.deviceId = SystemInfo.deviceUniqueIdentifier;
            logLogin.deviceIp = GetLocalIPAddress();
            logLogin.origem = 0;

            string logLoginJson = JsonConvert.SerializeObject(logLogin);
            StartCoroutine(RestClient.Instance.Post(1, "saveloglogin", logLoginJson, SendLogLoginCallBack));
        }
    }

    private void SendLogLoginCallBack(RespostaPadrao resposta, int id, int id2, int id3, bool isResult)
    {
        if (resposta.GetOk())
        {
            print(resposta.resposta);
        }
        else
        {
            print(resposta.resposta);
        }
    }

    public string GetLocalIPAddress()
    {
        var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }

        throw new Exception("No network adapters with an IPv4 address in the system!");
    }

    private void LoadMyThings()
    {
        loadManager = GameObject.FindWithTag("LoadSaveManager").GetComponent<LoadManager>();
        saveManager = GameObject.FindWithTag("LoadSaveManager").GetComponent<SaveManager>();
        scrollsManager = GameObject.FindWithTag("ScrollsManager").GetComponent<ScrollsManager>();

        activityScreen = GameObject.FindWithTag("AtividadeScreen");
        activitiesScreenTitleText = GameObject.FindWithTag("AtividadeScreenTitle").GetComponent<TextMeshProUGUI>();

        activityScreen.SetActive(false);

        mainPlayer = loadManager.mainPlayer;
        atividadeAtualSO = loadManager.atividadeAtualSO;
    }

    public void SwitchActivityScreen(bool op)
    {
        activityScreen.SetActive(op);
    }

    public void SetActivitiesTitleText(string title)
    {
        activitiesScreenTitleText.text = title;
    }

    public void SetPessoa(Pessoa p)
    {
        Pessoa = p;
        mainPlayer.idPessoa = Pessoa.idPessoa;
        mainPlayer.nome = Pessoa.nome;
        mainPlayer.email = Pessoa.email;

        SendLogLogin();
    }

    public void SetIdDisciplina(int id)
    {
        atividadeAtualSO.idDisciplina = id;
    }

    public void SetDisciplina(string disciplina)
    {
        atividadeAtualSO.disciplina = disciplina;
    }

    public void PlayButton(string whereToReturn = "Login")
    {
        if (PlayerPrefs.HasKey("whereToReturn"))
            PlayerPrefs.DeleteKey("whereToReturn");

        int disciplinaIndex = UnityEngine.Random.Range(0, loadManager.disciplinasDefault.Count);
        int atividadeIndex = UnityEngine.Random.Range(0, loadManager.disciplinasDefault[disciplinaIndex].atividades.Count);
        OpenThisActivity(loadManager.disciplinasDefault[disciplinaIndex].atividades[atividadeIndex], whereToReturn);
    }

    public void OpenThisActivity(Atividade atividade, string whereToReturn = "Atividade", bool precisaSalvar = false)
    {
        atividadeAtual = atividade;
        atividadeAtualSO.idAtividade = atividade.idAtividade;
        atividadeAtualSO.titulo = atividade.titulo;
        atividadeAtualSO.acertos = 0;
        atividadeAtualSO.erros = 0;
        atividadeAtualSO.numeroTentativas = atividade.numeroTentativas;
        atividadeAtualSO.questoes = atividade.questoes;
        numeroDeQuestoes = atividade.questoes.Count;
        atividadeAtualSO.numQuestoes = numeroDeQuestoes;
        atividadeAtualSO.maiorNota = atividade.maiorNota;
        atividadeAtualSO.menorTempo = atividade.menorTempo;
        atividadeAtualSO.isAleatorio = atividade.isAleatorio;
        atividadeAtualSO.maiorTempo = atividade.maiorTempo;
        atividadeAtualSO.precisaSalvarRespostas = precisaSalvar;
        atividadeAtualSO.numeroTentativasAtuais = atividade.numeroTentativasAtuais;
        atividadeAtualSO.idAtividadeAluno = atividade.idAtividadeAluno;
        atividadeAtualSO.whereToReturn = whereToReturn;

        OpenScene("Jogo");
    }

    public void PlayAgainButton(int tentativasAtuais, int idAtividadeAluno)
    {
        atividadeAtual.numeroTentativasAtuais = tentativasAtuais;
        atividadeAtual.idAtividadeAluno = idAtividadeAluno;
        OpenThisActivity(atividadeAtual, "Atividade", true);
    }

    private void OpenScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void LoadGameSceneElements()
    {
        certoErradoManager = GameObject.FindWithTag("CertoErradoManager").GetComponent<CertoErradoManager>();
        gameMenuManager = GameObject.FindWithTag("GameMenuManager").GetComponent<GameMenuManager>();
        respostaManager = GameObject.FindWithTag("RespostaManager").GetComponent<RespostaManager>();
        lacunaManager = GameObject.FindWithTag("LacunaManager").GetComponent<LacunaManager>();

        gameMenuManager.setBackgroundImage(atividadeAtualSO.disciplina);

        StartAtividade();
    }

    private void StartAtividade()
    {
        idQuestaoAtual = 0;

        respostaManager.SetNotaSomada(atividadeAtualSO.questoes);
        gameMenuManager.StartGame();
        ordemDosIndex = new int[atividadeAtualSO.numQuestoes];
        GeraOrdemDeInsercao();

        CarregaQuestao(atividadeAtualSO.questoes[ordemDosIndex[idQuestaoAtual]]);
    }

    private void GeraOrdemDeInsercao()
    {
        for (int i = 0; i < ordemDosIndex.Length; i++)
            ordemDosIndex[i] = i;

        if (atividadeAtualSO.isAleatorio) { 
            Randomm random = new Randomm();

            int n = ordemDosIndex.Length;
            for (int i = 0; i < (n - 1); i++)
            {
                int r = i + random.Next(n - i);
                int item = ordemDosIndex[r];
                ordemDosIndex[r] = ordemDosIndex[i];
                ordemDosIndex[i] = item;
            }
        }
    }

    private void CarregaQuestao(Questao questao)
    {
        int tipo = questao.idTipoQuestao;

        if (tipo == 1)
        {
            if (questao.temMidia)
                certoErradoManager.AtivaCertoErradoComFoto(ordemDosIndex[idQuestaoAtual], questao.textura);
            else
                certoErradoManager.AtivaCertoErradoSemFoto(ordemDosIndex[idQuestaoAtual]);
        }else if(tipo == 2)
        {
            lacunaManager.AtivaLacuna(ordemDosIndex[idQuestaoAtual]);
        }
        else if (tipo == 3)
        {
            print("Carregar o modo Colunas");
        }
        else if (tipo == 4)
        {
            print("Carregar o modo Associação");
        }
    }

    public void NextQuestionOrFinish()
    {
        numeroDeQuestoes = atividadeAtualSO.questoes.Count;
        idQuestaoAtual++;

        if (numeroDeQuestoes > idQuestaoAtual)
        {
            CarregaQuestao(atividadeAtualSO.questoes[ordemDosIndex[idQuestaoAtual]]);
        }
        else
        {
            //certoErradoManager.AcabouAtividade();
            gameMenuManager.showMenuAtividadeCompleta();
        }
    }
}