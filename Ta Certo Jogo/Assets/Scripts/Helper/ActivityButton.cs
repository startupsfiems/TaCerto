using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ActivityButton : MonoBehaviour
{
    public Atividade activity;
    private Color buttonColor;
    public TextMeshProUGUI activityNameText;
    public TextMeshProUGUI activityDateText;
    public TextMeshProUGUI activityStateText;
    public TextMeshProUGUI activityNotaText;
    public Image stateImage;
    public Button atividadeButton;
    private DateTime dataFim;

    [Header("Imagens de Estado")]
    public Sprite naoEntregueImage;
    public Sprite pendenteImage;
    public Sprite entregueImage;
    public Sprite entregueAtrasadaImage;

    private int questionsNumber;
    private int loadedCount = 0;
    private int midiasLoadCount = 0;
    private OptionsManager optionsManager;

    private void Start()
    {
        optionsManager = GameObject.FindWithTag("OptionsManager").GetComponent<OptionsManager>();
    }

    public void setActivity(Atividade atividade)
    {
        activity = atividade;
        atividade.questoes = new List<Questao>();
        setNameText();
        setDateText();
        if (activity.isProva)
            setNotaText();
        else
            setStateImage();
    }

    private void setNameText()
    {
        activityNameText.text = activity.titulo.ToString();

    }

    private void setDateText()
    {
        dataFim = Convert.ToDateTime(activity.dataFim);
        activityDateText.SetText("Prazo: " + dataFim.ToString("dd/MM/yyyy"));
    }

    private void setNotaText()
    {
        stateImage.gameObject.SetActive(false);
        activityNotaText.gameObject.SetActive(true);
        if (activity.numeroTentativasAtuais > 0)
        {
            activityNotaText.SetText(activity.maiorNota.ToString($"F{2}"));
            activityStateText.SetText("Entregue");
        }
        else
        {
            activityNotaText.SetText("0.00");
            if (DateTime.Now > dataFim)
                activityStateText.SetText("Não entregue");
            else
                activityStateText.SetText("Pendente");
        }
    }

    private void setStateImage()
    {
        stateImage.gameObject.SetActive(true);
        activityNotaText.gameObject.SetActive(false);
        if (activity.numeroTentativasAtuais > 0)
        {
            activityStateText.SetText("Entregue");
            stateImage.sprite = entregueImage;
        }
        else
        {
            if (DateTime.Now > dataFim)
            {
                activityStateText.SetText("Não entregue");
                stateImage.sprite = naoEntregueImage;
            }
            else
            {
                activityStateText.SetText("Pendente");
                stateImage.sprite = pendenteImage;
            }
        }
    }

    public void avoidClickToButton()
    {
        atividadeButton.interactable = false;
    }

    public void setButtonColor(Color color)
    {
        buttonColor = color;
    }

    public void showActivityInformationsScreen()
    {
        optionsManager.TriggerTelaDeInformacoesDeAtividade(gameObject);
    }

    public void loadActivityContent()
    {
        GameManager.Instance.loadManager.loadScreen.SetActive(true);
        StartCoroutine(RestClient.Instance.Get(2, "questoes/" + activity.idAtividade, GetQuestoes, false));
    }

    private void GetQuestoes(RespostaPadrao resposta, int id, int id2, int id3, bool isDefault)
    {
        if (resposta.GetOk()) { 
            List<Questao> questoes = JsonConvert.DeserializeObject<List<Questao>>(resposta.dado.ToString());

            int idQuestao = 0;
            foreach(Questao q in questoes)
            {
                q.GetConteudo();
                questionsNumber = questoes.Count;
                StartCoroutine(RestClient.Instance.Get(3, "questaoFoto/" + q.idQuestao, GetQuestaoMidia, isDefault, idQuestao));
                activity.questoes.Add(q);
                idQuestao++;
            }
        }
        else
        {
            Debug.LogError("Erro aqui");
        }
    }

    private void GetQuestaoMidia(RespostaPadrao resposta, int idQuestao, int id2, int id3, bool isDefault)
    {
        loadedCount++;
        if (resposta.GetOk())
        {
            Midia midia = JsonConvert.DeserializeObject<Midia>(resposta.dado.ToString());
            
            if (midia != null)
            {
                midiasLoadCount++;
                activity.questoes[idQuestao].temMidia = true;
                string url = GameConstants.UPLOAD_URL + "/Questao/" + midia.idMidia + midia.extensao;
                StartCoroutine(GetImage(url, idQuestao));
            }
            else
            {
                activity.questoes[idQuestao].temMidia = false;
            }
        }
        else
        {
            if (loadedCount >= questionsNumber && midiasLoadCount == 0)
            {
                StartCoroutine(openActivityScreen());
            }
        }
    }

    IEnumerator GetImage(string url, int idQuestao)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(www);
                activity.questoes[idQuestao].textura = texture;

                midiasLoadCount--;

                if (loadedCount >= questionsNumber && midiasLoadCount == 0)
                    StartCoroutine(openActivityScreen());
            }
        }
    }

    IEnumerator openActivityScreen()
    {
        yield return new WaitForSeconds(1f);
        GameManager.Instance.OpenThisActivity(activity, "", true);
    }
}
