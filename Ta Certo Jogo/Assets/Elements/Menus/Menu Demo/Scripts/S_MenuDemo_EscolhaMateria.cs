using UnityEngine;
using UnityEngine.SceneManagement;

public class S_MenuDemo_EscolhaMateria : MonoBehaviour{
    
    public SO_Modos_MateriasDemo materiaDemo;

    void Awake() {
        materiaDemo.portugues = false;
        materiaDemo.geografia = false;
        materiaDemo.historia = false;
        materiaDemo.ciencias = false;
        materiaDemo.artes = false;
        materiaDemo.geral = false;
    }

    public void EscolhaPortugues(int nivel){
        SceneManager.LoadScene(1);
        materiaDemo.portugues = true;
    }
    public void EscolhaGeografia(int nivel){
        SceneManager.LoadScene(1);
        materiaDemo.geografia = true;
    }
    public void EscolhaHistoria(int nivel){
        SceneManager.LoadScene(1);
        materiaDemo.historia = true;
    }
    public void EscolhaCienias(int nivel){
        SceneManager.LoadScene(1);
        materiaDemo.ciencias = true;
    }
    public void EscolhaArtes(int nivel){
        SceneManager.LoadScene(1);
        materiaDemo.artes = true;
    }
    public void EscolhaGeral(int nivel){
        SceneManager.LoadScene(1);
        materiaDemo.geral = true;
    }
}