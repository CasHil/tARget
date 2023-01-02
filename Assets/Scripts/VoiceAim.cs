using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System;
using TextSpeech;
using System.Linq;
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
    public void StopListening(){
        SpeechToText.Instance.StopRecording();
    }

   public void onPartialSpeechResult(string result)
   {
    List<string> ResultList = result.Split(' ').ToList();
    string last = ResultList[ResultList.Count - 1];
    Debug.Log(pos);
         if (last.Contains("shoot"))
        {
            StopListening();
            GetComponent<Pointer>().Shoot();
                         
        }
    }
}





