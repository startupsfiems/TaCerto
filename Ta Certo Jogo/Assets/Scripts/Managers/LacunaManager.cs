using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using Random = System.Random;

public class LacunaManager : MonoBehaviour
{

    public AtividadeAtual atividadeAtual;
    public AudioManager audioManager;
    public RespostaManager respostaManager;
    private Questao questaoAtual;
    private int idQuestaoAtual;
    private float pesoAtual;
    public GameMenuManager gameMenuManager;
    private string fraseResposta;
    private IDictionary<int, string> dicionarioDeLacunasCorretas = new Dictionary<int, string>();
    private IDictionary<int, string> dicionarioResposta = new Dictionary<int, string>();
    private int numAcerto = 0;
    private int numErro = 0;

    [Header("Lacuna Elements")]
    public GameObject lacunaScreen;
    public GameObject optionsPrefab;
    public Animator lacunaAnimator;
    public TextMeshProUGUI contentText;
    public GameObject textContentGO;
    public GameObject optionsContentGO;
    private string textoAtual = "";
    private List<string> palavrasOpcoes;
    private int[][] palavrasOpcoesIndex;
    private int[] ordemDosIndex;
    public GameObject caixaDeText;
    public VerticalLayoutGroup textContentLayoutGroup;
    public LacunaText lacunaText;


    public void AtivaLacuna(int _idQuestaoAtual)
    {
        idQuestaoAtual = _idQuestaoAtual;
        questaoAtual = atividadeAtual.questoes[idQuestaoAtual];
        pesoAtual = questaoAtual.pesoNota;
        ConteudoLacunas conteudo = (ConteudoLacunas)questaoAtual.conteudo;
        textoAtual = "";
        int count = conteudo.FraseIsTexto.Length;
        for (int i = 0; i < count; i++)
        {
            if (conteudo.FraseIsTexto[i])
            {
                textoAtual += conteudo.FraseTexto[i];
            }
            else
            {
                textoAtual += "<sprite=0>";
            }
        }

        contentText.text = textoAtual;

        count = conteudo.AlternativaIndex.Length;
        palavrasOpcoes = new List<string>();
        palavrasOpcoesIndex = new int[count][];
        for (int i = 0; i < count; i++)
        {
            int countJ = conteudo.AlternativaIndex[i].Length;
            palavrasOpcoesIndex[i] = new int[countJ];
            for(int j = 0; j < countJ; j++)
                palavrasOpcoesIndex[i][j] = conteudo.AlternativaIndex[i][j];
            palavrasOpcoes.Add(conteudo.AlternativaTexto[i]);
        }

        ordemDosIndex = new int[palavrasOpcoes.Count];
        GeraOrdemDeInsercao();

        foreach (Transform child in optionsContentGO.transform)
            Destroy(child.gameObject);

        for (int i = 0; i < ordemDosIndex.Length; i++)
        {
            string palavra = palavrasOpcoes[ordemDosIndex[i]];
            GameObject optionItem = Instantiate(optionsPrefab);
            optionItem.name = "lacuna-" + palavra;
            LacunaOption prefabScript = optionItem.GetComponent<LacunaOption>();
            prefabScript.setLacunaText(palavra);

            optionItem.transform.SetParent(optionsContentGO.transform, false);
        }

        SetDicionarioDeRespostasCorretas(conteudo);

        gameMenuManager.checaSePrecisaMostrarExplicao("Lacunas");

        SetTextPosition();

        gameMenuManager.mostraUmaTelaEEscondeAsOutras("lacuna");

        lacunaText.setIfCanClick(true);
    }

    private void SetTextPosition()
    { 
        StartCoroutine(GetSize());      
    }

    IEnumerator GetSize()
    {
        yield return new WaitForSeconds(0.1f);

        float textHeight = contentText.GetComponent<RectTransform>().sizeDelta.y;

        float textBoxHeight = caixaDeText.GetComponent<RectTransform>().sizeDelta.y;
        if (textHeight + 20 < textBoxHeight)
        {
            float difference = (textBoxHeight - textHeight) / 2;

            textContentLayoutGroup.padding.top = (int)difference;
            textContentLayoutGroup.enabled = false;
            textContentLayoutGroup.enabled = true;
        }
    }

    private void GeraOrdemDeInsercao()
    {
        for (int i = 0; i < ordemDosIndex.Length; i++)
            ordemDosIndex[i] = i;

        Random random = new Random();

        int n = ordemDosIndex.Length;
        for(int i = 0; i < (n - 1); i++)
        {
            int r = i + random.Next(n - i);
            int item = ordemDosIndex[r];
            ordemDosIndex[r] = ordemDosIndex[i];
            ordemDosIndex[i] = item;
        }
    }

