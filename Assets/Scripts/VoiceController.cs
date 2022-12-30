using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System; 
using TextSpeech;

public class VoiceController : MonoBehaviour {
const string LANG_CODE = "en-US";
public Text UiText;


[SerializeField]

void Start(){
    SetUp(LANG_CODE);
    
    SpeechToText.Instance.onResultCallback = OnFinalSpeechResult;
}
/*
#region Text to Speech
public void StartSpeaking(string message) {
    TextToSpeech.Instance.StartSpeak(message);
}
public void StopSpeaking() {
    TextToSpeech.Instance.StopSpeak();
}
void OnSpeakStart() {
    Debug.Log("Talking started...");
}
void OnSpeakStop() {
    Debug.Log("Talking stopped");
}
#endregion
*/
#region Speech to Text

public void StartListening() {
    SpeechToText.Instance.StartRecording();
}

public void StopListening(){
    SpeechToText.Instance.StopRecording();
}
void OnFinalSpeechResult(string result){
       UiText.text = result;
       /*
       if (result.Contains("Hello"))
        {   
              Debug.Log("kommando");
        }
    */
}
#endregion

void SetUp(string code) {
   // TextToSpeech.Instance.Setting(code,1,1);
    SpeechToText.Instance.Setting(code);

}
}
