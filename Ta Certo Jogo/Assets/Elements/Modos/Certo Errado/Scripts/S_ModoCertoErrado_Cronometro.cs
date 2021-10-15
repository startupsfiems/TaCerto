using UnityEngine;
using TMPro;

public class S_ModoCertoErrado_Cronometro : MonoBehaviour{
    
    public TextMeshProUGUI timerTxt;
    public bool startTimer;

    int numMinutos;
    float numSegundos;
    string txtMinutos;
    string txtSegundos;

    public void SwitchTimer(bool start)
    {
        startTimer = start;
    }

    void Update(){
        if(startTimer)
            UpdateTimer();

        if(Input.GetKeyDown(KeyCode.A)){
            numSegundos += 10f;
        }
    }

    void UpdateTimer(){
        numSegundos += Time.deltaTime;
        int segundos = (int)numSegundos % 60;
        
        numMinutos = (int)numSegundos / 60;
        
        txtSegundos = segundos < 10f ? "0" + segundos.ToString() : segundos.ToString();
        txtMinutos = numMinutos < 10f ? "0" + numMinutos.ToString() : numMinutos.ToString();
        timerTxt.text = txtMinutos + ":" + txtSegundos;
    }

    public string getTimeAndStopCount()
    {
        startTimer = false;
        return txtMinutos + ":" + txtSegundos;
    }

    public int getSegundos()
    {
        return (int)numSegundos;
    }
}