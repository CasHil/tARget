using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TouchScreenAiming : MonoBehaviour
{

    Camera arCam;

    // Start is called before the first frame update
    void Start()
    {
        arCam = GameObject.Find("AR Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount == 0)
        {
            return;
        }
        else
        {   
            Debug.Log("Touch!");
            RaycastHit hit;
            Ray ray = arCam.ScreenPointToRay(Input.GetTouch(0).position);
            GetComponent<instantiateProjectile>().Fire(arCam.transform.position, ray.direction);
        }
    }
}
