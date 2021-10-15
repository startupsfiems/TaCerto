using System;
using UnityEngine;

[Serializable]
public class Questao
{
    public int idQuestao { get; set; }
    public int idAtividade { get; set; }
    public int idTipoQuestao { get; set; }
    public string titulo { get; set; }
    public string enunciado { get; set; }
    public string jsonQuestao { get; set; }
    public float pesoNota { get; set; }
    public bool temMidia { get; set; }

    [NonSerialized]
    public Conteudo conteudo;
    [NonSerialized]
    public Midia midia;
    [NonSerialized]
    public Texture2D textura;

    public void GetConteudo()
    {
        ConteudoFactory aux = new ConteudoFactory();
        conteudo = new Conteudo();
        conteudo = aux.GetConteudo(this);
    }
}