    private void SetDicionarioDeRespostasCorretas(ConteudoLacunas conteudo)
    {
        //dicionarioDeLacunasCorretas
        int count = conteudo.AlternativaIndex.Length;
        for (int i = 0; i < count; i++)
        {
            for(int j = 0; j < conteudo.AlternativaIndex[i].Length; j++)
            {
                int index = conteudo.AlternativaIndex[i][j];

                if (index != -1) {
                    dicionarioDeLacunasCorretas[index] = conteudo.AlternativaTexto[i];
                }
            }
        }

        SetFraseAtual(conteudo);
    }

    private void SetFraseAtual(ConteudoLacunas conteudo)
    {
        fraseResposta = "";
        int count = conteudo.FraseIsTexto.Length;
        for(int i = 0; i < count; i++)
        {
            if (conteudo.FraseIsTexto[i])
                fraseResposta += conteudo.FraseTexto[i];
            else
            {
                fraseResposta += dicionarioDeLacunasCorretas[i];
            }
        }
    }

    public void RespondeQuestao(string textoPreenchido)
    {
        string textoRespostaDoUsuario = textoPreenchido;
        string parteDoTexto = "";
        string fraseRespostaCorrigido = "";
        int indexCount = 0;
        numAcerto = 0;
        numErro = 0;
        do
        {
            int endIndex = textoRespostaDoUsuario.IndexOf("<color");
            if(endIndex == 0) {
                int auxIndex = textoRespostaDoUsuario.IndexOf("</color>");
                auxIndex += 8;
                parteDoTexto = textoRespostaDoUsuario.Substring(0, auxIndex);
                textoRespostaDoUsuario = textoRespostaDoUsuario.Substring(auxIndex);
                fraseRespostaCorrigido += CorrigeQuestao(indexCount, parteDoTexto);
            }else if(endIndex == -1)
            {
                fraseRespostaCorrigido += textoRespostaDoUsuario;
                textoRespostaDoUsuario = "";
            }
            else
            {
                parteDoTexto = textoRespostaDoUsuario.Substring(0, endIndex);
                textoRespostaDoUsuario = textoRespostaDoUsuario.Substring(endIndex);
                fraseRespostaCorrigido += parteDoTexto;
            }
            indexCount++;
        } while (textoRespostaDoUsuario.Length != 0);

        contentText.SetText(fraseRespostaCorrigido);

        StartCoroutine(ShowAnswerAndMove());

        if (atividadeAtual.precisaSalvarRespostas)
            respostaManager.CalculaEAddQuestaoRespostaAluno(questaoAtual.idQuestao, numAcerto, numErro, "", pesoAtual);
    }

    IEnumerator ShowAnswerAndMove()
    {
        SetAcertosEErros();
        yield return new WaitForSeconds(2f);

        lacunaAnimator.SetTrigger("Sair");

        gameMenuManager.telaLacuna.SetActive(false);
        GameManager.Instance.NextQuestionOrFinish();
    }

    private void SetAcertosEErros()
    {
        for (int i = 0; i < numAcerto; i++)
            gameMenuManager.setAcerto(true);

        for (int i = 0; i < numErro; i++)
            gameMenuManager.setAcerto(false);

        if(numErro > 0)
            audioManager.PlaySound("ErroLacuna");
        else
            audioManager.PlaySound("AcertoLacuna");

    }

    private string CorrigeQuestao(int index, string resposta)
    {
        resposta = RemoveColorTag(resposta);

        if (dicionarioDeLacunasCorretas[index].Equals(resposta))
        {
            numAcerto++;
            resposta = AddColorTag(resposta, true);
        }
        else
        {
            numErro++;
            resposta = AddColorTag(resposta, false);
        }
        return resposta;
    }

    private string RemoveColorTag(string palavra)
    {
        palavra = palavra.Replace("<color=#3662C3>", "");
        palavra = palavra.Replace("</color>", "");
        return palavra;
    }

    private string AddColorTag(string palavra, bool acertou)
    {
        string tagAbre = "<color=#58DD6B>";
        string tagFecha = "</color>";
        if (!acertou)
        {
            tagAbre = "<color=#FF3432><s>";
            tagFecha = "</s></color>";
        }
            
        palavra = tagAbre + palavra + tagFecha;
        return palavra;
    }

}
