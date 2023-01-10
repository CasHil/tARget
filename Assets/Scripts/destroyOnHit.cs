using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyOnHit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "target"){
            Debug.Log("Destroyed!");
            Destroy(gameObject);
        }
    }
}
