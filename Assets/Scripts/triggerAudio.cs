using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerAudio : MonoBehaviour
{
     private void OnTriggerEnter(Collider other) {
        if(other.tag == "projectile"){
        gameObject.GetComponent<AudioSource>().Play();
        }
}
}