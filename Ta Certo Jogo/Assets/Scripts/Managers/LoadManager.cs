using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadManager : MonoBehaviour
{
    [Header("Scriptable objects")]
    public AtividadeAtual atividadeAtualSO;
    public MainPlayer mainPlayer;

    [Header("Aux elements")]
    public GameObject loadScreen;
    private Animator loadScreenAnimator;
    public ScrollsManager scrollsManager;
    public OptionsManager optionsManager;
    public Animator telaSalaAnimator;

    // Lista de disciplinas existentes
    public List<Disciplina> disciplinasDefault { get; set; }
    public List<Disciplina> disciplinasDoCurso { get; set; }

    // Lista de ids das atividadesdisciplinas existentes
    public List<AtividadeDisciplina> atividadeDisciplinas { get; set; }
    public List<AtividadeDisciplina> atividadeDisciplinasCurso { get; set; }
    public List<AtividadeDisciplina> atividadeDisciplinasAux { get; set; }

    // Lista de Atividades existentes
    public List<Atividade> atividades { get; set; }

    // Lista de Questoes existentes
    public List<Questao> questoes { get; set; }

    private List<AtividadeAluno> atividadeAlunos { get; set; }

    private void Awake()
    {
        StartMyLists();
        ChecaTiposConhecidos();
    }

    private void Start()
    {
        loadScreenAnimator = loadScreen.GetComponent<Animator>();
    }

    private void ChecaTiposConhecidos()
    {
        if (PlayerPrefs.HasKey(GameConstants.USER_JA_SABE_CERTO_ERRADO))
            atividadeAtualSO.userJaSabeCertoErrado = true;
        else
            atividadeAtualSO.userJaSabeCertoErrado = false;

        if (PlayerPrefs.HasKey(GameConstants.USER_JA_SABE_LACUNAS))
            atividadeAtualSO.userJaSabeLacunas = true;
        else
            atividadeAtualSO.userJaSabeLacunas = false;
    }


    public void SetIfNeedSave(bool need)
    {
        atividadeAtualSO.precisaSalvarRespostas = need;
    }

    private void StartMyLists()
    {
        disciplinasDefault = new List<Disciplina>();
        disciplinasDoCurso = new List<Disciplina>();
        atividadeDisciplinas = new List<AtividadeDisciplina>();
        atividadeDisciplinasCurso = new List<AtividadeDisciplina>();
        atividades = new List<Atividade>();
        questoes = new List<Questao>();
        atividadeAlunos = new List<AtividadeAluno>();
    }

    public void OpenDemonstracao(string whereToReturn)
    {
        GameManager.Instance.PlayButton(whereToReturn);
    }

    public void LoadDefault()
    {
        // Carregar Disciplinas Existentes
        StartCoroutine(RestClient.Instance.Get(2, "", GetDisciplinas, true));
    }

    public void GetDisciplinas(RespostaPadrao resposta, int id, int id2, int id3, bool isDefault)
    {
        StartMyLists();
        if (resposta.GetOk()) { 
            int i = 0;
            if (isDefault) {
                disciplinasDefault = JsonConvert.DeserializeObject<List<Disciplina>>(resposta.dado.ToString());

                for (i = 0; i < disciplinasDefault.Count; i++)
                {
                    disciplinasDefault[i].isDefault = true;
                    StartCoroutine(RestClient.Instance.Get(2, "" + disciplinasDefault[i].idDisciplina, GetAtividadesDisciplinas, isDefault, i));
                }
                scrollsManager.CreateSubjectList(disciplinasDefault);
            }
            else
            {
                disciplinasDoCurso = JsonConvert.DeserializeObject<List<Disciplina>>(resposta.dado.ToString());

                for (i = 0; i < disciplinasDoCurso.Count; i++)
                {
                    disciplinasDoCurso[i].isDefault = false;
                    StartCoroutine(RestClient.Instance.Get(2, "" + disciplinasDoCurso[i].idDisciplina, GetAtividadesDisciplinas, isDefault, i));
                }
                scrollsManager.CreateClassSubjectList(disciplinasDoCurso);
            }
        }
        else
        {
            Debug.LogError("Erro");
        }
    }

    void GetAtividadesDisciplinas(RespostaPadrao resposta, int id, int id2, int id3,  bool isDefault)
    {
        if (resposta.GetOk())
        {
            atividadeDisciplinasAux = JsonConvert.DeserializeObject<List<AtividadeDisciplina>>(resposta.dado.ToString());
            if (isDefault)
            {
                foreach (AtividadeDisciplina ativDiscAux in atividadeDisciplinasAux)
                    atividadeDisciplinas.Add(ativDiscAux);

                foreach (AtividadeDisciplina ad in atividadeDisciplinasAux)
                    StartCoroutine(RestClient.Instance.Get(2, "info/" + ad.idTurmaDisciplinaAutor, GetAtividades, isDefault, id));
            }
            else
            {
                foreach (AtividadeDisciplina ativDiscAux in atividadeDisciplinasAux)
                {
                    bool canAdd = true;
                    foreach (AtividadeDisciplina ativDisc in atividadeDisciplinasCurso)
                    {
                        if (ativDisc.idDisciplina == ativDiscAux.idDisciplina)
                        {
                            canAdd = false;
                            break;
                        }
                    }
                    if (canAdd)
                        atividadeDisciplinasCurso.Add(ativDiscAux);
                }

                foreach (AtividadeDisciplina ad in atividadeDisciplinasCurso)
                {
                    if (!ad.baixado)
                    {
                        StartCoroutine(RestClient.Instance.Get(2, "info/" + ad.idTurmaDisciplinaAutor, GetAtividades, isDefault, id));
                        ad.baixado = true;
                    }

                }
            }
        }
        else
        {
            Debug.LogError("Erro");
        }
    }

    public void GetAtividadesFeitas(RespostaPadrao resposta, int id, int id2, int id3, bool isResult)
    {
        try
        {
            if (resposta.GetOk())
            {
                atividadeAlunos = JsonConvert.DeserializeObject<List<AtividadeAluno>>(resposta.dado.ToString());

                StartCoroutine(SetAtividadesFeitas());
            }
        }
        catch (ArgumentException e)
        {
            Debug.Log("json inválido - " + e);
        }
    }

    private IEnumerator SetAtividadesFeitas()
    {

        yield return new WaitForSeconds(1f);
        foreach (Disciplina disc in disciplinasDoCurso)
        {
            foreach (Atividade at in disc.atividades)
            {
                at.numeroTentativasAtuais = 0;
                at.idAtividadeAluno = 0;
                foreach (AtividadeAluno ativAluno in atividadeAlunos)
                {
                    if (at.idAtividade == ativAluno.idAtividade) {
                        at.numeroTentativasAtuais = ativAluno.numeroTentativas;
                        at.maiorNota = ativAluno.maiorNota;
                        at.menorTempo = ativAluno.menorTempo;
                        at.maiorTempo = ativAluno.maiorTempo;
                        at.idAtividadeAluno = ativAluno.idAtividadeAluno;
                        break;
                    }
                }
            }
        }

        telaSalaAnimator.SetTrigger("Abrir");
        //loadScreenAnimator.SetTrigger("Fechar");
        loadScreen.SetActive(false);
        //StartCoroutine(FecharLoadScreen());
    }

    IEnumerator FecharLoadScreen()
    {
        yield return new WaitForSeconds(.5f);
        loadScreen.SetActive(false);
    }

    void GetAtividades(RespostaPadrao resposta, int idDisciplina, int id2, int id3, bool isDefault)
    {
        if (resposta.GetOk()) { 
            List<Atividade> ativs = JsonConvert.DeserializeObject<List<Atividade>>(resposta.dado.ToString());
            if(ativs.Count == 0)
            {
                if (isDefault)
                    GameManager.Instance.saveManager.SaveDefaultContent(disciplinasDefault);
                telaSalaAnimator.SetTrigger("Abrir");
                loadScreen.SetActive(false);
                return;
            }
            if (isDefault)
            {
                foreach (Atividade at in ativs)
                    disciplinasDefault[idDisciplina].atividades.Add(at);

                int idAtividade = 0;
                foreach (Atividade at in ativs)
                {
                    StartCoroutine(RestClient.Instance.Get(2, "questoes/" + at.idAtividade, GetQuestoes, isDefault, idDisciplina, idAtividade));
                    idAtividade++;
                }
            }
            else
            {
                foreach (Atividade at in ativs)
                    disciplinasDoCurso[idDisciplina].atividades.Add(at);
            }
        }
        else
        {
            Debug.LogError("Erro");
        }
    }

    void GetQuestoes(RespostaPadrao resposta, int idDisciplina, int idAtividade, int id3, bool isDefault)
    {
        if (resposta.GetOk()) { 
            List<Questao> questoes = JsonConvert.DeserializeObject<List<Questao>>(resposta.dado.ToString());
            int idQuestao = 0;
            if (isDefault)
            {
                foreach (Questao q in questoes)
                {
                    q.GetConteudo();
                    StartCoroutine(RestClient.Instance.Get(3, "questaoFoto/" + q.idQuestao, GetQuestaoMidia, isDefault, idDisciplina, idAtividade, idQuestao));
                    disciplinasDefault[idDisciplina].atividades[idAtividade].questoes.Add(q);
                    idQuestao++;
                }
            }
            else
            {
                foreach (Questao q in questoes)
                {
                    q.GetConteudo();
                    StartCoroutine(RestClient.Instance.Get(3, "questaoFoto/" + q.idQuestao, GetQuestaoMidia, isDefault, idDisciplina, idAtividade, idQuestao));
                    disciplinasDoCurso[idDisciplina].atividades[idAtividade].questoes.Add(q);
                }
            }

            if (isDefault)
                GameManager.Instance.saveManager.SaveDefaultContent(disciplinasDefault);
        }
        else
        {
            Debug.LogError("Erro");
        }
        loadScreen.SetActive(false);
    }

    private void GetQuestaoMidia(RespostaPadrao resposta, int idDisciplina, int idAtividade, int idQuestao, bool isDefault)
    {
        try
        {
            if (resposta.GetOk())
            {
                Midia midia = JsonConvert.DeserializeObject<Midia>(resposta.dado.ToString());
                if(midia != null)
                {
                    if (isDefault)
                        disciplinasDefault[idDisciplina].atividades[idAtividade].questoes[idQuestao].temMidia = true;
                    else
                        disciplinasDoCurso[idDisciplina].atividades[idAtividade].questoes[idQuestao].temMidia = true;

                    string url = GameConstants.UPLOAD_URL + "/Midia/" + midia.idMidia + midia.extensao;
                    
                    GameManager.Instance.saveManager.SaveQuestionImage(url, idDisciplina, idAtividade, idQuestao);
                    StartCoroutine(GetImage(url, idDisciplina, idAtividade, idQuestao, isDefault));
                }
            }
        }
        catch (ArgumentException ae)
        {
            Debug.LogWarning(ae);
        }
    }

    IEnumerator GetImage(string url, int indexDisciplina, int indexAtividade, int indexQuestao, bool isDefault)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        Texture2D texture = DownloadHandlerTexture.GetContent(www);
        if (isDefault)
            disciplinasDefault[indexDisciplina].atividades[indexAtividade].questoes[indexQuestao].textura = texture;
        else
            disciplinasDoCurso[indexDisciplina].atividades[indexAtividade].questoes[indexQuestao].textura = texture;
    }

    public void loadActivitiesOfSubject(List<Atividade> atividades, string whereToReturn)
    {
        scrollsManager.CreateActivityList(atividades);
        PlayerPrefs.SetString("whereToReturn", whereToReturn);

        optionsManager.TriggerActivitiesScreen();
    }

    public void loadActivitiesOfDefaultSubject(int idDisciplina)
    {
        int i = 0;
        foreach(Disciplina disc in disciplinasDefault)
        {
            if (disc.idDisciplina == idDisciplina)
            {
                break;
            }
            i++;
        }
        scrollsManager.CreateActivityList(disciplinasDefault[i].atividades);
        optionsManager.TriggerActivitiesScreen();
    }

    /* LOAD FROM DEVICE */
    public void LoadFromDevice()
    {
        // Checa se tem dados salvos checando se os valores do PlayerPrefs
        if (PlayerPrefs.HasKey(GameConstants.NUM_DISCIPLINAS_PATH))
        {
            int numDisciplinas = PlayerPrefs.GetInt(GameConstants.NUM_DISCIPLINAS_PATH);
            LoadDisciplinasFromDevice(numDisciplinas);
        }
    }

    public void LoadPessoa()
    {
        string path = "/" + GameConstants.USER_PATH;
        if (FileExists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = GetStream(path);
            Pessoa pessoa = formatter.Deserialize(stream) as Pessoa;
            GameManager.Instance.SetPessoa(pessoa);
            optionsManager.SetPessoaInfo(pessoa.nome, pessoa.email);
            if(PlayerPrefs.GetInt(GameConstants.HAS_USER_DATA) == 2)
                LoadPessoaPhoto();
            else
                StartCoroutine(RestClient.Instance.Get(3, "pessoaFoto/" + pessoa.idPessoa, optionsManager.GetAndSetPessoaMidia, true));
            stream.Close();           
        }else
        {
            Debug.Log("Save file not found in " + path);
        }
    }

    /*private void LoadPessoaPhoto()
    {
        string path = "/" + GameConstants.USER_PHOTO_PATH;
        if (FileExists(path))
        {
            BinaryFormatter formmater = new BinaryFormatter();
            FileStream stream = GetStream(path);
            byte[] textureByte = null;
            textureByte = formmater.Deserialize(stream) as byte[];

            Texture2D texture = new Texture2D(PlayerPrefs.GetInt(GameConstants.USER_PHOTO_WIDTH), PlayerPrefs.GetInt(GameConstants.USER_PHOTO_HEIGHT), TextureFormat.RGBA32, mipmap, false);
            //Texture2D texture = new Texture2D(1, 1);
            texture.LoadRawTextureData(textureByte);
            texture.Apply();
            optionsManager.SetPessoaMidia(texture);
            stream.Close();
        }
        else
        {
            Debug.Log("Load file not found in " + path);
        }
    }*/

    private void LoadPessoaPhoto()
    {
        string path = "/" + GameConstants.USER_PHOTO_PATH;
        if (FileExists(path))
        {
            BinaryFormatter formmater = new BinaryFormatter();
            FileStream stream = GetStream(path);
            string url;
            url = formmater.Deserialize(stream) as string;
            optionsManager.CallGetImage(url);
            stream.Close();
        }
        else
        {
            Debug.Log("Load file not found in " + path);
        }
    }

    private void LoadDisciplinasFromDevice(int numDisciplinas)
    {
        FileStream stream = null;

        for (int i = 0; i < numDisciplinas; i++)
        {
            string path = "/disciplinas/disc" + i;
            if (FileExists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                stream = GetStream(path);
                Disciplina disciplina = formatter.Deserialize(stream) as Disciplina;
                disciplina.atividades = new List<Atividade>();
                disciplinasDefault.Add(disciplina);
                stream.Close();

                LoadAtividadesFromDevice(i);
            }
        }

        scrollsManager.CreateSubjectList(disciplinasDefault);
    }

    private void LoadAtividadesFromDevice(int numDisciplina)
    {
        FileStream stream = null;
        bool hasActivities = true;

        int numAtividade = 0;
        while (hasActivities)
        {
            string path = "/atividades/ativ-" + numDisciplina + "-" + numAtividade;
            if (FileExists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                stream = GetStream(path);
                Atividade atividade = formatter.Deserialize(stream) as Atividade;
                atividade.questoes = new List<Questao>();
                disciplinasDefault[numDisciplina].atividades.Add(atividade);
                stream.Close();

                LoadQuestoesFromDevice(numDisciplina, numAtividade);
                numAtividade++;
            }
            else
            {
                hasActivities = false;
            }
        }
    }

    private void LoadQuestoesFromDevice(int numDisciplina, int numAtividade)
    {
        FileStream stream = null;
        bool hasQuestions = true;

        int numQuestao = 0;
        while (hasQuestions)
        {
            string path = "/questoes/quest-" + numDisciplina + "-" + numAtividade + "-" + numQuestao;
            if (FileExists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                stream = GetStream(path);
                Questao questao = formatter.Deserialize(stream) as Questao;
                disciplinasDefault[numDisciplina].atividades[numAtividade].questoes.Add(questao);

                if (questao.temMidia)
                    LoadMidiaDaQuestao(numDisciplina, numAtividade, numQuestao);

                LoadConteudoFromDevice(numDisciplina, numAtividade,numQuestao);

                numQuestao++;
                stream.Close();
            }
            else
            {
                hasQuestions = false;
            }
        }

        loadScreen.SetActive(false);
    }

    private void LoadMidiaDaQuestao(int indexDisciplina, int indexAtividade, int indexQuestao)
    {
        FileStream stream = null;
        string path = "/midias/quest-" + indexDisciplina + "-" + indexAtividade + "-" + indexQuestao;
        if (FileExists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            stream = GetStream(path);
            string url = formatter.Deserialize(stream) as string;
            stream.Close();

            StartCoroutine(GetImage(url, indexDisciplina, indexAtividade, indexQuestao, true));
        }
    }

    private void LoadConteudoFromDevice(int numDisciplina, int numAtividade, int numQuestao)
    {
        FileStream stream = null;
        int tipo = disciplinasDefault[numDisciplina].atividades[numAtividade].questoes[numQuestao].idTipoQuestao;

        string path = "/conteudos/cont-" + numDisciplina + "-" +
            numAtividade + "-" + numQuestao + "-" + tipo;
        if (FileExists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            stream = GetStream(path);
            Conteudo conteudo = formatter.Deserialize(stream) as Conteudo;
            disciplinasDefault[numDisciplina].atividades[numAtividade].questoes[numQuestao].conteudo = conteudo;
            stream.Close();
        }
        else
        {
            print("Caminho " + path + ", não existe");
        }

        loadScreen.SetActive(false);
    }

    public FileStream GetStream(string pathName)
    {
        string path = Application.persistentDataPath + pathName + ".sesi";
        FileStream stream = new FileStream(path, FileMode.Open);

        return stream;
    }

    public bool FileExists(string pathName)
    {
        string path = Application.persistentDataPath + pathName + ".sesi";
        if (File.Exists(path))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}