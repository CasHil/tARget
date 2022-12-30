using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System;
using TextSpeech;
public class VoiceAim : MonoBehaviour
{

    Camera arCam;
    const string LANG_CODE = "en-US";
    Vector3 pos = new Vector3(100, 100, 0);
    [SerializeField]

    // Start is called before the first frame update
    void Start()
    {
        arCam = GameObject.Find("AR Camera").GetComponent<Camera>();
        SetUp(LANG_CODE);
        StartListening();
        SpeechToText.Instance.onPartialResultsCallback = onPartialSpeechResult;


    }

   
    void SetUp(string code) {
   // TextToSpeech.Instance.Setting(code,1,1);
    SpeechToText.Instance.Setting(code);

    }
    public void StartListening() {
    SpeechToText.Instance.StartRecording();
    
}
   public void onPartialSpeechResult(string result)
   {
        Debug.Log(result);
         if (result.Contains("Shoot") || result.Contains("shoot"))
        {
              RaycastHit hit;
              Ray ray = arCam.ScreenPointToRay(pos);
              GetComponent<instantiateProjectile>().Fire(arCam.transform.position, ray.direction);
              Debug.Log("BOOM");
        }
        
        
       
    }
}





