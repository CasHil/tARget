using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TextSpeech;



public class Clicking : MonoBehaviour
{
    const string LANG_CODE = "en-US";
    [SerializeField]

    public Text UiText;
    public Button triggerButton;
    

void Start(){
    SetUp(LANG_CODE);
    
    SpeechToText.Instance.onPartialResultsCallback = onPartialSpeechResult;


}
   

void Update(){
    Button btn = triggerButton.GetComponent<Button>();
    btn.onClick.AddListener(StartListening);
   

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
void OnFinalSpeechResult(string result)
   {
        UiText.text = result;
        if (result.Contains("Jump") || result.Contains("jump"))
        {
              UiText.text = "yes";
        }
    }

void onPartialSpeechResult(string result)
   {
        UiText.text = result;
         if (result.Contains("Jump") || result.Contains("jump"))
        {
              UiText.text = "DEEBUG";
        }
        
       
    }
}
   


