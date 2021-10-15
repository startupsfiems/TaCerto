using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Modos_DemoManager : MonoBehaviour{

    public SO_Modos_MateriasDemo materiasDemo;

    [Header("Background Materias")]
    public List<GameObject> backgroundModos = new List<GameObject>();

    [Header("Hud dos Modos")]
    public GameObject[] modos;
    public GameObject modoAtual;
    private int currentModo;

    [Header("Listas de Questoes")]
    public SO_ListaQuestoes[] Questoes;

    void Start(){
        if(materiasDemo.portugues){
            for (int i = 0; i < backgroundModos.Count; i++){
                if(backgroundModos[i].name == "Portugues")
                    backgroundModos[i].SetActive(true);
            }
        }else if(materiasDemo.geografia){
            for (int i = 0; i < backgroundModos.Count; i++){
                if(backgroundModos[i].name == "Geografia")
                    backgroundModos[i].SetActive(true);
            }
        }else if(materiasDemo.historia){
            for (int i = 0; i < backgroundModos.Count; i++){
                if(backgroundModos[i].name == "Historia")
                    backgroundModos[i].SetActive(true);
            }
        }else if(materiasDemo.ciencias){
            for (int i = 0; i < backgroundModos.Count; i++){
                if(backgroundModos[i].name == "Ciencias")
                    backgroundModos[i].SetActive(true);
            }
        }else if(materiasDemo.artes){
            for (int i = 0; i < backgroundModos.Count; i++){
                if(backgroundModos[i].name == "Artes")
                    backgroundModos[i].SetActive(true);
            }
        }else if(materiasDemo.geral){
            for (int i = 0; i < backgroundModos.Count; i++){
                if(backgroundModos[i].name == "Geral")
                    backgroundModos[i].SetActive(true);
            }
        }

        int index = Random.Range(0, modos.Length);
        currentModo = index;
        modos[currentModo].SetActive(true);
        modoAtual = modos[currentModo];
    }

    public void RandomModo(){
        int index = Random.Range(0, modos.Length);
        modos[currentModo].gameObject.GetComponent<Animator>().SetBool("Sair", true);
        currentModo = index;
        modos[currentModo].SetActive(true);
        modoAtual = modos[currentModo];
    }
}