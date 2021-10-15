using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LacunaText : MonoBehaviour, IPointerDownHandler
{

    private TextMeshProUGUI textContent;
    public AudioManager audioManager;
    private string conteudoDoTexto;
    private string conteudoDoTextoInicial;
    private int indexEncontrado;
    private IDictionary<string, GameObject> dicionariosDeLacunas = new Dictionary<string, GameObject>();
    private List<string> possiveisLacunasCompostas = new List<string>();
    public Transform lacunaParents;
    private int indexAtualClicado = -1;
    public LacunaManager lacunaManager;

    private bool canClick = true;

    // Start is called before the first frame update
    void Awake()
    {
        textContent = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        CarregaConteudo();
    }

    private void CarregaConteudo()
    {
        foreach (var item in dicionariosDeLacunas.Keys)
        {
            possiveisLacunasCompostas.Add(item.ToString());
        }
        conteudoDoTextoInicial = conteudoDoTexto = SetConteudoDoTexto();
    }

    public void setIfCanClick(bool value)
    {
        canClick = value;

        if (value)
            CarregaConteudo();
    }

    public string SetConteudoDoTexto()
    {
        conteudoDoTexto = textContent.text;

        if (Regex.Matches(conteudoDoTexto, "<sprite=0>").Count == 0)
        {
            lacunaManager.RespondeQuestao(conteudoDoTexto);
            setIfCanClick(false);
        }

        return conteudoDoTexto.Replace("<sprite=0>", "*");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        eventData.Use();
        string palavraClicada;
        if (canClick)
        {
            if (ChecaSeClicouNoTextoDaLacuna(eventData))
            {
                print("Clicou numa Lacuna");
            }
            else
            {
                palavraClicada = ChecaSeClicouNaPalavraLacuna(eventData);
                if (palavraClicada != null)
                {
                    if (dicionariosDeLacunas.ContainsKey("lacuna-" + palavraClicada))
                        RetiraPalavraDaLacuna(palavraClicada);
                }
            }  
        }
    }

    private void RetiraPalavraDaLacuna(string palavraClicada)
    {
        string fraseAtual = textContent.text;
        int indexDaPalavraParaSerRemovida = fraseAtual.IndexOf(">"+palavraClicada+"<");
        // pegar o index inicial
        int indexInicial = indexDaPalavraParaSerRemovida - 14;
        //Pegar o index final
        int indexFinal = indexDaPalavraParaSerRemovida + palavraClicada.Length + 9;
        //remover a palavra/ colocar sprite no lugar
        
        fraseAtual = fraseAtual.Remove(indexInicial, indexFinal-indexInicial);
        fraseAtual = fraseAtual.Insert(indexInicial, "<sprite=0>");
        SetLacunaTexto(fraseAtual);

        // voltar a lacuna de volta, filho do seu pai de verdade
        EncontraAndReativa(palavraClicada);
    }

    private void EncontraAndReativa(string palavra)
    {
        foreach (var item in dicionariosDeLacunas.Keys)
        {
            string palavraDaLacuna = item.ToString().Replace("lacuna-", "");
            if (palavraDaLacuna.Equals(palavra))
            {
                dicionariosDeLacunas[item].SetActive(true);
                //dicionariosDeLacunas[item].transform.SetParent(lacunaParents);
                dicionariosDeLacunas[item].gameObject.GetComponent<LacunaOption>().VoltaParaOPai();
                dicionariosDeLacunas.Remove(item);
                break;
            }
        }
    }

    public bool TextEndDrag(PointerEventData eventData, string lacunaText, GameObject lacuna)
    {
        if (ChecaSeClicouNoTextoDaLacuna(eventData))
        {
            InserePalavraNaLacuna(lacunaText);
            lacuna.SetActive(false);
            dicionariosDeLacunas[lacuna.name] = lacuna;
            return false;
        }
        else
        {
            return true;
        }
    }

    private void InserePalavraNaLacuna(string palavra)
    {
        // adicionar o tamanho das sprites se não for a primeira sprite clicada
        string conteudoAtualDoTexto = textContent.text;
        int numeroDeAsteriscos =  ContarAsteriscosAntes();
        int numeroDeTagColor = ContarTagsColor(indexEncontrado);
        if (numeroDeAsteriscos > 0)
            indexEncontrado += numeroDeAsteriscos * 9;
        if (numeroDeTagColor > 0)
            indexEncontrado += numeroDeTagColor * 23;

        audioManager.PlaySound("ColocandoLacuna");

        string antes = conteudoAtualDoTexto.Substring(0, indexEncontrado);
        string depois = conteudoAtualDoTexto.Substring(indexEncontrado + 10);
        string between = "<color=#3662C3>"+ palavra + "</color>";
        SetLacunaTexto(antes + between + depois);
    }

    private void SetLacunaTexto(string texto)
    {
        textContent.text = texto;
        conteudoDoTexto = SetConteudoDoTexto();
    }

    private int ContarTagsColor(int indexEncontrado)
    {
        string conteudoDoTextoSemTag = conteudoDoTexto;
        string conteudoDoTextComTag = conteudoDoTexto;
        conteudoDoTextoSemTag = conteudoDoTextoSemTag.Replace("<color=#3662C3>", "");
        conteudoDoTextoSemTag = conteudoDoTextoSemTag.Replace("</color>", "");

        int conteudoDoTextoSemTagSize = conteudoDoTextoSemTag.Length;

        int count = 0;
        bool semMaisTags = false;
        int indexDoMatch = 0;
        do
        {   
            if (Regex.Matches(conteudoDoTextComTag, "<col").Count >= indexDoMatch+1)
            {
                int colorTagIndex = Regex.Matches(conteudoDoTextComTag, "<col")[indexDoMatch].Index;
    
                colorTagIndex -= indexDoMatch * 23;
                int colorTagEndIndex = colorTagIndex;

                for (; conteudoDoTextoSemTagSize != colorTagEndIndex && !conteudoDoTextoSemTag[colorTagEndIndex].Equals(' '); colorTagEndIndex++) ;

                string palavraComTag = conteudoDoTextoSemTag.Substring(colorTagIndex, colorTagEndIndex - colorTagIndex);
                if (indexEncontrado > colorTagEndIndex)
                    count++;

                indexDoMatch++;
            } else
            {
                semMaisTags = true;
            }
        } while (!semMaisTags);

        return count;
    }

    private int ContarAsteriscosAntes()
    {
        string conteudoAtualDoTexto = conteudoDoTexto;
        conteudoAtualDoTexto = conteudoAtualDoTexto.Replace("<color=#3662C3>", "");
        conteudoAtualDoTexto = conteudoAtualDoTexto.Replace("</color>", "");
        int count = 0;
        for (int i = 0; i < indexAtualClicado - 1; i++) {
            if (conteudoAtualDoTexto[i] == '*')
                count++;
        }

        return count;
    }

    private string ChecaSeClicouNaPalavraLacuna(PointerEventData eventData)
    {
        foreach(GameObject go in eventData.hovered)
        {
            if(go.tag == "LacunaContentText")
            {
                int index = TMP_TextUtilities.FindIntersectingCharacter(textContent, eventData.position, Camera.main, true);
                int indexPalavra = TMP_TextUtilities.FindIntersectingWord(textContent, eventData.position, Camera.main);
                string palavraClicada = GetPalavraClicada(index);
                return palavraClicada;
            }
        }
        return null;
    }

    private string GetPalavraClicada(int index, bool precisaChecarComposta = true)
    {
        int start = -1;
        int end = -1;
        int i = index;
        string frase = conteudoDoTexto;
        frase = frase.Replace("<color=#3662C3>", "");
        frase = frase.Replace("</color>", "");
        int tamanho = frase.Length;
        do
        {
            i--;
            try
            {
                if (frase[i].Equals(' '))
                    start = i + 1;
            }catch
            {
                start = i + 1;
            }
        } while (start == -1);

        i = index;
        do
        {
            i++;
            try
            {
                if (frase[i].Equals(' ') || frase[i].Equals(',') || frase[i].Equals('.') || 
                    frase[i].Equals('?') || frase[i].Equals('!') || frase[i].Equals(';') || 
                    frase[i].Equals(':'))
                    end = i;
            }catch
            {
                end = i;
            }
        } while (end == -1);
        
        string palavra = frase.Substring(start, end - start);

        if(precisaChecarComposta)
            palavra = ChecaComposta(index, start, end, palavra, tamanho);

        return palavra;
    }

    private string ChecaComposta(int index, int start, int end, string palavra, int tamanho)
    {        
        if(dicionariosDeLacunas.ContainsKey("lacuna-" + palavra))
        {
            return palavra;
        }
        else
        {
            if(start - 1 >= 0)
            {
                string primeiraPalavra = GetPalavraClicada(start - 1, false);
                if (dicionariosDeLacunas.ContainsKey("lacuna-" + primeiraPalavra))
                {
                    return primeiraPalavra;
                }
            }

            if(end+1 <= tamanho)
            {
                string segundaPalavra = GetPalavraClicada(end + 1, false);
                if (dicionariosDeLacunas.ContainsKey("lacuna-" + palavra + " " + segundaPalavra))
                {
                    return palavra + " " + segundaPalavra;
                }
            }
            return palavra;
        }
    }

    private bool ChecaSeClicouNoTextoDaLacuna(PointerEventData eventData)
    {
        foreach (GameObject go in eventData.hovered)
        {
            if (go.tag == "LacunaContentText")
            {
                indexAtualClicado = TMP_TextUtilities.FindIntersectingCharacter(textContent, eventData.position, Camera.main, true);
                int indexPalavra = TMP_TextUtilities.FindIntersectingWord(textContent, eventData.position, Camera.main);
                if (ChecaSeClicouEmLacuna(indexAtualClicado, indexPalavra))
                {
                    indexEncontrado = indexAtualClicado;
                    return true;
                }
            }
        }

        return false;
    }

    private bool ChecaSeClicouEmLacuna(int indexLetra, int indexPalavra)
    {
        bool res;
        string[] palavra = new string[conteudoDoTexto.Length];
        string conteudoDoTextoLimpo = conteudoDoTexto;
        conteudoDoTextoLimpo = conteudoDoTextoLimpo.Replace("<color=#3662C3>", "");
        conteudoDoTextoLimpo = conteudoDoTextoLimpo.Replace("</color>", "");
        try
        {
            if (conteudoDoTextoLimpo[indexLetra].Equals('*'))
            {
                res = true;
            }
            else
            {
                res = false;
            }
        }
        catch
        {
            res = false;
        }
        
        return res;
    }
}
