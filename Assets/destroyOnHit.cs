using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyOnHit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        gameObject.GetComponent<AudioSource>().Play();
        Destroy(gameObject);
    }
}
