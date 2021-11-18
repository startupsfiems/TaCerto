using Newtonsoft.Json;
using System;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class RestClient : MonoBehaviour
{
    private static RestClient _instance;
    public int test = 1231;
    //private const string BASE_URL = "https://192.168.137.131:5200";
    //private const string BASE_URL = "http://localhost:5000";
    // private const string BASE_URL = "https://localhost:5001";
    //private const string BASE_URL = "http://10.47.18.128:5000"; //usado para fazer conexão com api local
    private const string BASE_URL = "http://tacertoapi.sesims.com.br";
    private const string PESSOAS_URL = BASE_URL + "/api/pessoas/";
    private const string ATIVIDADES_URL = BASE_URL + "/api/atividades/";
    private const string MIDIAS_URL = BASE_URL + "/api/midias/";

    private GameObject telaDesconectado;
    private TextMeshProUGUI desconectadoMessageText;
    private bool internetConnection = true;
    private bool serverConnection = true;
    private int internetErrorCount = 0;
    private int serverErrorCount = 0;

    // Possíveis mensagens para mostrar ao usuário
    private const string noInternetConnectionMessage = "Você está offline";
    private const string noServerConnectionMessage = "Falha na Conexão";
    private const string serverUpdateMessage = "Servidor offline - Estamos Atualizando";

    private void Start()
    {
        LoadMyThings();
        gameObject.transform.parent = null;
        SceneManager.sceneLoaded += OnLoadCallback;
        CheckInternet();
    }

    private void OnLoadCallback(Scene scene, LoadSceneMode sceneMode)
    {
        LoadMyThings();
    }

    private void LoadMyThings()
    {
        telaDesconectado = GameObject.FindWithTag("TelaDesconectado");
        desconectadoMessageText = GameObject.FindWithTag("DesconectadoMessageText").GetComponent<TextMeshProUGUI>();
        telaDesconectado.SetActive(false);
    }

    public static RestClient Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<RestClient>();
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = typeof(RestClient).Name;
                    _instance = go.AddComponent<RestClient>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    public void IsAlive()
    {
        print(name + " is alive");
    }

    public IEnumerator Get(int op, string offset, Action<RespostaPadrao, int, int, int, bool> callBack, bool isDefault, int id = -1, int id2 = -1, int id3 = -1)
    {
        Debug.Log("To no GET");

        string url = GetUrl(op) + offset;
        Debug.Log("url = " + url);
        using (UnityWebRequest unityWebRequest = new UnityWebRequest())
        {
            SetUpUnityWebRequest(unityWebRequest, url, "get", true);

            yield return unityWebRequest.SendWebRequest();

            if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("eNTROU NO ERRO");

                CheckInternet();
            }
            else
            {
                Debug.Log("FPO Ç[A");

                if (unityWebRequest.isDone && unityWebRequest.downloadHandler.isDone)
                {
                    string jsonResult = Encoding.UTF8.GetString(unityWebRequest.downloadHandler.data);
                    Debug.Log(jsonResult);
                    try
                    {
                        RespostaPadrao resposta = JsonConvert.DeserializeObject<RespostaPadrao>(jsonResult);

                        callBack(resposta, id, id, id, isDefault);
                    }
                    catch
                    {
                        Debug.LogError(url + "\n" + jsonResult);
                        callBack(null, id, id, id, isDefault);
                    }
                }
            }
            unityWebRequest.certificateHandler?.Dispose();
        }
    }

    public IEnumerator Post(int op, string offset, string jsonBody, Action<RespostaPadrao, int, int, int, bool> callBack, int id = -1, bool needUpload = true)
    {
        string url = GetUrl(op) + offset;
        Debug.Log(url);

        using (UnityWebRequest unityWebRequest = new UnityWebRequest())
        {
            SetUpUnityWebRequest(unityWebRequest, url, "post", true, needUpload, jsonBody);

            yield return unityWebRequest.SendWebRequest();
            Debug.Log("yield");

            if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError("Código de resposta: " + unityWebRequest.error);
                CheckInternet();
            }
            else
            {
                Debug.Log("eita " + unityWebRequest.isDone);
                Debug.Log("eita 2 " + unityWebRequest.downloadHandler.isDone);
                if (unityWebRequest.isDone && unityWebRequest.downloadHandler.isDone)
                {
                    string jsonResult = Encoding.UTF8.GetString(unityWebRequest.downloadHandler.data);
                    Debug.Log(jsonResult);
                    try
                    {
                        RespostaPadrao resposta = JsonConvert.DeserializeObject<RespostaPadrao>(jsonResult);
                        callBack(resposta, id, id, id, true);
                    }
                    catch
                    {
                        Debug.LogError(url + "\n" + jsonResult);
                        callBack(null, id, id, id, true);
                    }
                }
            }

            unityWebRequest.certificateHandler?.Dispose();
        }
    }

   
    private string GetUrl(int op)
    {
        if (op == 1)
        {
            return PESSOAS_URL;
        }
        else if (op == 2)
        {
            return ATIVIDADES_URL;
        } else if(op == 3)
        {
            return MIDIAS_URL;
        }else
        {
            return PESSOAS_URL;
        }
    }

    private UnityWebRequest SetUpUnityWebRequest(UnityWebRequest unityWebRequest, string url, string method, bool needDownload = false, bool needUpload = false, string bodyJson = null)
    {
        unityWebRequest.url = url;

        unityWebRequest.SetRequestHeader("content-Type", "application/json");
        unityWebRequest.SetRequestHeader("Accept", "application/json");
        unityWebRequest.SetRequestHeader("api-version", "0.1");

        string token = null;
        if (PlayerPrefs.HasKey("token"))
            token = PlayerPrefs.GetString("token");

        unityWebRequest.SetRequestHeader("Authorization", "Bearer " + token);
        if (method.Equals("get"))
        {
            unityWebRequest.method = UnityWebRequest.kHttpVerbGET;
        }
        else if (method.Equals("post"))
        {
            unityWebRequest.method = UnityWebRequest.kHttpVerbPOST;
        }

        if (needDownload)
        {
            DownloadHandlerBuffer _downloadHandler = new DownloadHandlerBuffer();
            unityWebRequest.downloadHandler = _downloadHandler;
        }
        if (needUpload)
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJson);
            unityWebRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        }

        unityWebRequest.useHttpContinue = false;
        //unityWebRequest.chunkedTransfer = false;
        unityWebRequest.redirectLimit = 0;

        AcceptAllCertificatesSignedWithASpecificKeyPublicKey certHandler = new AcceptAllCertificatesSignedWithASpecificKeyPublicKey();
        unityWebRequest.certificateHandler = certHandler;

        return unityWebRequest;

    }

    private void CheckInternet()
    {
        //CheckIfHasInternetConnection();
        CheckIfServerHasConnection();
    }

    private void CheckIfHasInternetConnection()
    {
        StartCoroutine(CheckInternetConnection((isConnected) =>
        {
            internetConnection = isConnected;
            if (isConnected)
            {
                print("Usuário está conectado à internet");
                if (serverConnection)
                {
                    telaDesconectado.SetActive(false);
                    if (internetErrorCount > 0)
                        SceneManager.LoadScene("Menus");
                }

                internetErrorCount = 0;
            }
            else
            {
                internetErrorCount += 1;
                telaDesconectado.SetActive(true);
                desconectadoMessageText.text = noInternetConnectionMessage;
                StartCoroutine(WaitSomeSeconds(5f, false));
            }
        }));
    }

    IEnumerator CheckInternetConnection(Action<bool> action)
    {
        string url = "https://google.com";
        using (UnityWebRequest unityWebRequest = new UnityWebRequest())
        {
            unityWebRequest.url = url;

            yield return unityWebRequest.SendWebRequest();

            if (unityWebRequest.error != null)
            {
                action(false);
            }
            else
            {
                action(true);
            }
            unityWebRequest.certificateHandler?.Dispose();
        }
    }

    private void CheckIfServerHasConnection()
    {
        StartCoroutine(checkServerConnection((responseCode) =>
        {
            // Código de resposta == 200 == server ok
            // Código de resposta == 0 == server not ok
            print("Check server = " + responseCode);
            if (responseCode == 200)
            {
                serverConnection = true;
                if (internetConnection)
                    telaDesconectado.SetActive(false);
                if (serverErrorCount > 0)
                    SceneManager.LoadScene("Menus");

                serverErrorCount = 0;
            }
            else if (responseCode == 0 || responseCode == 504)
            {
                serverErrorCount += 1;

                serverConnection = false;
                telaDesconectado.SetActive(true);
                desconectadoMessageText.text = noServerConnectionMessage;
                StartCoroutine(WaitSomeSeconds(5f, true));
            }
        }));
    }

    IEnumerator WaitSomeSeconds(float sec, bool isServerCheck)
    {
        yield return new WaitForSeconds(sec);

        if (isServerCheck)
            CheckIfServerHasConnection();
        else
            CheckIfHasInternetConnection();
    }

    IEnumerator checkServerConnection(Action<long> action)
    {
        string url = PESSOAS_URL + "checaConexao";
        Debug.Log(url);
        using (UnityWebRequest unityWebRequest = new UnityWebRequest())
        {
            SetUpUnityWebRequest(unityWebRequest, url, "get", true);
            yield return unityWebRequest.SendWebRequest();
            if (unityWebRequest.error != null)
            {
                action(unityWebRequest.responseCode);
            }
            else
            {
                action(unityWebRequest.responseCode);
            }
            unityWebRequest.certificateHandler?.Dispose();
        }
    }
}