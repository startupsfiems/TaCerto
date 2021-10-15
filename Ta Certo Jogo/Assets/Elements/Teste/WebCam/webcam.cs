using UnityEngine;
using System.Collections;

public class webcam : MonoBehaviour {
    void Start () {
        WebCamDevice[] devices = WebCamTexture.devices;
        Debug.Log("Number of web cams connected: " + devices.Length);
        Renderer rend = this.GetComponentInChildren<Renderer>();

        WebCamTexture mycam = new WebCamTexture();
        string camName = devices[0].name;
        Debug.Log("The webcam name is " + camName);
        mycam.deviceName = camName;
        rend.material.mainTexture = mycam;

        mycam.Play();
	}    
}